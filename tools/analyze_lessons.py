#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""Analyze AlgoNeo course Markdown lessons.

Examples:
  py tools/analyze_lessons.py --root lessons2
  py tools/analyze_lessons.py --root lessons2 --module m5
  py tools/analyze_lessons.py --root lessons2 --module m5 --details
  py tools/analyze_lessons.py --root lessons2 --module m5 --csv lesson-report.csv
  py tools/analyze_lessons.py --root lessons2 --allow-quiz-without-explanations
  py tools/analyze_lessons.py --root lessons2 --skip-contains

Besides the methodology norms the script also validates every ```check block:
each `contains` is bound to the nearest preceding `filename` (the same way the
Unity checker does) and must literally occur in the matching
result-scripts/<lesson>/<file>. It also measures code load: size of csharp
blocks, streaks of consecutive code slides and the size of practice sections.
"""

from __future__ import annotations

import argparse
import csv
import re
from dataclasses import asdict, dataclass
from pathlib import Path
from statistics import mean


@dataclass
class Block:
    kind: str
    start_line: int
    end_line: int | None
    text: str


@dataclass
class LessonReport:
    file: str
    slides: int
    quiz: int
    check: int
    rules: int
    steps: int
    h2: int
    h3: int
    theory: int
    practice: int
    explain: int
    questions: int
    lifehack: int
    error_help: int
    verify_text: int
    quiz_no_explain: int
    long_slides: int
    empty_slides: int
    code_blocks: int
    code_lines: int
    long_code_blocks: int
    max_code_streak: int
    multi_code_slides: int
    contains_checked: int
    contains_failed: int
    score: int
    issues: str
    quiz_notes: str
    check_notes: str
    chunk_notes: str


def parse_args() -> argparse.Namespace:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--root", default="lessons2", help="Lessons root folder.")
    parser.add_argument("--module", default="", help="Optional module folder, e.g. m5.")
    parser.add_argument("--min-slides", type=int, default=22)
    parser.add_argument("--min-quizzes", type=int, default=5)
    parser.add_argument("--min-checks", type=int, default=2)
    parser.add_argument("--min-steps", type=int, default=4)
    parser.add_argument("--min-explain", type=int, default=5)
    parser.add_argument("--min-lifehacks", type=int, default=1)
    parser.add_argument("--max-long-slides", type=int, default=2)
    parser.add_argument("--long-slide-chars", type=int, default=1400)
    parser.add_argument(
        "--max-code-block-lines",
        type=int,
        default=20,
        help="Maximum lines in a single csharp block before it is flagged.",
    )
    parser.add_argument(
        "--max-code-streak",
        type=int,
        default=4,
        help="Maximum number of consecutive code slides before it is flagged.",
    )
    parser.add_argument(
        "--max-practice-steps",
        type=int,
        default=6,
        help="Maximum steps in one practice section before it is flagged as oversized.",
    )
    parser.add_argument(
        "--max-practice-code-lines",
        type=int,
        default=55,
        help="Maximum csharp lines in one practice section before it is flagged as oversized.",
    )
    parser.add_argument(
        "--skip-contains",
        action="store_true",
        help="Do not verify check/contains rules against result-scripts sources.",
    )
    parser.add_argument(
        "--allow-quiz-without-explanations",
        action="store_true",
        help="Do not flag quizzes that are not followed by a nearby explanation slide.",
    )
    parser.add_argument(
        "--include-support-files",
        action="store_true",
        help="Include non-lesson Markdown files such as summaries and guides.",
    )
    parser.add_argument(
        "--details",
        action="store_true",
        help="Print exact quiz ids/lines that are not followed by a nearby explanation slide.",
    )
    parser.add_argument("--csv", default="", help="Optional CSV output path.")
    return parser.parse_args()


def rel(path: Path) -> str:
    try:
        return str(path.resolve().relative_to(Path.cwd().resolve()))
    except ValueError:
        return str(path)


def count(pattern: str, text: str, flags: int = 0) -> int:
    return len(re.findall(pattern, text, flags))


def fenced_blocks(lines: list[str], kind: str) -> list[Block]:
    blocks: list[Block] = []
    start_re = re.compile(rf"^```\s*{re.escape(kind)}\s*$")
    i = 0
    while i < len(lines):
        if not start_re.match(lines[i].strip()):
            i += 1
            continue

        start = i
        end: int | None = None
        j = i + 1
        while j < len(lines):
            if lines[j].strip() == "```":
                end = j
                break
            j += 1

        block_text = "\n".join(lines[start + 1 : end]) if end is not None else ""
        blocks.append(
            Block(
                kind=kind,
                start_line=start + 1,
                end_line=(end + 1) if end is not None else None,
                text=block_text,
            )
        )
        i = (end + 1) if end is not None else (start + 1)
    return blocks


