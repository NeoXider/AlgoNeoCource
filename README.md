# AlgoNeoCource

Репозиторий с учебным контентом для Unity-пакета `AlgoNeoCourse`: структуры курсов, Markdown-уроки, готовые скрипты, примеры и справочники для авторов.

Сам Unity-плагин находится отдельно: [Neo-Cource-Unity](https://github.com/NeoXider/Neo-Cource-Unity).

## Что здесь лежит

| Путь | Назначение |
| --- | --- |
| `course.json` | Первый курс, сейчас содержит урок по Terrain |
| `course2.json` | Основной курс C# для Unity |
| `lessons1/` | Уроки первого курса |
| `lessons2/` | Уроки второго курса, разложенные по модулям |
| `lessons2/*/result-scripts/` | Итоговые C#-скрипты для уроков |
| `Docs/` | Справочники по формату уроков, квизам, проверкам и медиа |
| `tools/` | Вспомогательные инструменты для анализа материалов |

Это не Unity-проект: здесь нет `Assets/`, `Packages/` и `ProjectSettings/`.

## Курсы

`course.json`:

- `lessons1/m2/y3.md` — Terrain в Unity.

`course2.json`:

- `m2` — основы C#, переменные, методы, жизненный цикл Unity;
- `m3` — условия, циклы и Dino 3D;
- `m4` — крестики-нолики;
- `m5` — Tower Defence;
- `m6` — аркадный космический шутер.

Каждая запись курса указывает на Markdown-файл урока:

```json
{ "id": "l2m2y1", "title": "l2 / m2 / Основы синтаксиса C#", "file": "lessons2/m2/y1.md" }
```

## Подключение в Unity

1. Установите Unity-пакет `AlgoNeoCourse` из репозитория [Neo-Cource-Unity](https://github.com/NeoXider/Neo-Cource-Unity).
2. Откройте `Tools -> AlgoNeoCourse -> Settings -> Open Course Settings`.
3. Укажите `repositoryBaseUrl`:

```text
https://raw.githubusercontent.com/NeoXider/AlgoNeoCource/main
```

4. Выберите курс:
   - `Course1` использует `course.json`;
   - `Course2` использует `course2.json`;
   - `Custom` позволяет указать другой JSON.
5. Нажмите `Загрузить список уроков`.
6. Откройте `Tools -> AlgoNeoCourse -> Open Course Window`.

## Формат урока

Уроки пишутся в Markdown. Слайды разделяются строкой:

```md
---
```

Поддерживаются:

- обычный Markdown;
- блоки кода;
- `quiz` для вопросов;
- `check` для автопроверок;
- изображения и видео;
- ссылки `unity://open` и `unity://check`.

Пример квиза:

````md
```quiz
id: l2m2y1-q-example
kind: single
text: Что такое скрипт в Unity?
answers:
  - text: Файл с кодом для объекта
    correct: true
  - text: Картинка в проекте
  - text: Сцена целиком
```
````

Пример проверки:

````md
```check
rules:
  - filename: "PlayerController.cs"
  - contains: "public class PlayerController"
```
````

## Рекомендации для квизов

- Не делайте правильный ответ постоянно самым длинным или самым подробным.
- Неправильные варианты должны быть понятными и явно неверными, но не абсурдными.
- Для `single` используйте один `correct: true`.
- Для `multiple` явно пишите в вопросе, что нужно выбрать все подходящие варианты.
- `True`/`False` оставляйте короткими и одинаково привычными для ученика.

Подробнее: [Docs/QuizReference.md](Docs/QuizReference.md).

## Зависимости

У этого репозитория контента нет Unity-зависимостей. Единственная внешняя зависимость самого Unity-пакета `AlgoNeoCourse` — `com.unity.nuget.newtonsoft-json`.

## Документация

- [QuickStart.md](QuickStart.md) — быстрый старт для автора урока.
- [CourseTechGuide.md](CourseTechGuide.md) — технический обзор формата.
- [LessonMethodGuide.md](LessonMethodGuide.md) — методика подготовки уроков.
- [Docs/README.md](Docs/README.md) — навигация по полной документации.
- [Docs/CourseMarkdownSpec.md](Docs/CourseMarkdownSpec.md) — спецификация Markdown-формата.
- [Docs/QuizReference.md](Docs/QuizReference.md) — правила и примеры квизов.
- [Docs/CheckReference.md](Docs/CheckReference.md) — правила автопроверок.
- [Docs/MediaReference.md](Docs/MediaReference.md) — изображения, видео и пути к медиа.
- [Docs/LessonTemplates.md](Docs/LessonTemplates.md) — шаблоны уроков.

## Быстрый workflow автора

1. Добавьте запись в `course.json` или `course2.json`.
2. Создайте Markdown-файл урока.
3. Разбейте материал на слайды через `---`.
4. Добавьте квизы и проверки там, где они реально помогают ученику.
5. Проверьте урок в Unity через окно `AlgoNeoCourse`.
