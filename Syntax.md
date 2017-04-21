# Foreword
In this document, I'll assume that you are already familiar with programming
lanugages beyond Scratch.

In the Choop syntax, I've tried to keep it similar as possible to existing
languages, whilst still bearing in mind that this compiles down to Scratch
code.

Please be aware that the Choop grammar is **case sensitive**, whilst
string comparisons in Choop are **case insensitive**.

For any additional information, you can view the
[Choop grammar file](Choop.Compiler/Choop.g4) or
[contact me](https://scratch.mit.edu/users/chooper100/).

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

## Type Conversions
For information on how to convert types, please see
[Type Conversions Using Operators](#type-conversions-using-operators).

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

# Lists
Lists store groups of values. They have an initial size,
but values can later be added to or removed from them
at runtime.

Like arrays, you can also specify the inital items in
the list.

**Examples:**
```C#
list[] MyEmptyList; // Has no items initially
list[4] MyList; // Has 4 items initially

list<num>[3] MyNumList; // A list of 3 numbers
list<bool>[2] MyBoolList; // A list of 2 booleans

// These lists specify their initial items:
list[3] MyFilledList = {12, "foo", true};
list<string>[2] MyStringList = {'bar', 'bazz'};
```

Again, when specifying the initial items in a list, the
number of items given must match the number in the
square brackets.

## List Usage
To get and set items in a list, you can use the same
syntax as arrays:

```C#
// ...

// Create list
list<string>[3] MyList = {'foo', 'bar', 'bazz'};

// Get items
var First = MyList[0]; // = 'foo'
var Second = MyList[1]; // = 'bar'
var Third = MyList[2]; // = 'bazz'

// Set items
MyList[0] = 'hello'; // Sets the value of the first item to 'hello'
MyArray[3] = 'boo'; // Sets the value of the last item to 'boo'

// ...
```

### Adding and Removing Items
TODO

### Getting the List Length
TODO

# Operators
Choop supports several different operators.
Each operator will accept any type of data, but will
always output data of a certain type.

You can use multiple operators in a single statement
without the use of brackets. However, as Choop
follows order of operations (BIDMAS) first, then
left-to-right order, you may wish to use brackets
to indicate how some maths should be evaluated.

**Example:**
```C#
var foo = 4 - 3 * 2 + 1;
// Equivalent to:
var bar = 4 - (3 * 2) + 1;
```

**Full list of operators, in order of precedence:**

Operator | Example | Output type | Description
-------- | ------- | ----------- | -----------
`^` | `3 ^ 2` | `num` | 3 to the power of 2 (= 9)
`*` | `4 * 2` | `num` | 4 times 2 (= 8)
`/` | `10 / 5` |`num` | 10 divided by 5 (= 2)
`%` | `13 % 3` | `num` | 13 modulo 3 (= 1)
`.` | `'foo'.'bar'` | `string` | 'foo' and 'bar' (= 'foobar')
`+` | `3 + 5` | `num` | 3 plus 5 (= 8)
`-` | `9 - 4` | `num` | 9 minus 4 (= 5)
`<<` | `8 << 2` | `num` | Shift 8 left by 2 bits (= 32)
`>>` | `8 >> 2` | `num` | Shift 8 right by 2 bits (= 2)
`<` | `3 < 4` | `bool` | Is 3 less than 4 (= true)
`>` | `3 > 4` | `bool` | Is 3 greater than 4 (= false)
`<=` | `5 <= 5` | `bool` | Is 5 less than or equal to 5 (= true)
`>=` | `8 >= 4` | `bool` | Is 8 greater than or equal to 4 (= true)
`==` | `"bar" == "bar"` | `bool` | Is "bar" equal to "bar" (= true)
`!=` | `"bar" != "bar"` | `bool` | Is "bar" not equal to "bar" (= false)
`&&` | `false && true` | `bool` | AND: Are both inputs true (= false)
`\|\|` | `false \|\| true` | `bool` | OR: Are any of the inputs true (= true)

## Unary Operators
These only require 1 input

There are only 2 unary operators currently:

```C#
num foo = 3;

num bar = -foo; // Makes foo negative (= -3)

bool bam = false;

bool baz = !bam; // NOT: Inverts bam (= true)
```

## Type Conversions Using Operators
When using typed variables in Choop, the compiler
will raise an error if the type of the variable
you are setting does not match the type of the data.

For example, this will not work:
```C#
num foo = 23;
string bar = foo;
```
This is because foo is a number but bar is a string.

To change the type of something, we can use operators:
```C#
num foo = 23;
string bar = foo.'';
```
Here, we concatenate an empty string onto foo. The
concatenation operator will always output a string
value, therefore this will work as the types now
match.

Here's how you can convert other types:
```C#
num NumToConvert = 1;
string StringToConvert = "12";
bool BoolToConvert = true;

num Num1 = StringToConvert + 0; // = 12
num Num2 = BoolToConvert + 0; // = 1

string String1 = NumToConvert.''; // = "1"
string String2 = BoolToConvert.''; // = "true"

bool Bool1 = NumToConvert==1; // = true
bool Bool2 = StringToConvert=="true"; // = false
```

**Note:** If a value cannot easily be converted into
a different type (eg. "foo" cannot be converted into
a number), the default value for that type will be
used (which in this case would be 0).

# Selection
Selection is basically if and switch statements.

## If Statements
This is how if statements work in Choop:

```C#
var foo = 13;

var bar;
if (foo == 2) {
    // This code runs if foo is 2

    bar = 1;
} else if (foo > 10) {
    // This code runs if foo is greater than 10

    bar = 2;
} else if (foo < 4) {
    // This code runs if foo is less than 4,
    // but not if foo is 2 because we already
    // tested that

    bar = 3;
} else {
    // This code runs if none of the previous
    // conditions were met

    // In this case, this would occur if foo
    // was between 4 and 10 (inclusive)

    bar = 4;
}
```

You can have as many `else if` blocks as you want, or
none at all.

The `else` block is also optional, but it must go at
the end if you do use it.

The simplest type of if statement is just:
```C#
var foo = 2;
var bar;

if (foo == 4) {
    // This code runs when foo equals 4

    bar = 1;
}
```

## Switch statements
Switch statements follow the usual syntax of
other languages.

Please be aware that 'falling through' is
not supported and therefore each non-empty
case must end with either a return or break
statement.

**Example:**
```C#
var foo = 3;
var bar;

switch(foo) { // We will compare foo
    case 1:
        // This code runs when foo = 1
        
        bar = 1;

        break; // Indicates the end of this block
    case "bizz":
        // This code runs when foo = "bizz"

        bar = 2;

        break;
    case 3:
    case 4:
        // This code runs when foo = 3 or foo = 4
        
        bar = 3;

        break;
    default:
        // The default statement is optional
        // It is ran if none of the above
        // criteria are met

        bar = 4;

        break;
}
```

As an alternative to `break` statements, you can
also use `return` statements, which are covered
later in this document.

# Repetion
There are 5 kinds of loop in Choop, and each have
their own uses.

Outside of atomic scripts, loops that cause screen
refreshes will be rate limited to 30 FPS. Inline
loops, or loops inside atomic scripts, run as fast as
possible however.

## Repeat Loops
These are the most common type of loop. They run
code for a set number of repeats:

```C#
var foo = 0;
var bar = 6;

repeat (bar) {
    // This code will run 6 times
    foo++;
}
```

If a constant term (eg. 6) is used inside the brackets,
you can also add the inline modifier. This causes the
compiler to replace the loop with the code physically
repeated for the specified number of times. This
avoids any loop delay (particularly when outside of
atomic mode).

```C#
var foo = 0;

inline repeat (6) {
    // This code will run 6 times
    foo++;
}
```

## Forever Loops
These are loops that, once started, will run until
the script is externally stopped - eg. by stopping
the entire project.

**Syntax:**
```C#
var foo = 0;

forever {
    // This code will run until the project is stopped

    foo++;
}
```

## While Loops
These loops will run until a given condition becomes
no longer true.

```C#
var foo = 6;

while (foo > 1) {
    // This code will be repeated until foo
    // is less than or equal to 1

    foo--;
}
```

Note that a while loop like this is equivalent to a
forever loop:

```C#
var foo = 0;

while (true) {
    // This code will run forever

    foo++;
}
```

Wheras, the code in this loop will never be ran:
```C#
var foo = 0;

while (false) {
    // This code will never be run

    foo++;
}
```

## For Loops
For loops are a special type of while loop. They
have a scoped counter variable which is either
increased or decreased each loop until a certain
value is reached.

The general syntax for them is:
```C#
for (declaration; stopping condition; step) {
    // Code to repeat
}
```

An actual usage might look like this:
```C#
array[10] items;

for (var i = 0; i < 10; i++) {
    items[i] = 10;
}
```

In the above example, i starts off as being 0 in
the first loop, and then gets incremented each
loop, until i = 10, at which point the loop stops.

## Foreach loops
Foreach loops are used when you want to read each
item in a list, but do not need the index of the
item or need to set the values.

**Example:**
```C#
array[3] values = {5, 7, 3};
var total = 0;

foreach (var item in values) {
    total += item;
}
```

You can also use a type specifier instead of var:
```C#
num[3] values = {5, 7, 3};
num total = 0;

foreach (num item in values) {
    total += item;
}
```

**Note:** The loop variable is read-only.
Therfore, the following example will not compile:

```C#
array[3] values = {5, 3, 7};

foreach (var item in values) {
    item = 0; // Error: 'item' is readonly
}
```

Instead, a for loop should be used for that situation.