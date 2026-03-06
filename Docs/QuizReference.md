# Справочник по квизам

Актуальное руководство по `quiz`-блокам для уроков `AlgoNeoCourse`.

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

Обязательные поля:

| Поле | Описание |
| --- | --- |
| `id` | Уникальный идентификатор вопроса |
| `kind` | Тип: `single`, `multiple`, `truefalse` |
| `text` | Формулировка вопроса |
| `answers` | Список вариантов ответа |

Для каждого ответа используется:

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

Путь:

```text
Tools -> AlgoNeoCourse -> Settings -> Open Quiz Settings
```

Ключевые опции:

| Опция | Назначение |
| --- | --- |
| `maxAttemptsPerQuestion` | Максимум попыток |
| `randomizeAnswersOnCourseOpen` | Перемешивание ответов при открытии курса |
| `guardSlideNavigation` | Блокировка перехода вперёд, пока квиз не завершён |
| `stateJsonFolder` | Папка локальных JSON-сохранений |

Примечание:

- состояние квизов сохраняется локально автоматически;
- пользователь не должен заново проходить уже завершённые вопросы после перезапуска Unity;
- если нужен полный сброс, это делается через настройки плагина.

## Поведение и UX

### Когда вопрос считается завершённым

Вопрос завершён, если:

- дан правильный ответ;
- или закончились попытки.

### Блокировка навигации

Если включён `guardSlideNavigation`, переход к следующему слайду блокируется, пока на текущем слайде есть незавершённый квиз.

### Перемешивание ответов

Если включён `randomizeAnswersOnCourseOpen`, ответы перемешиваются при открытии курса и затем сохраняют это состояние.

## Рекомендации по проектированию вопросов

- один вопрос — один слайд;
- пояснение к вопросу лучше выносить на следующий слайд;
- для `multiple` всегда явно пишите "выберите все подходящие";
- не используйте очевидно неправильные дистракторы;
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

### Структура "вопрос -> пояснение"

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

Проверьте:

- есть ли закрывающие обратные кавычки;
- корректны ли YAML-отступы;
- уникален ли `id`.

### Ответы не перемешиваются

Проверьте `randomizeAnswersOnCourseOpen` и перезагрузите курс.

### Навигация не блокируется

Проверьте `guardSlideNavigation` и убедитесь, что квиз реально ещё не завершён.

### Подсветка работает неверно

Проверьте:

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
