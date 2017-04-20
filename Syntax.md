# Foreword
In this document, I'll assume that you are already familiar with programming
lanugages beyond Scratch.

In the Choop syntax, I've tried to keep it similar as possible to existing
languages, whilst still bearing in mind that this compiles down to Scratch
code.

Please be aware that the Choop grammar is **case sensitive**, whilst
string comparisons in Choop are **case insensitive**.

# Comments
Comments can be either single or multi-line.
They use the following syntax:

```JS
// This is a single line comment

/* This is a comment that spans
   multiple lines */

var num; // Comments can also be inline, like so
```

# Sprites
To declare a sprite, you use the following syntax:

```C#
[ResourcesFile("SpriteName.res")]
sprite SpriteName {
    // Sprite code goes in here
}
```

Where:
- **SpriteName** is the name of your sprite
- **SpriteName.res** is the file that specifies all the costumes and sounds
  used by the sprite

## Sprite Attributes
In the example above, ResourcesFile was an attribute of the sprite.
There are 8 other attributes you can use, and they are all optional:

Attribute | Parameters | Default | Meaning
--------- | ---------- | ------- | -------
Import | Module name | N/A | Import the specified module code into this sprite
Location | X, Y | `0, 0` | The intial location of the sprite
Size | Size | `100` | The initial size of the sprite
Rotation | Angle (0 - 360) | `90` | The initial direction of the sprite
Visible | Visible | `true` | Whether the sprite is initially visible
Costume | Costume ID / Name | `1` | The initial costume of the sprite
RotationStyle | `"normal"` \| `"leftRight"` \| `"none"` | `"normal"` | The initial rotation style of the sprite
Draggable | Draggable | `false` | Whether the sprite is draggable in the player

**Note:** It is possible to have multiple import attributes to import more
than 1 module.

**Examples:**
```C#
[ResourcesFile("SpriteName.res")]
[Import(MyModule)]
[Location(10, -10)]
[Size(100)]
[Rotation(90)]
[Visible(true)]
[Costume(1)]
[RotationStyle("normal")]
[Draggable(false)]
sprite SpriteName {
    // Sprite code goes in here
}
```

# Modules
One of the key principals of Choop is modular design.

Modules are independent units of code that can be bolted onto any sprite,
and can be used to provide common functionality across sprites.

The syntax to declare modules looks like this:

```C#
module ModuleName {
    // Module code goes in here
}
```

Where:
- **ModuleName** is the name of your module

Note that the code inside as a module is the same as in a sprite - variables,
events and methods are all supported.

To import a module into a sprite, use the `Import` attribute on the sprite, as
shown above. Please be aware however that modules themselves cannot import
other modules.

Common use cases for modules might include:
- String manipulation functions
- Rendering methods (eg. draw triangle)

# Data Types
Whilst you can allow a variable in Choop to store any type of data, it is
possible to specify the types of data that a variable can store.

The fundamental data types supported by Choop are:

Type | Description
--- | ---
`num` | A number
`string` | Text
`bool` | Boolean value (true/false)

# Numeric Literals
Choop allows 3 different ways to specify numbers:

**Standard:**
`23`, `-4`, `13.67`

**Scientific:**
`1.7e2`, `4e-5`, `40e2`

**Hexadecimal:**
`Ox3E`, `-0xffa4`, `0X30`

# String Literals
Strings can either be enclosed in double or single quotes:

eg. `"hello world"` or `'hello world'`

Certain special characters require escape codes however:

Escape code | Character
--- | ---
`\"` | "
`\'` | '
`\n` | Newline
`\\` | \

**Example:**
`"hello\nworld"`

Both single and double quoted strings use the same escape codes.

# Boolean Literals
Boolean values are as simple as it gets:
- `true`
- `false`

That's it, nothing else is needed.

# Constants
Constants are read-only variables that are set at compile time.

When you compile your code, the compiler will replace each usage of the
constant with it's actual value, so there is no performance impact.

**Examples:**
```C#
const MyGenericConst = "foo";
const num MyNumber = 2.5;
const string MyString = 'bar';
const bool MyBool = false;
const num Pi = 3.1416;
```

# Variables
Variables can be both read and set at runtime.

**Examples:**
```C#
var MyGenericVar = 23;
var MyOtherVar;
num SomeNumber = 0xF0;
string MyString = 'Bizz';
string Name;
bool IsVisible = true;
```

When you don't specifiy an initial value for a var,
one of the default values are used:

Type | Default value
--- | ---
generic (var) | `""`
num | `0`
string | `""`
bool | `false`

## Variable Usage
You can get the value of variables simply by typing
the name of the variable:

```C#
// ...

var foo = 10;
var bar = foo + 2; // = 12

// ...
```

For assignments, there are more options available:

```C#
// ...

num foo = 12;

foo = 4; // Sets foo to 4
foo++; // Increments foo by 1 (= 5)
foo--; // Decrements foo by -1 (= 4)
foo += 2; // Adds 2 to foo (= 6)
foo -= 4; // Substracts 4 from foo (= 2)

string bar = "bam";

bar = "hello" // Sets bar to hello
bar .= " world" // Concatenates " world" to bar (= "hello world")

// ...
```

# Arrays
Arrays are lists of a fixed length.

The syntax for declaring arrays is as follows, where
the number inside the square brackets is the amount of
items in the array:
```C#
array[4] MyArray;
num[3] SomeNums;
string[5] Strings;
```

You can also specify the starting elements in the
array:
```C#
array[4] MyArray = {'foo', -23.2, true, "bar"};
num[3] SomeNumbers = {3, 4e-2, 0xE4};
string[5] Strings = {"foo\nbar", "baz", "qux", "bizz", "boo"};
```

Note that the number of elements given must match the
number in the square brackets.

## Array Usage
Array indexes are 0-based.

You can get and assign values like so:
```C#
// ...

// Create array
array[4] MyArray = {'foo', -23.2, true, "bar"};

// Get items
var First = MyArray[0]; // = 'foo'
var Second = MyArray[1]; // = -23.2
var Third = MyArray[2]; // = true
var Fourth = MyArray[3]; // = "bar"

// Set items
MyArray[0] = 12; // Sets the value of the first item to 12
MyArray[3] = "baz"; // Sets the value of the last item to "baz"

// ...
```

**Important:** Arrays *do not* have bounds checking.

Therefore, if we did the following in the above example,
we would get unexpected results:

```C#
// ...

var Item = MyArray[-1]; // = ????
var AnotherItem = MyArray[5]; // = ????

MyArray[-1] = "test"; // What did we set here?
MyArray[5] = "value"; // This could seriously disrupt data flow in our app

// ...
```