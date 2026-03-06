# CourseTechGuide

Краткий технический обзор формата уроков, который отражает текущее поведение `AlgoNeoCourse`.

Если нужны педагогические правила и структура занятий, откройте [LessonMethodGuide.md](LessonMethodGuide.md).

## Как устроен курс

Один курс описывается отдельным JSON-файлом:

- `course1.json`
- `course2.json`
- или кастомный `courseN.json`

Пример записи урока:

```json
{ "id": "l2m2y1", "title": "Основы синтаксиса C#", "file": "lessons2/m2/y1.md" }
```

Один урок — это один `.md` файл.

## Как это открывается в Unity

### Настройки источника курса

```text
Tools -> AlgoNeoCourse -> Settings -> Open Course Settings
```

Здесь задаются:

- `repositoryBaseUrl`;
- выбор курса `Course1`, `Course2` или `Custom`;
- загрузка списка уроков;
- скачивание уроков;
- настройки GIF-конвертации и путей.

### Окно курса

```text
Tools -> AlgoNeoCourse -> Open Course Window
```

Горячие клавиши:

- `Left` / `Right` — предыдущий или следующий слайд;
- `R` — перезагрузить урок;
- `O` — открыть текущий Markdown-файл.

## Слайды

Слайды разделяются `---`.

```md
# Слайд 1

Текст.

---

## Слайд 2

Следующий шаг.
```

Рекомендации:

- один слайд — одна мысль;
- не делайте длинные стены текста;
- оставляйте пустые строки между крупными блоками.

## Медиа

Можно использовать:

- относительные пути;
- `Assets/...`;
- `Packages/...`;
- внешние URL;
- поиск по имени файла в проекте.

Примеры:

```md
![](images/example.png)
![](videos/demo.mp4)
![](https://example.com/reference.jpg)
![](Assets/UI/Icons/logo.png)
```

По видео лучший вариант — MP4.

GIF:

- как GIF не проигрывается;
- может быть автоматически преобразован в MP4, если это включено в `Course Settings`;
- после успешной конвертации слайд обновляется.

Подробнее: [Docs/MediaReference.md](Docs/MediaReference.md)

## Квизы

Поддерживаются:

- `single`
- `multiple`
- `truefalse`

Пример:

```quiz
id: l2m2y1-q1
kind: single
text: Какой метод Unity вызывается при старте объекта?
answers:
  - text: Start()
    correct: true
  - text: Update()
  - text: Awake()
```

Ключевые факты:

- квизы сохраняют прогресс локально автоматически;
- навигация вперёд может блокироваться до завершения вопроса;
- для `multiple` проверка идёт по кнопке `Проверить`.

Подробнее: [Docs/QuizReference.md](Docs/QuizReference.md)

## Автопроверки

Проверки оформляются через `check`.

````md
```check
rules:
  - object_exists: "Player"
  - component_exists: { object: "Player", type: "Rigidbody" }
  - filename: "PlayerController.cs"
  - contains: "public class PlayerController"
```
````

Базовые правила:

- `object_exists`
- `component_exists`
- `filename`
- `contains`

Лучше несколько небольших проверок, чем одна сверхстрогая.

Для отладки можно включить:

```text
Project Settings -> AlgoNeoCourse -> Validation -> Debug Render Check Blocks
```

Подробнее: [Docs/CheckReference.md](Docs/CheckReference.md)

## Unity-ссылки

Можно открывать ассеты прямо из урока:

```md
[Открыть префаб](unity://open?path=Assets/Prefabs/Player.prefab)
```

## Front matter

Необязательный блок в начале урока:

```yaml
---
id: l2m2y3
module: 2
lesson: 3
title: Операторы и приоритет
tags: [beginner, csharp]
est_time_min: 20
---
```

## Минимальный рабочий каркас урока

````md
# Название урока

Вступление.

---

## Теория

Объяснение.

---

## Вопрос

```quiz
id: lesson-q1
kind: single
text: Вопрос?
answers:
  - text: Верно
    correct: true
  - text: Неверно
```

---

## Практика

```check
rules:
  - filename: "Example.cs"
```

---

## Итоги
````

## Чек-лист

- урок добавлен в правильный `courseN.json`;
- путь `file` корректен;
- слайды разделены `---`;
- у квизов уникальные `id`;
- медиа доступны;
- практики имеют рабочие проверки, если они нужны.

## Куда смотреть дальше

- [QuickStart.md](QuickStart.md)
- [LessonMethodGuide.md](LessonMethodGuide.md)
- [Docs/CourseMarkdownSpec.md](Docs/CourseMarkdownSpec.md)
- [Docs/LessonTemplates.md](Docs/LessonTemplates.md)
- [Docs/Examples/](Docs/Examples/)