def slide_stats(slides: list[list[str]], long_slide_chars: int) -> tuple[int, int, int]:
    texts = ["\n".join(slide).strip() for slide in slides]
    long_slides = sum(1 for slide in texts if len(slide) > long_slide_chars)
    empty_slides = sum(1 for slide in texts if not slide)
    return len(texts), long_slides, empty_slides


def quiz_id(block: Block) -> str | None:
    match = re.search(r"(?m)^id:\s*(\S+)", block.text)
    return match.group(1) if match else None


EXPLAIN_HEADING_RE = re.compile(
    r"^#{1,6}\s+("
    r"Пояснение|"
    r"Ответы|"
    r"Как работает|"
    r"Как читать|"
    r"Почему|"
    r"Что значит"
    r")"
)


def quiz_without_explanation(lines: list[str], quiz_blocks: list[Block]) -> list[str]:
    missing: list[str] = []
    for quiz in quiz_blocks:
        if quiz.end_line is None:
            missing.append(f"{quiz_id(quiz) or 'unknown'} at line {quiz.start_line}: unclosed")
            continue

        found = False
        start_index = quiz.end_line
        end_index = min(start_index + 18, len(lines))
        for line in lines[start_index:end_index]:
            stripped = line.strip()
            if stripped == "---":
                continue
            if re.match(r"^#{1,6}\s+", stripped):
                found = bool(EXPLAIN_HEADING_RE.search(stripped))
                break

        if not found:
            missing.append(f"{quiz_id(quiz) or 'unknown'} at line {quiz.start_line}")
    return missing


def split_slides(lines: list[str]) -> list[list[str]]:
    """Split a lesson into slides by top-level `---`, ignoring fenced code."""
    slides: list[list[str]] = []
    current: list[str] = []
    in_fence = False
    for line in lines:
        stripped = line.strip()
        if stripped.startswith("```"):
            in_fence = not in_fence
            current.append(line)
            continue
        if not in_fence and stripped == "---":
            slides.append(current)
            current = []
        else:
            current.append(line)
    slides.append(current)
    return slides


def csharp_blocks(slide: list[str]) -> list[list[str]]:
    """Return the bodies of all ```csharp blocks inside one slide."""
    blocks: list[list[str]] = []
    body: list[str] | None = None
    lang = ""
    for line in slide:
        stripped = line.strip()
        if stripped.startswith("```"):
            if body is None:
                lang = stripped[3:].strip()
                body = []
            else:
                if lang == "csharp":
                    blocks.append(body)
                body = None
                lang = ""
            continue
        if body is not None:
            body.append(line)
    return blocks


def scalar_value(raw: str) -> str:
    """Read a YAML-ish scalar the way the Unity checker does: quotes are honoured."""
    value = raw.strip()
    if value.startswith('"'):
        out: list[str] = []
        index = 1
        while index < len(value):
            char = value[index]
            if char == "\\" and index + 1 < len(value):
                out.append(value[index + 1])
                index += 2
                continue
            if char == '"':
                break
            out.append(char)
            index += 1
        return "".join(out)
    return value.split("#")[0].strip()


