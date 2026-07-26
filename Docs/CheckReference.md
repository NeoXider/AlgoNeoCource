# Справочник по проверкам

## Содержание

- [Основы](#основы)
- [Синтаксис](#синтаксис)
- [Поддерживаемые правила](#поддерживаемые-правила)
- [Комбинированные проверки](#комбинированные-проверки)
- [Примеры](#примеры)
- [Рекомендации](#рекомендации)
- [Отладка](#отладка)

## Основы

Блок `check` проверяет выполнение задания в Unity: объекты сцены, компоненты, файлы и фрагменты кода.

Базовый пример:

````md
```check
rules:
  - object_exists: "Player"
  - component_exists:
      object: "Player"
      type: "Rigidbody"
```
````

Как это работает:

1. Студент выполняет задание в Unity.
2. Нажимает `Проверить`.
3. Плагин выполняет правила из `rules`.
4. Результат появляется под кнопкой и в консоли Unity.

## Синтаксис

````check
# Необязательно: обычно определяется автоматически
type: scene | script | combined

# Обязательно
rules:
  - правило1
  - правило2
````

Правила форматирования:

- `rules:` начинается с новой строки;
- каждое правило начинается с `- `;
- отступы только пробелами, стандартно 2 пробела;
- вложенные параметры пишутся с дополнительным отступом;
- `type` обычно не нужен.

Правильное форматирование:

````yaml
rules:
  - object_exists: "Player"
  - component_exists:
      object: "Player"
      type: "Rigidbody"
  - filename: "Script.cs"
  - contains: "код"
````

> **Важно:** `component_exists` пишется только многострочно. Инлайн-форма через фигурные скобки парсером не читается, и проверка молча не выполнится.

## Поддерживаемые правила

Поддерживаются только:

- `object_exists`
- `component_exists`
- `filename`
- `contains`

Любые другие ключи проверяющий молча пропускает. Если в блоке нет ни одного распознанного правила, блок станет «всегда зелёным».

### `object_exists`

Проверяет точное имя объекта на активной сцене. Регистр важен.

```yaml
rules:
  - object_exists: "ИмяОбъекта"
```

**Примеры:**

````check
rules:
  - object_exists: "Player"
  - object_exists: "Enemy"
  - object_exists: "Main Camera"
````

### `component_exists`

Проверяет компонент на объекте. Формат только многострочный:

```yaml
rules:
  - component_exists:
      object: "ИмяОбъекта"
      type: "ИмяКомпонента"
```

````check
rules:
  - component_exists:
      object: "Player"
      type: "Rigidbody"
  
  - component_exists:
      object: "Player"
      type: "BoxCollider"
  
  - component_exists:
      object: "Main Camera"
      type: "Physics2DRaycaster"
````

Параметры:

- `object` — точное имя объекта;
- `type` — имя типа компонента без namespace;
- для 2D используйте точные типы: `Rigidbody2D`, `BoxCollider2D`, `CircleCollider2D`.

Частые компоненты:

| 3D | 2D | UI | Other |
|----|----|----|-------|
| Rigidbody | Rigidbody2D | Canvas | AudioSource |
| BoxCollider | BoxCollider2D | Image | Camera |
| SphereCollider | CircleCollider2D | Button | Light |
| MeshRenderer | SpriteRenderer | Text | Animator |

### `filename`

Проверяет наличие файла в проекте рекурсивно. Указывайте имя с расширением, регистр важен.

```yaml
rules:
  - filename: "ИмяФайла.cs"
```

**Примеры:**

````check
rules:
  - filename: "PlayerController.cs"
  - filename: "EnemyAI.cs"
  - filename: "GameManager.cs"
````

### `contains`

Ищет подстроку в файле, указанном ближайшим предшествующим `filename`.

```yaml
rules:
  - filename: "ИмяФайла.cs"
  - contains: "строка для поиска"
```

**Примеры:**

````check
rules:
  - filename: "PlayerController.cs"
  - contains: "public class PlayerController"
  - contains: "void Update()"
  - contains: "Debug.Log"
````

Важно: `contains` применяется к ближайшему `filename`, который стоит ВЫШЕ него.
В одном блоке можно указать несколько пар `filename` + `contains` — каждая группа
проверяется в своём файле, а общий результат = все группы пройдены:

````check
rules:
  - filename: "Script1.cs"
  - contains: "text1"  # ищет в Script1.cs
  
  - filename: "Script2.cs"
  - contains: "text2"  # ищет в Script2.cs
  - contains: "text3"  # тоже ищет в Script2.cs
````

Если `filename` для группы указан, но `contains` в ней нет — достаточно самого факта
существования файла.

Если `contains` записан ДО первого `filename`, он относится к первому `filename` блока.

**Значение и кавычки.** Значение берётся после первого двоеточия, поэтому двоеточия
внутри строки поиска сохраняются. Если значение обрамлено двойными кавычками,
кавычки снимаются, а внутри распознаются экранирования `\"`, `\\`, `\n`, `\t`:

````check
rules:
  - filename: "GameManager.cs"
  - contains: "currencyText.text = \"Монеты: \" + currency"
````

> **Предупреждение для авторов уроков (совместимость со старыми версиями плагина).**
> Старые версии плагина брали только ПЕРВЫЙ `filename` в блоке (все `contains` искались
> в нём) и обрезали значение по ВТОРОМУ двоеточию, не снимая экранирование.
> Если урок может открываться на старой версии, безопаснее:
> - держать один `filename` на блок `check` (несколько файлов — несколько блоков);
> - не использовать в `contains` двоеточия и кавычки — проверяйте более короткий
>   и гибкий фрагмент (например, `currencyText.text` вместо целой строки со строковым литералом).

## Комбинированные проверки

Можно проверять сцену и код в одном блоке.

```check
rules:
  - object_exists: "Player"
  - component_exists:
      object: "Player"
      type: "Rigidbody"
  - component_exists:
      object: "Player"
      type: "BoxCollider"
  
  - filename: "PlayerController.cs"
  - contains: "public class PlayerController"
  - contains: "void Start()"
  - contains: "void Update()"
```

Несколько объектов и скриптов:

```check
rules:
  - object_exists: "Player"
  - object_exists: "Enemy"
  
  - component_exists:
      object: "Player"
      type: "SpriteRenderer"
  - component_exists:
      object: "Player"
      type: "Collider2D"
  
  # Компоненты Enemy
  - component_exists:
      object: "Enemy"
      type: "Rigidbody2D"
  
  - filename: "PlayerController.cs"
  - contains: "public class PlayerController"
  
  - filename: "EnemyAI.cs"
  - contains: "public class EnemyAI"
  - contains: "void Update()"
```

UI:

```check
rules:
  - object_exists: "Canvas"
  - component_exists:
      object: "Canvas"
      type: "Canvas"
  
  - object_exists: "ScoreText"
  - component_exists:
      object: "ScoreText"
      type: "TMP_Text"
```

## Примеры

### Игровой объект

**Задание:**
> Создайте на сцене объект с именем `Player` и добавьте ему компонент `Rigidbody`.

**Проверка:**

```check
rules:
  - object_exists: "Player"
  - component_exists:
      object: "Player"
      type: "Rigidbody"
```

### Скрипт с методом

**Задание:**
> Создайте скрипт `HelloWorld.cs` с классом `HelloWorld` и методом `Start()`, который выводит сообщение в консоль через `Debug.Log`.

**Проверка:**

```check
rules:
  - filename: "HelloWorld.cs"
  - contains: "public class HelloWorld"
  - contains: "void Start()"
  - contains: "Debug.Log"
```

### Интерактивный объект

**Задание:**
> Настройте объект `Player` для обработки кликов:
> 1. Добавьте `Circle Collider2D`
> 2. На камере добавьте `Physics2DRaycaster`
> 3. В скрипте `ClickerGame.cs` реализуйте интерфейс `IPointerDownHandler`

**Проверка:**

```check
rules:
  - component_exists:
      object: "Player"
      type: "Collider2D"
  - component_exists:
      object: "Main Camera"
      type: "Physics2DRaycaster"
  
  - filename: "ClickerGame.cs"
  - contains: "using UnityEngine.EventSystems;"
  - contains: "IPointerDownHandler"
  - contains: "public void OnPointerDown(PointerEventData eventData)"
```

### UI с обновлением текста

**Задание:**
> Создайте UI текст и свяжите его со скриптом:
> 1. Добавьте на сцену Text - TextMeshPro
> 2. В скрипте `GameManager.cs` подключите `TMPro`
> 3. Создайте публичную переменную `TMP_Text scoreText`
> 4. Обновляйте текст в методе `Update()`

**Проверка:**

```check
rules:
  - filename: "GameManager.cs"
  - contains: "using TMPro;"
  - contains: "public TMP_Text scoreText;"
  - contains: "void Update()"
  - contains: "scoreText.text"
```

### Переменные

**Задание:**
> В скрипте `ClickerGame.cs`:
> 1. Объявите публичную переменную `score` типа `float`
> 2. Объявите публичную переменную `clickPower` типа `int`
> 3. В методе `OnPointerDown` увеличивайте `score` на `clickPower`

**Проверка:**

```check
rules:
  - filename: "ClickerGame.cs"
  - contains: "public float score"
  - contains: "public int clickPower"
  - contains: "OnPointerDown"
  - contains: "score += clickPower"
```

## Рекомендации

### Гибкость важнее точности

**Плохо (слишком строго):**

```check
rules:
  - filename: "PlayerController.cs"
  - contains: "Debug.Log(\"Игрок прыгнул с силой \" + jumpForce);"
```

**Хорошо (гибко):**

```check
rules:
  - filename: "PlayerController.cs"
  - contains: "Debug.Log"
  - contains: "jumpForce"
```

Важно проверить смысл, а не дословную строку.

### Проверяйте ключевое

- для скриптов: имя класса, ключевые методы, изученные конструкции;
- для сцены: объекты и обязательные компоненты;
- не проверяйте точную реализацию, если она не является целью.

### Проверяйте то, что учили

Не требуйте использования концептов, которые ещё не были изучены:

```check
rules:
  - filename: "PlayerMove.cs"
  # Хорошо: проверяем только изученное
  - contains: "void Update()"
  - contains: "transform.Translate"
  
  # Плохо: требуем то, что не объяснили
  # - contains: "Input.GetAxis"
  # - contains: "Time.deltaTime"
```

### Порядок правил

Сначала сцена, затем скрипты:

```check
rules:
  - object_exists: "Player"
  - component_exists:
      object: "Player"
      type: "Rigidbody"
  
  - filename: "PlayerController.cs"
  - contains: "public class PlayerController"
  - contains: "void Update()"
```

### Комментарии

Используйте комментарии для ясности:

```check
rules:
  # === Проверка игрового объекта ===
  - object_exists: "Player"
  - component_exists:
      object: "Player"
      type: "Rigidbody2D"
  
  # === Проверка скрипта движения ===
  - filename: "PlayerMove.cs"
  - contains: "void Update()"
  - contains: "transform.Translate"
```

### Типичные сценарии

#### Создание скрипта

**Минимальная проверка:**

```check
rules:
  - filename: "MyScript.cs"
  - contains: "public class MyScript"
```

**Расширенная проверка:**

```check
rules:
  - filename: "MyScript.cs"
  - contains: "public class MyScript"
  - contains: "void Start()"
  - contains: "Debug.Log"
```

#### Настройка объекта для физики

```check
rules:
  - object_exists: "Player"
  - component_exists:
      object: "Player"
      type: "Rigidbody2D"
  - component_exists:
      object: "Player"
      type: "BoxCollider2D"
```

#### Реализация интерфейса

```check
rules:
  - filename: "ClickHandler.cs"
  - contains: "using UnityEngine.EventSystems;"
  - contains: "IPointerDownHandler"
  - contains: "OnPointerDown"
```

#### Работа с UI

```check
rules:
  - filename: "UIManager.cs"
  - contains: "using TMPro;"
  - contains: "TMP_Text"
  - contains: ".text ="
```

#### Использование переменных

```check
rules:
  - filename: "GameManager.cs"
  - contains: "public int score"
  - contains: "score +="
```

## Отладка

Чтобы видеть подробности в консоли:

1. Откройте `Project Settings`
2. Найдите секцию `AlgoNeoCourse`
3. Включите `Validation → Debug Render Check Blocks`

Пример вывода:

```
=== Check Results ===
✓ object_exists: "Player"
✓ component_exists: "Player" has "Rigidbody"
✓ filename: "PlayerController.cs"
✓ contains: "public class PlayerController"
✗ contains: "void Update()"

Result: 4/5 passed
```

### Частые проблемы

#### Проверка не срабатывает

- Опечатка в имени объекта/компонента/файла
- Неправильные отступы в YAML
- Файл скрипта не сохранён
- Объект находится в другой сцене
- Использован неподдерживаемый ключ правила

Проверьте точные имена, YAML-структуру, сохранение файлов/сцены и список поддерживаемых правил.

#### `contains` не находит код

- Код написан с другими пробелами/переносами
- Регистр не совпадает
- Забыли указать `filename` перед `contains`

Используйте короткие фрагменты и всегда ставьте `filename` перед `contains`.

#### Компонент не находится

- Компонент не добавлен
- Неправильный тип: например, `Rigidbody2D` вместо `Rigidbody`
- Ошибка в имени объекта
- `component_exists` записан не многострочно

## Шаблоны для копирования

### Объект с компонентами

````check
rules:
  - object_exists: ""
  - component_exists:
      object: ""
      type: ""
````

### Скрипт

````check
rules:
  - filename: ".cs"
  - contains: "public class "
  - contains: ""
````

### Комбинированная проверка

````check
rules:
  - object_exists: ""
  - component_exists:
      object: ""
      type: ""
  
  - filename: ".cs"
  - contains: ""
````

## См. также

- [CourseTechGuide.md](../CourseTechGuide.md) — краткий обзор всех возможностей
- [LessonMethodGuide.md](../LessonMethodGuide.md) — правила создания заданий
- [Docs/Examples/check_block_scene.md](Examples/check_block_scene.md) — примеры проверок сцены
- [Docs/Examples/check_block_script.md](Examples/check_block_script.md) — примеры проверок скриптов

