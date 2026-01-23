# Документация курса — Оглавление

Полная документация по созданию интерактивных курсов для Unity.

## Быстрая навигация

### Начало работы

Если вы только начинаете, читайте в таком порядке:

1. **[../QuickStart.md](../QuickStart.md)** — создайте первый урок за 5 минут
2. **[../CourseTechGuide.md](../CourseTechGuide.md)** — обзор всех возможностей системы
3. **[../LessonMethodGuide.md](../LessonMethodGuide.md)** — правила оформления и педагогический подход
4. **[CourseMarkdownSpec.md](CourseMarkdownSpec.md)** — полная спецификация формата Markdown

### Справочники (Reference)

Подробные руководства по отдельным функциям:

- **[QuizReference.md](QuizReference.md)** — всё про интерактивные тесты (quiz)
- **[CheckReference.md](CheckReference.md)** — всё про автоматические проверки (check)
- **[MediaReference.md](MediaReference.md)** — работа с изображениями и видео

### Шаблоны

- **[LessonTemplates.md](LessonTemplates.md)** — готовые шаблоны уроков для копирования

### Примеры (Examples)

Практические примеры всех возможностей:

- **[Examples/full_lesson.md](Examples/full_lesson.md)** — полный пример урока со всеми элементами
- **[Examples/quiz_single.md](Examples/quiz_single.md)** — примеры single-choice тестов
- **[Examples/quiz_multiple_truefalse.md](Examples/quiz_multiple_truefalse.md)** — примеры multiple и true/false
- **[Examples/media.md](Examples/media.md)** — примеры локальных медиа
- **[Examples/mediaUrl.md](Examples/mediaUrl.md)** — примеры внешних URL
- **[Examples/check_block_scene.md](Examples/check_block_scene.md)** — проверка объектов сцены
- **[Examples/check_block_script.md](Examples/check_block_script.md)** — проверка скриптов
- **[Examples/check_block_debug.md](Examples/check_block_debug.md)** — дебаг-режим проверок
- **[Examples/open_asset.md](Examples/open_asset.md)** — открытие ассетов через ссылки

## Описание файлов

### CourseMarkdownSpec.md
**Полная спецификация Markdown-формата**

Что внутри:
- Описание уроков в `course.json`
- Создание `.md` файлов уроков
- Разделение на слайды
- Медиа (изображения/видео)
- Проверки и действия (`unity://` ссылки)
- Квизы (интерактивные тесты)
- Метаданные урока
- Каркасы и шаблоны
- Чек-лист перед публикацией

**Кому:** Для углубленного понимания формата и всех технических деталей.

### QuizReference.md
**Справочник по интерактивным тестам**

Что внутри:
- Три типа квизов: single, multiple, truefalse
- Полный синтаксис YAML
- Настройки в Quiz Settings
- Поведение и UX каждого типа
- Персистенция и сохранение прогресса
- Примеры всех типов
- Рекомендации по созданию вопросов
- Шаблоны для копирования
- Отладка

**Кому:** Для тех, кто создаёт тесты и хочет понять все нюансы поведения.

### CheckReference.md
**Справочник по автоматическим проверкам**

Что внутри:
- Все типы правил проверки
- Синтаксис YAML
- `object_exists`, `component_exists`, `filename`, `contains`
- Комбинированные проверки (сцена + скрипт)
- Примеры для типичных задач
- Рекомендации по гибкости проверок
- Шаблоны для копирования
- Отладка и частые проблемы

**Кому:** Для тех, кто создаёт практические задания с автоматической проверкой.

### MediaReference.md
**Справочник по работе с медиа**

Что внутри:
- Поддерживаемые форматы изображений и видео
- Способы указания путей (относительные, абсолютные, URL, проектные)
- Особенности GIF и конвертация в MP4
- Организация файлов медиа
- Оптимизация размеров
- Примеры всех вариантов путей
- Отладка проблем с загрузкой

**Кому:** Для тех, кто работает с изображениями и видео в уроках.

### LessonTemplates.md
**Готовые шаблоны уроков**

Что внутри:
- Шаблон полного урока (Теория → Практика → Перерыв → Теория → Практика → Итоги)
- Шаблон короткого урока
- Шаблон концепт + тест
- Шаблоны отдельных слайдов (теория, задание, лайфхак, итоги)
- Шаблоны блоков (quiz, check)
- Специальные шаблоны (введение, практикум)
- Рекомендации по выбору и адаптации

