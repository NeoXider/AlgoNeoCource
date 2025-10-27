# Справочник по проверкам (Check Reference)

Полное руководство по автоматическим проверкам выполнения заданий студентами.

## Содержание

- [Основы](#основы)
- [Синтаксис](#синтаксис)
- [Типы правил](#типы-правил)
- [Комбинированные проверки](#комбинированные-проверки)
- [Примеры](#примеры)
- [Рекомендации](#рекомендации)
- [Отладка](#отладка)

## Основы

Блоки `check` позволяют автоматически проверять, выполнил ли студент задание в Unity. Проверки могут включать:

- Наличие объектов на сцене
- Наличие компонентов на объектах
- Существование файлов скриптов
- Содержимое кода в скриптах

### Базовый синтаксис

````md
```check
rules:
  - object_exists: "Player"
  - component_exists:
      object: "Player"
      type: "Rigidbody"
```
````

### Как это работает

1. Студент видит задание на слайде
2. Выполняет задачу в Unity (создаёт объект, пишет код и т.д.)
3. Нажимает кнопку "Проверить" на слайде
4. Плагин выполняет все правила из блока `check`
5. Результат отображается под кнопкой и в консоли Unity

## Синтаксис

### Структура блока

````check
# Опционально: тип проверки (обычно определяется автоматически)
type: scene | script | combined

# Обязательно: список правил
rules:
  - правило1
  - правило2
  - правило3
````

**Примечания:**
- Поле `type` обычно не требуется — система автоматически определяет тип по набору правил
- **`rules:` всегда начинается с новой строки**
- **Каждое правило начинается с `-` (дефис + пробел)** — это YAML-список
- Отступы делаются **пробелами** (не табами), стандартно 2 пробела

### YAML синтаксис

Проверки используют YAML-подобный синтаксис. **Важно:**

- **`rules:`** — ключевое слово, после которого идёт список правил
- Каждое правило начинается с **`-`** (дефис + пробел)
- Отступы делаются **пробелами** (не табами)
- Стандартный отступ — 2 пробела
- Вложенные параметры пишутся с дополнительным отступом

**Пример правильного форматирования:**
````yaml
rules:
  - object_exists: "Player"
  - component_exists:
      object: "Player"
      type: "Rigidbody"
  - filename: "Script.cs"
  - contains: "код"
````

## Типы правил

### 1. object_exists — Проверка наличия объекта

Проверяет, существует ли объект с указанным именем на сцене.

**Синтаксис (в блоке `check`):**

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

**Особенности:**
- Проверяет **точное имя** объекта
- Регистр имеет значение: `"Player"` ≠ `"player"`
- Ищет на **активной сцене**
- Объект может быть неактивным — проверка всё равно пройдёт

### 2. component_exists — Проверка наличия компонента

Проверяет, есть ли указанный компонент на объекте.

**Синтаксис (в блоке `check`):**

```yaml
rules:
  - component_exists:
      object: "ИмяОбъекта"
      type: "ИмяКомпонента"
```

**Примеры:**

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

**Особенности:**
- `object` — точное имя объекта на сцене
- `type` — имя типа компонента (без namespace)
- Работает со встроенными компонентами Unity и пользовательскими скриптами
- Для 2D: `"Rigidbody2D"`, `"BoxCollider2D"` и т.д.

**Распространённые компоненты:**

| 3D | 2D | UI | Other |
|----|----|----|-------|
| Rigidbody | Rigidbody2D | Canvas | AudioSource |
| BoxCollider | BoxCollider2D | Image | Camera |
| SphereCollider | CircleCollider2D | Button | Light |
| MeshRenderer | SpriteRenderer | Text | Animator |

### 3. filename — Проверка наличия файла

Проверяет, существует ли файл с указанным именем в проекте.

**Синтаксис (в блоке `check`):**

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

**Особенности:**
- Ищет файл **рекурсивно** во всех папках проекта
- Указывайте **полное имя** с расширением
- Регистр имеет значение
- Обычно используется перед `contains` для проверки кода

### 4. contains — Проверка содержимого файла

Проверяет, содержит ли указанный файл определённую строку кода.

**Синтаксис (в блоке `check`):**

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

**Особенности:**
- Требует предварительного указания `filename`
- Ищет **подстроку** (не точное совпадение)
- Регистр имеет значение
- Игнорирует пробелы в начале и конце строки
- **Последний `filename`** определяет, в каком файле искать

**Важно:** `contains` применяется к последнему указанному `filename`:

````check
rules:
  - filename: "Script1.cs"
  - contains: "text1"  # ищет в Script1.cs
  
  - filename: "Script2.cs"
  - contains: "text2"  # ищет в Script2.cs
  - contains: "text3"  # тоже ищет в Script2.cs
````

## Комбинированные проверки

Можно комбинировать проверки сцены и скриптов в одном блоке.

### Пример 1: Объект + компонент + скрипт

```check
rules:
  # Проверка сцены
  - object_exists: "Player"
  - component_exists:
      object: "Player"
      type: "Rigidbody"
  - component_exists:
      object: "Player"
      type: "BoxCollider"
  
  # Проверка скрипта
  - filename: "PlayerController.cs"
  - contains: "public class PlayerController"
  - contains: "void Start()"
  - contains: "void Update()"
```

### Пример 2: Несколько объектов и скриптов

```check
rules:
  # Объекты
  - object_exists: "Player"
  - object_exists: "Enemy"
  
  # Компоненты Player
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
  
  # Скрипты
  - filename: "PlayerController.cs"
  - contains: "public class PlayerController"
  
  - filename: "EnemyAI.cs"
  - contains: "public class EnemyAI"
  - contains: "void Update()"
```

### Пример 3: UI элементы

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

### Пример 1: Создание игрового объекта

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

### Пример 2: Написание скрипта с методом

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

### Пример 3: Интерактивный объект (кликер)

**Задание:**
> Настройте объект `Player` для обработки кликов:
> 1. Добавьте `Circle Collider2D`
> 2. На камере добавьте `Physics2DRaycaster`
> 3. В скрипте `ClickerGame.cs` реализуйте интерфейс `IPointerDownHandler`

**Проверка:**

```check
rules:
  # Проверка сцены
  - component_exists:
      object: "Player"
      type: "Collider2D"
  - component_exists:
      object: "Main Camera"
      type: "Physics2DRaycaster"
  
  # Проверка кода
  - filename: "ClickerGame.cs"
  - contains: "using UnityEngine.EventSystems;"
  - contains: "IPointerDownHandler"
  - contains: "public void OnPointerDown(PointerEventData eventData)"
```

### Пример 4: UI с обновлением текста

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

### Пример 5: Использование переменных

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

### Принципы создания проверок

#### 1. Гибкость важнее точности

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

Студент может написать текст немного по-другому — важно, что он использует `Debug.Log` и переменную `jumpForce`.

#### 2. Проверяйте ключевые элементы

**Для скриптов проверяйте:**
- ✅ Имя класса
- ✅ Наличие ключевых методов
- ✅ Использование требуемых переменных/конструкций
- ❌ Точную реализацию (если не критично)

**Для сцены проверяйте:**
- ✅ Наличие объектов
- ✅ Наличие обязательных компонентов
- ❌ Параметры компонентов (если не критично)

#### 3. Проверяйте то, что учили

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

#### 4. Порядок правил

Логичный порядок облегчает понимание:

```check
rules:
  # Сначала сцена
  - object_exists: "Player"
  - component_exists:
      object: "Player"
      type: "Rigidbody"
  
  # Потом скрипты
  - filename: "PlayerController.cs"
  - contains: "public class PlayerController"
  - contains: "void Update()"
```

#### 5. Комментарии в блоках

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

#### Сценарий: Создание скрипта

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

#### Сценарий: Настройка объекта для физики

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

#### Сценарий: Реализация интерфейса

```check
rules:
  - filename: "ClickHandler.cs"
  - contains: "using UnityEngine.EventSystems;"
  - contains: "IPointerDownHandler"
  - contains: "OnPointerDown"
```

#### Сценарий: Работа с UI

```check
rules:
  - filename: "UIManager.cs"
  - contains: "using TMPro;"
  - contains: "TMP_Text"
  - contains: ".text ="
```

#### Сценарий: Использование переменных

```check
rules:
  - filename: "GameManager.cs"
  - contains: "public int score"
  - contains: "score +="
```

## Отладка

### Включение Debug-режима

Для просмотра результатов проверок в консоли:

1. Откройте `Project Settings`
2. Найдите секцию `AlgoNeoCourse`
3. Включите `Validation → Debug Render Check Blocks`

### Формат вывода в консоль

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

#### Проблема: Проверка не срабатывает

**Причины:**
- Опечатка в имени объекта/компонента/файла
- Неправильные отступы в YAML
- Файл скрипта не сохранён
- Объект находится в другой сцене

**Решение:**
- Проверьте точные имена (регистр важен!)
- Проверьте структуру YAML
- Сохраните все файлы и сцену
- Убедитесь, что объект на активной сцене

#### Проблема: `contains` не находит код

**Причины:**
- Код написан с другими пробелами/переносами
- Регистр не совпадает
- Забыли указать `filename` перед `contains`

**Решение:**
- Используйте короткие фрагменты для поиска
- Проверьте регистр
- Всегда указывайте `filename` перед `contains`

#### Проблема: Компонент не находится

**Причины:**
- Неправильное имя типа (например, `"Rigidbody2D"` вместо `"Rigidbody"`)
- Компонент не добавлен
- Объект неактивен (но это не должно мешать)

**Решение:**
- Уточните точный тип компонента (2D vs 3D)
- Проверьте наличие компонента в Inspector
- Убедитесь, что объект существует

## Шаблоны для копирования

### Шаблон: Проверка объекта с компонентами

````check
rules:
  - object_exists: ""
  - component_exists:
      object: ""
      type: ""
````

### Шаблон: Проверка скрипта

````check
rules:
  - filename: ".cs"
  - contains: "public class "
  - contains: ""
````

### Шаблон: Комбинированная проверка

````check
rules:
  # Сцена
  - object_exists: ""
  - component_exists:
      object: ""
      type: ""
  
  # Скрипт
  - filename: ".cs"
  - contains: ""
````

## См. также

- [CourseGuide.md](../CourseGuide.md) — краткий обзор всех возможностей
- [CourseStyleGuide.md](../CourseStyleGuide.md) — правила создания заданий
- [Docs/Examples/check_block_scene.md](Examples/check_block_scene.md) — примеры проверок сцены
- [Docs/Examples/check_block_script.md](Examples/check_block_script.md) — примеры проверок скриптов

