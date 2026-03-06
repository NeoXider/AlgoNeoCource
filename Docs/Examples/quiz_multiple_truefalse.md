# Пример Quiz — Multiple Choice и True/False

Примеры тестов с несколькими правильными ответами и формата `truefalse`.

## Общие правила

- Блок `quiz` содержит `id`, `kind`, `text`, `answers`.
- Для `multiple` пользователь выбирает несколько вариантов и нажимает `Проверить`.
- Для `truefalse` поведение такое же, как у `single`, но с двумя ответами.
- Попытки, перемешивание и защита навигации настраиваются в `Tools -> AlgoNeoCourse -> Settings -> Open Quiz Settings`.
- Состояние квизов сохраняется локально автоматически.
- После каждого вопроса лучше размещать поясняющий слайд. [▶](unity://slide?dir=next)

## Пример `multiple`

```quiz
id: mc-unity-components
kind: multiple
text: Какие из перечисленных являются компонентами Unity?
answers:
  - text: Transform
    correct: true
  - text: Rigidbody
    correct: true
  - text: AnimatorController
  - text: SceneManager
```

## Пример `truefalse`

```quiz
id: tf-update
kind: truefalse
text: Метод Update вызывается один раз за кадр.
answers:
  - text: True
    correct: true
  - text: False
```

---

## Пояснение (пример)

- Компоненты в Unity — это `Transform`, `Rigidbody`, `Collider` и другие типы `Component`.
- `AnimatorController` — это ассет-контроллер, а не компонент.
- `SceneManager` — статический API для управления сценами, а не компонент.