**Кому:** Для быстрого старта — копируйте и адаптируйте под свои нужды.

## Структура документации

```
AlgoNeoCource/
├── README.md                    # Обзор проекта, ссылки на документацию
├── QuickStart.md                # Быстрый старт за 5 минут
├── CourseTechGuide.md           # Краткий обзор возможностей
├── LessonMethodGuide.md         # Правила оформления и педагогика
│
├── course1.json                 # Структура курса 1
├── course2.json                 # Структура курса 2
│
├── lessons1/                    # Уроки курса 1
│   └── m2/
│       └── y1.md
│
├── lessons2/                    # Уроки курса 2
│   ├── m2/                      # Модуль 2
│   │   ├── y1.md               # Урок 1
│   │   ├── y2.md               # Урок 2
│   │   └── images/             # Медиа для модуля 2
│   └── m3/                      # Модуль 3
│
└── Docs/                        # Полная документация
    ├── README.md               # Этот файл - навигация
    ├── CourseMarkdownSpec.md   # Полная спецификация
    ├── QuizReference.md        # Справочник по квизам
    ├── CheckReference.md       # Справочник по проверкам
    ├── MediaReference.md       # Справочник по медиа
    ├── LessonTemplates.md      # Шаблоны уроков
    │
    └── Examples/               # Практические примеры
        ├── full_lesson.md
        ├── quiz_single.md
        ├── quiz_multiple_truefalse.md
        ├── check_block_scene.md
        ├── check_block_script.md
        ├── media.md
        └── ...
```

## Рекомендуемый путь обучения

### Уровень 1: Новичок (15-20 минут)
1. Прочитайте [../README.md](../README.md) — обзор проекта
2. Следуйте [../QuickStart.md](../QuickStart.md) — создайте первый урок
3. Просмотрите [Examples/full_lesson.md](Examples/full_lesson.md) — увидьте полный пример

**Результат:** Вы сможете создать базовый урок с текстом и слайдами.

### Уровень 2: Базовый (30-40 минут)
1. Прочитайте [../CourseTechGuide.md](../CourseTechGuide.md) — все возможности
2. Изучите [LessonTemplates.md](LessonTemplates.md) — готовые шаблоны
3. Прочитайте [../LessonMethodGuide.md](../LessonMethodGuide.md) — правила оформления
4. Попробуйте примеры из [Examples/](Examples/)

**Результат:** Вы сможете создавать уроки с медиа, тестами и проверками.

### Уровень 3: Продвинутый (1-2 часа)
1. Изучите [QuizReference.md](QuizReference.md) — все про тесты
2. Изучите [CheckReference.md](CheckReference.md) — все про проверки
3. Изучите [MediaReference.md](MediaReference.md) — все про медиа
4. Прочитайте [CourseMarkdownSpec.md](CourseMarkdownSpec.md) — полную спецификацию

**Результат:** Вы будете знать все нюансы и сможете решать сложные задачи.

## Частые вопросы

### Как создать первый урок?
→ [../QuickStart.md](../QuickStart.md)

### Какие типы тестов поддерживаются?
→ [QuizReference.md](QuizReference.md) — single, multiple, truefalse

### Как проверить, что студент выполнил задание?
→ [CheckReference.md](CheckReference.md) — блоки `check`

### Как вставить изображение или видео?
→ [MediaReference.md](MediaReference.md) — все способы

### Где взять готовый шаблон урока?
→ [LessonTemplates.md](LessonTemplates.md) — копируйте и адаптируйте

### Как правильно оформить урок?
→ [../LessonMethodGuide.md](../LessonMethodGuide.md) — правила и педагогика

### Где посмотреть примеры?
→ [Examples/](Examples/) — практические примеры всех фич

## Полезные ссылки

- **Репозиторий плагина:** [Neo-Cource-Unity](https://github.com/NeoXider/Neo-Cource-Unity)
- **Unity версия:** 6.2
- **Формат уроков:** Markdown (.md)
- **Конфигурация:** course.json

## Обратная связь

Если нашли ошибку в документации или есть предложения по улучшению, создайте issue в репозитории плагина или свяжитесь с командой разработки.

---

**Версия документации:** 1.1  
**Последнее обновление:** 2025-01-27

