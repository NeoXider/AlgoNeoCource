# Документация AlgoNeoCource

Навигационный файл по документации репозитория с материалами курсов для `AlgoNeoCourse`.

## С чего начать

Если вы только подключились к проекту, идите в таком порядке:

1. [../README.md](../README.md) — что хранится в репозитории и как подключить курс в Unity.
2. [../QuickStart.md](../QuickStart.md) — как быстро добавить новый урок и увидеть его в редакторе.
3. [../LessonMethodGuide.md](../LessonMethodGuide.md) — правила структуры, педагогики и практик.
4. [CourseMarkdownSpec.md](CourseMarkdownSpec.md) — полная техническая спецификация формата.

## Основные документы

### Базовые гайды

- [../README.md](../README.md) — обзор репозитория курсов
- [../QuickStart.md](../QuickStart.md) — быстрый старт
- [../CourseTechGuide.md](../CourseTechGuide.md) — технический обзор всех возможностей
- [../LessonMethodGuide.md](../LessonMethodGuide.md) — методические правила

### Справочники

- [CourseMarkdownSpec.md](CourseMarkdownSpec.md) — формат уроков и файла курса
- [QuizReference.md](QuizReference.md) — всё про `quiz`
- [CheckReference.md](CheckReference.md) — всё про `check`
- [MediaReference.md](MediaReference.md) — картинки, видео, GIF и пути

### Шаблоны и примеры

- [LessonTemplates.md](LessonTemplates.md) — шаблоны уроков
- [Examples/full_lesson.md](Examples/full_lesson.md) — полный пример урока
- [Examples/quiz_single.md](Examples/quiz_single.md) — single-choice
- [Examples/quiz_multiple_truefalse.md](Examples/quiz_multiple_truefalse.md) — multiple и true/false
- [Examples/media.md](Examples/media.md) — локальные медиа
- [Examples/mediaUrl.md](Examples/mediaUrl.md) — внешние URL
- [Examples/open_asset.md](Examples/open_asset.md) — `unity://open`
- [Examples/check_block_scene.md](Examples/check_block_scene.md) — проверка объектов сцены
- [Examples/check_block_script.md](Examples/check_block_script.md) — проверка скриптов
- [Examples/check_block_debug.md](Examples/check_block_debug.md) — debug-показ проверок

## Что где описано

| Документ | Когда читать |
| --- | --- |
| `QuickStart.md` | Когда нужно быстро добавить новый урок |
| `LessonMethodGuide.md` | Когда важно выдержать структуру и методику |
| `CourseTechGuide.md` | Когда нужен обзор возможностей без глубокой детализации |
| `CourseMarkdownSpec.md` | Когда нужен точный синтаксис и ограничения |
| `QuizReference.md` | Когда добавляете тесты |
| `CheckReference.md` | Когда добавляете практические задания с автопроверкой |
| `MediaReference.md` | Когда работаете с картинками, видео и GIF |
| `LessonTemplates.md` | Когда нужен каркас для нового урока |

## Ключевые факты проекта

- Один курс описывается отдельным JSON-файлом: `course1.json`, `course2.json` или любым `courseN.json`.
- Один урок — это один `.md` файл, указанный в поле `file`.
- Плагин устанавливается через UPM из репозитория [Neo-Cource-Unity](https://github.com/NeoXider/Neo-Cource-Unity).
- В Unity курс открывается через `Tools -> AlgoNeoCourse -> Open Course Window`.
- Источник материалов настраивается в `Tools -> AlgoNeoCourse -> Settings -> Open Course Settings`.
- Прогресс квизов и позиция курса сохраняются локально в Unity-плагине автоматически.

## Структура репозитория

```text
AlgoNeoCource/
├── README.md
├── QuickStart.md
├── CourseTechGuide.md
├── LessonMethodGuide.md
├── course1.json
├── course2.json
├── lessons1/
├── lessons2/
└── Docs/
    ├── README.md
    ├── CourseMarkdownSpec.md
    ├── QuizReference.md
    ├── CheckReference.md
    ├── MediaReference.md
    ├── LessonTemplates.md
    └── Examples/
```

## Рекомендуемый маршрут для автора

### Быстрый старт

1. Добавьте запись в `course1.json` или `course2.json`.
2. Создайте `.md` файл урока.
3. Проверьте урок через `AlgoNeoCourse` в Unity.

### Нормальный рабочий цикл

1. Сверьтесь с [LessonMethodGuide.md](../LessonMethodGuide.md).
2. Соберите каркас урока по [LessonTemplates.md](LessonTemplates.md).
3. Проверьте синтаксис по [CourseMarkdownSpec.md](CourseMarkdownSpec.md).
4. Для интерактива используйте:
   - [QuizReference.md](QuizReference.md)
   - [CheckReference.md](CheckReference.md)
   - [MediaReference.md](MediaReference.md)

## Полезные ссылки

- Репозиторий плагина: [Neo-Cource-Unity](https://github.com/NeoXider/Neo-Cource-Unity)
- UPM Git URL плагина:

```text
https://github.com/NeoXider/Neo-Cource-Unity.git?path=Assets/_AlgoNeoCourse
```

## Версия документации

- Совместимо с актуальным плагином `AlgoNeoCourse 1.4.x`
