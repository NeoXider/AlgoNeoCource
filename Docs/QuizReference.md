# Справочник по квизам

Краткий справочник по `quiz`-блокам для уроков `AlgoNeoCourse`.

## Базовый синтаксис

````md
```quiz
id: lesson-q1
kind: single
text: Текст вопроса
answers:
  - text: Верный вариант
    correct: true
  - text: Неверный вариант
```
````

Поля квиза:

| Поле | Описание |
| --- | --- |
| `id` | Уникальный идентификатор вопроса |
| `kind` | Тип: `single`, `multiple`, `truefalse` |
| `text` | Формулировка вопроса |
| `answers` | Список вариантов ответа |

Поля ответа:

| Поле | Описание |
| --- | --- |
| `text` | Текст варианта |
| `correct` | `true`, если ответ правильный |

## Поддерживаемые типы

### `single`

Один правильный вариант.

```quiz
id: method-start
kind: single
text: Какой метод вызывается при старте объекта?
answers:
  - text: Start()
    correct: true
  - text: Update()
  - text: Awake()
```

Поведение:

- ответ фиксируется по клику;
- попытка списывается сразу;
- правильный ответ закрывает вопрос.

### `multiple`

Несколько правильных вариантов.

```quiz
id: unity-components
kind: multiple
text: Выберите все компоненты Unity.
answers:
  - text: Transform
    correct: true
  - text: Rigidbody
    correct: true
  - text: GameObject
  - text: SceneManager
```

Поведение:

- можно отметить несколько вариантов;
- проверка выполняется кнопкой `Проверить`;
- попытка списывается в момент проверки.

### `truefalse`

Короткое утверждение на проверку.

```quiz
id: update-frame
kind: truefalse
text: Метод Update вызывается один раз за кадр.
answers:
  - text: True
    correct: true
  - text: False
```

Поведение аналогично `single`.

## Настройки в Unity

```text
Tools -> AlgoNeoCourse -> Settings -> Open Quiz Settings
```

Ключевые опции:

| Опция | Назначение |
| --- | --- |
| `maxAttemptsPerQuestion` | Максимум попыток |
| `randomizeAnswersOnCourseOpen` | Перемешивание ответов при открытии курса |
| `guardSlideNavigation` | Блокировка перехода вперёд, пока квиз не завершён |
| `stateJsonFolder` | Папка локальных сохранений |

Состояние квизов сохраняется локально. Завершённые вопросы не нужно проходить заново после перезапуска Unity; полный сброс делается через настройки плагина.

## Поведение

- вопрос завершён после правильного ответа или окончания попыток;
- `guardSlideNavigation` блокирует переход вперёд, пока на слайде есть незавершённый квиз;
- `randomizeAnswersOnCourseOpen` перемешивает ответы при открытии курса и сохраняет порядок.

## Рекомендации по проектированию вопросов

- один вопрос — один слайд;
- пояснение к вопросу лучше выносить на следующий слайд;
- для `multiple` явно пишите "выберите все подходящие";
- не используйте очевидно неверные дистракторы;
- не делайте правильный ответ систематически самым длинным или самым подробным вариантом;
- выравнивайте варианты по длине и конкретности;
- делайте `id` с привязкой к уроку, например `l2m2y3-q1`.

## Примеры

### Короткий вопрос по синтаксису

```quiz
id: csharp-private
kind: single
text: Какой модификатор доступа делает поле приватным?
answers:
  - text: private
    correct: true
  - text: public
  - text: protected
  - text: internal
```

### Вопрос с несколькими ответами

```quiz
id: rendering-components
kind: multiple
text: Какие компоненты участвуют в отрисовке?
answers:
  - text: MeshRenderer
    correct: true
  - text: SpriteRenderer
    correct: true
  - text: Rigidbody
  - text: Camera
    correct: true
```

### Вопрос и пояснение

````md
## Вопрос

```quiz
id: modulo-q1
kind: single
text: Чему равно `14 % 4`?
answers:
  - text: 3
  - text: 2
    correct: true
  - text: 0
```

---

## Пояснение

`%` возвращает остаток от деления.
````

## Частые ошибки

### Квиз не отображается

- есть ли закрывающие обратные кавычки;
- корректны ли YAML-отступы;
- уникален ли `id`.

### Ответы не перемешиваются

Проверьте `randomizeAnswersOnCourseOpen` и перезагрузите курс.

### Навигация не блокируется

Проверьте `guardSlideNavigation` и убедитесь, что квиз реально ещё не завершён.

### Подсветка работает неверно

- что у `single` и `truefalse` ровно один `correct: true`;
- что у `multiple` отмечены все правильные варианты.

## Шаблоны

### Single

```quiz
id:
kind: single
text:
answers:
  - text:
    correct: true
  - text:
  - text:
```

### Multiple

```quiz
id:
kind: multiple
text:
answers:
  - text:
    correct: true
  - text:
    correct: true
  - text:
  - text:
```

### True/False

```quiz
id:
kind: truefalse
text:
answers:
  - text: True
    correct: true
  - text: False
```

## См. также

- [../CourseTechGuide.md](../CourseTechGuide.md)
- [CourseMarkdownSpec.md](CourseMarkdownSpec.md)
- [Examples/quiz_single.md](Examples/quiz_single.md)
- [Examples/quiz_multiple_truefalse.md](Examples/quiz_multiple_truefalse.md)
