# Choop
*A text-based programming language that compiles down to Scratch SB2*

Inspired by [ApuC](https://github.com/MegaApuTurkUltra/Scratch-ApuC)
by [@MegaApuTurkUltra](https://scratch.mit.edu/users/MegaApuTurkUltra/).

## Features
- Scoped variables and arrays
- Full recursion support
- Compile-time type support
- Complete thread safety
- Modules, so different sprites can have common functionality
- Inline voids and repeat loops
- Optional parameters
- Constants
- Unicode escaping within strings

## IDE
Once the Choop compiler has been completed, an editor will
be made for Choop.

## Syntax
You can see the documentation for the syntax of
Choop at [Syntax.md](Syntax.md).
To get a feel for the language, check out the
[sample projects](Choop.Demo/Samples/)
You can also view the Antlr grammar file for Choop
[here](Choop.Compiler/Choop.g4).
Eventually, a table of inbuilt functions will be produced.

## Using Choop
Whilst Choop can be used to create any type of Scratch
project, I have provided these recommendations of what
should be made in Choop so you can easily see whether it
is right for you.

Of course, there are exceptions to every rule and this
guidance should not be taken as gospel.

### What kinds of projects should be made in Choop:
- Large, resource intensive projects
- Mathematical projects
- Pen-based projects
- Engines
- Projects where tracking changes / version control may be beneficial
- Projects which would benefit from recursion and custom functions

### What kinds of projects should not be made in Choop:
- Animations
- Tutorials
- Art projects

## Under development
Currently, significant functionality is missing from
from the Choop compiler.

## External links
- [Scratch website](https://scratch.mit.edu/)
- [SB2 file format](https://wiki.scratch.mit.edu/wiki/Scratch_File_Format_(2.0))

Scratch is a project of the Lifelong Kindergarten Group at the MIT Media Lab.