def check_groups(block: Block) -> tuple[list[tuple[str, list[str]]], list[str]]:
    """Group `contains` rules under the nearest preceding `filename`."""
    groups: list[tuple[str, list[str]]] = []
    orphans: list[str] = []
    current: tuple[str, list[str]] | None = None
    for line in block.text.splitlines():
        stripped = line.strip()
        match = re.match(r"^-?\s*filename\s*:\s*(.+)$", stripped)
        if match:
            current = (scalar_value(match.group(1)), [])
            groups.append(current)
            continue
        match = re.match(r"^-?\s*contains\s*:\s*(.+)$", stripped)
        if match:
            term = scalar_value(match.group(1))
            if current is None:
                orphans.append(term)
            else:
                current[1].append(term)
    return groups, orphans


def verify_checks(path: Path, check_blocks: list[Block]) -> tuple[int, int, list[str]]:
    """Check that every `contains` literally occurs in the matching result script."""
    scripts_dir = path.parent / "result-scripts" / path.stem
    checked = 0
    failed = 0
    notes: list[str] = []

    for block in check_blocks:
        groups, orphans = check_groups(block)
        for term in orphans:
            failed += 1
            notes.append(f"line {block.start_line}: contains without filename -> {term}")
        for filename, terms in groups:
            if not terms:
                continue
            source_path = scripts_dir / filename
            if not source_path.exists():
                failed += len(terms)
                notes.append(f"line {block.start_line}: no result script {rel(source_path)}")
                continue
            source = source_path.read_text(encoding="utf-8").lower()
            for term in terms:
                checked += 1
                if term.lower() not in source:
                    failed += 1
                    notes.append(f"line {block.start_line} [{filename}]: not found -> {term}")
    return checked, failed, notes


def chunk_stats(slides: list[list[str]], args: argparse.Namespace) -> dict[str, object]:
    """Measure code load: block sizes, streaks of code slides and practice sizes."""
    blocks_total = 0
    lines_total = 0
    long_blocks = 0
    multi_slides = 0
    streak = 0
    max_streak = 0
    notes: list[str] = []

    for slide in slides:
        blocks = csharp_blocks(slide)
        blocks_total += len(blocks)
        if len(blocks) > 1:
            multi_slides += 1
        for body in blocks:
            size = len([line for line in body if line.strip()])
            lines_total += size
            if size > args.max_code_block_lines:
                long_blocks += 1
                head = next((line.strip() for line in slide if line.strip()), "")
                notes.append(f"code block {size} lines: {head[:60]}")
        if blocks:
            streak += 1
            max_streak = max(max_streak, streak)
        else:
            streak = 0

    practices: list[dict[str, object]] = []
    current: dict[str, object] | None = None
    for slide in slides:
        heading = next(
            (line.strip() for line in slide if re.match(r"^##\s+", line.strip())),
            None,
        )
        if heading:
            if current:
                practices.append(current)
                current = None
            if re.match(r"^##\s+Практика", heading):
                current = {"name": heading.lstrip("# ").strip(), "slides": 0, "steps": 0,
                           "blocks": 0, "lines": 0}
        if current is None:
            continue
        current["slides"] = int(current["slides"]) + 1
        current["steps"] = int(current["steps"]) + sum(
            1 for line in slide if re.match(r"^###\s+Шаг\b", line.strip())
        )
        blocks = csharp_blocks(slide)
        current["blocks"] = int(current["blocks"]) + len(blocks)
        current["lines"] = int(current["lines"]) + sum(
            len([line for line in body if line.strip()]) for body in blocks
        )
    if current:
        practices.append(current)

    for practice in practices:
        if (
            int(practice["steps"]) > args.max_practice_steps
            or int(practice["lines"]) > args.max_practice_code_lines
        ):
            notes.append(
                "%s: шагов=%d блоков=%d строк=%d"
                % (practice["name"][:40], practice["steps"], practice["blocks"], practice["lines"])
            )
    if max_streak > args.max_code_streak:
        notes.append(f"подряд слайдов с кодом: {max_streak}")

    return {
        "blocks": blocks_total,
        "lines": lines_total,
        "long_blocks": long_blocks,
        "multi_slides": multi_slides,
        "max_streak": max_streak,
        "notes": notes,
    }


