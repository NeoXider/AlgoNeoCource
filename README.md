# AlgoNeoCource

Материалы курсов для `AlgoNeoCourse`: JSON-описания курсов, Markdown-уроки, примеры, шаблоны и справочники для авторов контента.

## Что в репозитории

Этот репозиторий хранит не сам Unity-плагин, а содержимое курсов:

- файлы структуры курсов: `course1.json`, `course2.json` и при необходимости другие `courseN.json`;
- уроки в формате Markdown;
- встроенные примеры `quiz`, `check`, медиа и ссылок `unity://`;
- методические и технические гайды для авторов.

Плагин, который загружает эти материалы в Unity Editor, находится в отдельном репозитории: [Neo-Cource-Unity](https://github.com/NeoXider/Neo-Cource-Unity).

## Установка плагина

Рекомендуемый способ установки `AlgoNeoCourse` в Unity: через UPM по Git URL.

1. Откройте `Window -> Package Manager`.
2. Нажмите `+ -> Add package from git URL...`.
3. Укажите:

```text
https://github.com/NeoXider/Neo-Cource-Unity.git?path=Assets/_AlgoNeoCourse
```

Альтернатива через `Packages/manifest.json`:

```json
{
  "dependencies": {
    "com.neoxider.algoneocourse": "https://github.com/NeoXider/Neo-Cource-Unity.git?path=Assets/_AlgoNeoCourse"
  }
}
```

## Как подключить этот репозиторий как источник уроков

После установки плагина:

1. Откройте `Tools -> AlgoNeoCourse -> Settings -> Open Course Settings`.
2. Убедитесь, что `repositoryBaseUrl` указывает на raw-репозиторий курса.
3. Выберите файл курса:
   - `Course1` -> `course1.json`
   - `Course2` -> `course2.json`
   - `Custom` -> любой другой `courseN.json`
4. Нажмите `Загрузить список уроков`.
5. При необходимости скачайте выбранные уроки в локальную папку `Assets/_AlgoNeoCourse/Downloaded`.
6. Откройте `Tools -> AlgoNeoCourse -> Open Course Window`.

По умолчанию курс загружается из:

```text
https://raw.githubusercontent.com/NeoXider/AlgoNeoCource/main
```

## Быстрый workflow автора курса

- Добавьте запись в нужный файл курса, например в `course2.json`:

```json
{ "id": "l2m2y6", "title": "Новый урок", "file": "lessons2/m2/y6.md" }
```

- Создайте соответствующий `.md` файл, например `lessons2/m2/y6.md`.
- Разделите урок на слайды через `---`.
- При необходимости добавьте:
  - `quiz` для закрепления теории;
  - `check` для автопроверки;
  - изображения и видео;
  - ссылки `unity://open` для быстрого открытия ассетов.
- Откройте курс в Unity и проверьте урок в окне `AlgoNeoCourse`.

Подробная пошаговая инструкция: [QuickStart.md](QuickStart.md)

## Структура репозитория

| Путь | Назначение |
| --- | --- |
| `course1.json`, `course2.json` | Описание курсов и список уроков |
| `lessons1/`, `lessons2/` | Markdown-уроки по курсам |
| `QuickStart.md` | Быстрый старт для авторов |
| `CourseTechGuide.md` | Технический обзор формата уроков |
| `LessonMethodGuide.md` | Методические рекомендации |
| `Docs/` | Подробные справочники и шаблоны |
| `Docs/Examples/` | Готовые рабочие примеры |

## Что поддерживается

### Слайды

Слайды разделяются строкой `---`.

### Медиа

Поддерживаются:

- локальные относительные пути;
- проектные пути `Assets/...` и `Packages/...`;
- внешние URL;
- видео по прямой ссылке (`.mp4`, `.webm`, `.mov` и др.).

### Квизы

Поддерживаются три типа:

- `single`
- `multiple`
- `truefalse`

Прогресс квизов сохраняется локально в Unity-плагине автоматически, поэтому предыдущие уроки не нужно проходить заново после перезапуска Editor.

### Проверки

Поддерживаются `check`-блоки с правилами вроде:

- `object_exists`
- `component_exists`
- `filename`
- `contains`

## Курсы в репозитории

- `course1.json` -> `lessons1/`
- `course2.json` -> `lessons2/`

`course2.json` используется для второго курса, который уже разложен по модулям `m2`, `m3` и далее.

## Документация

- [QuickStart.md](QuickStart.md) — первый урок за несколько минут
- [CourseTechGuide.md](CourseTechGuide.md) — технический обзор формата
- [LessonMethodGuide.md](LessonMethodGuide.md) — структура, педагогика и правила оформления
- [Docs/README.md](Docs/README.md) — навигация по полной документации
- [Docs/CourseMarkdownSpec.md](Docs/CourseMarkdownSpec.md) — спецификация формата Markdown
- [Docs/QuizReference.md](Docs/QuizReference.md) — подробности по тестам
- [Docs/CheckReference.md](Docs/CheckReference.md) — подробности по проверкам
- [Docs/MediaReference.md](Docs/MediaReference.md) — изображения, видео и GIF
- [Docs/LessonTemplates.md](Docs/LessonTemplates.md) — шаблоны уроков
- [Docs/Examples/](Docs/Examples/) — рабочие примеры

## Важно помнить

- Этот репозиторий отвечает за содержимое курсов, а не за код плагина.
- Если меняется поведение окна курса, настроек, JSON-прогресса или GIF -> MP4, документацию здесь тоже нужно обновлять.
- Для новых курсов удобно придерживаться схемы `courseN.json` + `lessonsN/`.
