# Быстрый старт: создать и проверить новый урок

Это краткий путь от нового `.md` файла до проверки урока в окне `AlgoNeoCourse`.

## Шаг 1. Добавьте урок в JSON курса

Откройте нужный файл курса, например `course2.json`, и добавьте запись в массив `lessons`.

```json
{
  "id": "l2m2y6",
  "title": "Ваш новый урок",
  "file": "lessons2/m2/y6.md"
}
```

Поддерживаемая схема репозитория:

- `course1.json` -> `lessons1/`
- `course2.json` -> `lessons2/`
- любой новый `courseN.json` -> `lessonsN/`

Минимальные требования:

- `id` — уникальный идентификатор урока;
- `title` — название, которое увидит пользователь в окне курса;
- `file` — путь к `.md` файлу относительно корня этого репозитория.

## Шаг 2. Создайте Markdown-файл урока

Создайте файл по пути, указанному в `file`, например `lessons2/m2/y6.md`.

Минимальный шаблон:

```md
# Модуль 2, Урок 6: Новый урок

Короткое вступление.

---

## Теория

Объяснение темы.

---

## Практика

Небольшое задание.

---

## Итоги

- Что изучили
- Что закрепили
```

Правила:

- каждый слайд разделяется `---`;
- между слайдами оставляйте пустую строку;
- один слайд должен содержать одну законченную мысль.

## Шаг 3. Добавьте интерактив

### Картинки и видео

```md
![](images/example.png)
![](https://example.com/demo.mp4)
```

### Квиз

````md
```quiz
id: q-y6-1
kind: single
text: Какой метод Unity вызывается один раз при запуске объекта?
answers:
  - text: Start()
    correct: true
  - text: Update()
  - text: FixedUpdate()
```
````

### Автопроверка

````md
```check
rules:
  - filename: "PlayerController.cs"
  - contains: "public class PlayerController"
  - contains: "Debug.Log"
```
````

## Шаг 4. Подключите курс в Unity

- Установите плагин `AlgoNeoCourse` через UPM:

```text
https://github.com/NeoXider/Neo-Cource-Unity.git?path=Assets/_AlgoNeoCourse
```

- Откройте `Tools -> AlgoNeoCourse -> Settings -> Open Course Settings`.
- Проверьте `repositoryBaseUrl`:

```text
https://raw.githubusercontent.com/NeoXider/AlgoNeoCource/main
```

- Выберите курс:
  - `Course1`
  - `Course2`
  - `Custom`
- Нажмите `Загрузить список уроков`.
- При необходимости нажмите `Скачать выбранные`.
- Откройте `Tools -> AlgoNeoCourse -> Open Course Window`.

## Шаг 5. Проверьте урок

В окне курса:

- выберите урок в выпадающем списке;
- проверьте переключение слайдов;
- проверьте квизы;
- проверьте `check`-блоки;
- при необходимости нажмите `R` для перезагрузки урока.

Горячие клавиши:

- `Left` / `Right` — переход по слайдам;
- `R` — обновить текущий урок;
- `O` — показать текущий `.md` в проводнике.

## Что проверить перед публикацией

- урок добавлен в правильный `courseN.json`;
- путь `file` совпадает с реальным путём файла;
- все квизы имеют уникальные `id`;
- `check`-блоки проверяют ключевые условия, а не слишком точные детали;
- медиа открываются локально или по URL;
- в конце есть слайд `## Итоги`.

## Что читать дальше

- [CourseTechGuide.md](CourseTechGuide.md) — технический обзор формата
- [LessonMethodGuide.md](LessonMethodGuide.md) — методика и структура уроков
- [Docs/CourseMarkdownSpec.md](Docs/CourseMarkdownSpec.md) — спецификация формата
- [Docs/LessonTemplates.md](Docs/LessonTemplates.md) — шаблоны уроков
- [Docs/Examples/](Docs/Examples/) — рабочие примеры