def lesson_files(root: Path, module: str, include_support_files: bool) -> list[Path]:
    search_root = root / module if module else root
    if not search_root.exists():
        raise FileNotFoundError(f"Path not found: {search_root}")

    files = sorted(search_root.rglob("*.md"))
    if include_support_files:
        return files

    return [
        path
        for path in files
        if re.fullmatch(r"m\d+", path.parent.name) and re.fullmatch(r"y\d+", path.stem)
    ]


def score_issues(issues: list[str]) -> int:
    score = 100
    for issue in issues:
        if issue.startswith("slides"):
            score -= 15
        elif issue.startswith("quiz"):
            score -= 15
        elif issue.startswith("check"):
            score -= 10
        elif issue.startswith("steps"):
            score -= 10
        elif issue.startswith("explain"):
            score -= 10
        elif issue.startswith("lifehack"):
            score -= 5
        elif issue.startswith("missing"):
            score -= 10
        elif issue.startswith("unbalanced"):
            score -= 25
        elif issue.startswith("contains"):
            score -= 25
        elif issue.startswith("long code"):
            score -= 8
        elif issue.startswith("too many") or issue.startswith("empty"):
            score -= 8
        else:
            score -= 5
    return max(score, 0)


def lesson_number(path: Path) -> int:
    match = re.fullmatch(r"y(\d+)", path.stem)
    return int(match.group(1)) if match else 0


def analyze_file(
    path: Path,
    args: argparse.Namespace,
    quiz_ids: dict[str, str],
    global_issues: list[str],
    last_lesson_by_module: dict[str, int],
) -> LessonReport:
    text = path.read_text(encoding="utf-8")
    lines = text.splitlines()
    relative = rel(path)

    slide_list = split_slides(lines)
    slides, long_slides, empty_slides = slide_stats(slide_list, args.long_slide_chars)
    chunks = chunk_stats(slide_list, args)
    quizzes = fenced_blocks(lines, "quiz")
    checks = fenced_blocks(lines, "check")
    if args.skip_contains:
        contains_checked, contains_failed, check_notes = 0, 0, []
    else:
        contains_checked, contains_failed, check_notes = verify_checks(path, checks)
    module = path.parent.name
    current_lesson = lesson_number(path)
    is_edge_lesson = current_lesson == 1 or current_lesson == last_lesson_by_module.get(module)
    min_slides = min(args.min_slides, 18) if is_edge_lesson else args.min_slides
    min_steps = min(args.min_steps, 2) if is_edge_lesson else args.min_steps

    for quiz in quizzes:
        qid = quiz_id(quiz)
        if not qid:
            global_issues.append(f"Quiz without id in {relative} line {quiz.start_line}")
            continue
        if qid in quiz_ids:
            global_issues.append(f"Duplicate quiz id '{qid}' in {relative} and {quiz_ids[qid]}")
        else:
            quiz_ids[qid] = relative

    quiz_no_explain = quiz_without_explanation(lines, quizzes)

    h2 = count(r"(?m)^##\s+", text)
    h3 = count(r"(?m)^###\s+", text)
    steps = count(r"(?m)^###\s+(Шаг|Часть|Задание)\b", text)
    theory = count(r"(?m)^##\s+Теория\b", text)
    practice = count(r"(?m)^##\s+Практика\b", text)
    explain = count(
        r"^##\s+Пояснение|^###\s+Пояснение|>\s+\*\*Как работает|>\s+\*\*По шагам",
        text,
        re.MULTILINE,
    )
    questions = count(r"(?m)^##\s+(Как|Почему|Что|Зачем|Когда)\b", text)
    lifehack = count(r"Лайфхак", text)
    breaks = count(r"(?m)^##\s+Перерыв\s*$", text)
    summary = count(r"(?m)^##\s+Итоги\s*$", text)
    error_help = count(
        r"NullReferenceException|ошибк|не работает|не появляется|не вызывается|"
        r"не двигается|не получает|не добавил|ничего не делает|проверь",
        text,
        re.IGNORECASE,
    )
    verify_text = count(r"Что проверить|Проверьте|чеклист|Play Mode|Console", text, re.IGNORECASE)
    rules = count(r"(?m)^rules:\s*$", text)
    fence_count = text.count("```")

    issues: list[str] = []
    if slides < min_slides:
        issues.append(f"slides<{min_slides}")
    if len(quizzes) < args.min_quizzes:
        issues.append(f"quiz<{args.min_quizzes}")
    if len(checks) < args.min_checks:
        issues.append(f"check<{args.min_checks}")
    if steps < min_steps:
        issues.append(f"steps<{min_steps}")
    if explain < args.min_explain:
        issues.append(f"explain<{args.min_explain}")
    if lifehack < args.min_lifehacks:
        issues.append(f"lifehack<{args.min_lifehacks}")
    if breaks == 0:
        issues.append("missing break")
    if summary == 0:
        issues.append("missing summary")
    if fence_count % 2:
        issues.append("unbalanced fences")
    if long_slides > args.max_long_slides:
        issues.append("too many long slides")
    if empty_slides:
        issues.append("empty slides")
    if quiz_no_explain and not args.allow_quiz_without_explanations:
        issues.append(f"quiz may lack explanation: {len(quiz_no_explain)}")
    if contains_failed:
        issues.append(f"contains not found: {contains_failed}")
    if int(chunks["long_blocks"]):
        issues.append(f"long code blocks: {chunks['long_blocks']}")

    return LessonReport(
        file=relative,
        slides=slides,
        quiz=len(quizzes),
        check=len(checks),
        rules=rules,
        steps=steps,
        h2=h2,
        h3=h3,
        theory=theory,
        practice=practice,
        explain=explain,
        questions=questions,
        lifehack=lifehack,
        error_help=error_help,
        verify_text=verify_text,
        quiz_no_explain=len(quiz_no_explain),
        long_slides=long_slides,
        empty_slides=empty_slides,
        code_blocks=int(chunks["blocks"]),
        code_lines=int(chunks["lines"]),
        long_code_blocks=int(chunks["long_blocks"]),
        max_code_streak=int(chunks["max_streak"]),
        multi_code_slides=int(chunks["multi_slides"]),
        contains_checked=contains_checked,
        contains_failed=contains_failed,
        score=score_issues(issues),
        issues="; ".join(issues),
        quiz_notes="; ".join(quiz_no_explain),
        check_notes="; ".join(check_notes),
        chunk_notes="; ".join(str(note) for note in chunks["notes"]),
    )


def module_name(file_path: str) -> str:
    normalized = file_path.replace("\\", "/")
    match = re.search(r"/(m\d+)/", "/" + normalized)
    return match.group(1) if match else "unknown"


def fmt(value: object) -> str:
    if isinstance(value, float):
        return f"{value:.1f}"
    return str(value)


def print_table(rows: list[dict[str, object]], columns: list[str]) -> None:
    if not rows:
        print("(no rows)")
        return

    widths = {column: len(column) for column in columns}
    for row in rows:
        for column in columns:
            widths[column] = max(widths[column], len(fmt(row.get(column, ""))))

    print(" ".join(column.ljust(widths[column]) for column in columns))
    print(" ".join("-" * widths[column] for column in columns))
    for row in rows:
        print(" ".join(fmt(row.get(column, "")).ljust(widths[column]) for column in columns))


def summarize(reports: list[LessonReport]) -> list[dict[str, object]]:
    groups: dict[str, list[LessonReport]] = {}
    for report in reports:
        groups.setdefault(module_name(report.file), []).append(report)

    rows: list[dict[str, object]] = []
    for module, group in sorted(groups.items()):
        rows.append(
            {
                "Module": module,
                "Lessons": len(group),
                "AvgSlides": mean(item.slides for item in group),
                "AvgQuiz": mean(item.quiz for item in group),
                "AvgCheck": mean(item.check for item in group),
                "AvgSteps": mean(item.steps for item in group),
                "AvgExplain": mean(item.explain for item in group),
                "AvgQuizNoExplain": mean(item.quiz_no_explain for item in group),
                "AvgCodeLines": mean(item.code_lines for item in group),
                "AvgScore": mean(item.score for item in group),
                "WeakLessons": sum(1 for item in group if item.issues),
            }
        )
    return rows


def main() -> int:
    args = parse_args()
    root = Path(args.root)
    files = lesson_files(root, args.module, args.include_support_files)
    if not files:
        raise SystemExit(f"No lesson markdown files found under {root}")

    quiz_ids: dict[str, str] = {}
    global_issues: list[str] = []
    last_lesson_by_module: dict[str, int] = {}
    for path in files:
        if re.fullmatch(r"m\d+", path.parent.name):
            last_lesson_by_module[path.parent.name] = max(
                last_lesson_by_module.get(path.parent.name, 0),
                lesson_number(path),
            )

    reports = [
        analyze_file(path, args, quiz_ids, global_issues, last_lesson_by_module)
        for path in files
    ]

    print()
    print("Lesson quality analysis")
    search_root = root / args.module if args.module else root
    print(f"Root: {search_root}")
    print(
        "Thresholds: "
        f"slides>={args.min_slides}, quiz>={args.min_quizzes}, "
        f"check>={args.min_checks}, steps>={args.min_steps}, "
        f"explain>={args.min_explain}, lifehack>={args.min_lifehacks}"
    )
    print("Edge lesson rule: first and last lesson use relaxed slides>=18 and steps>=2.")
    print()

    report_rows = [asdict(report) for report in reports]
    print_table(
        report_rows,
        [
            "file",
            "slides",
            "quiz",
            "check",
            "steps",
            "lifehack",
            "explain",
            "error_help",
            "quiz_no_explain",
            "long_slides",
            "code_blocks",
            "code_lines",
            "max_code_streak",
            "score",
            "issues",
        ],
    )

    print()
    print("Module summary")
    print_table(
        summarize(reports),
        [
            "Module",
            "Lessons",
            "AvgSlides",
            "AvgQuiz",
            "AvgCheck",
            "AvgSteps",
            "AvgExplain",
            "AvgQuizNoExplain",
            "AvgCodeLines",
            "AvgScore",
            "WeakLessons",
        ],
    )

    total_contains = sum(report.contains_checked for report in reports)
    failed_contains = sum(report.contains_failed for report in reports)
    if not args.skip_contains:
        print()
        print(f"Check rules: contains verified {total_contains}, mismatches {failed_contains}")
        for report in reports:
            if report.check_notes:
                print(f"- {report.file}: {report.check_notes}")

    chunky = [report for report in reports if report.chunk_notes]
    print()
    print("Code load warnings (soft, not scored)")
    if chunky:
        for report in chunky:
            print(f"- {report.file}: {report.chunk_notes}")
    else:
        print("(none)")

    if global_issues:
        print()
        print("Global issues")
        for issue in global_issues:
            print(f"- {issue}")

    weak = [report for report in reports if report.issues]
    if weak:
        print()
        print("Weak lessons")
        for report in weak:
            print(f"- {report.file}: {report.issues}")

    if args.details:
        detailed = [report for report in reports if report.quiz_notes]
        if detailed:
            print()
            print("Quiz explanation gaps")
            for report in detailed:
                print(f"- {report.file}: {report.quiz_notes}")

    if args.csv:
        with Path(args.csv).open("w", encoding="utf-8", newline="") as handle:
            writer = csv.DictWriter(handle, fieldnames=list(asdict(reports[0]).keys()))
            writer.writeheader()
            writer.writerows(asdict(report) for report in reports)
        print()
        print(f"CSV written: {args.csv}")

    return 0


if __name__ == "__main__":
    raise SystemExit(main())
