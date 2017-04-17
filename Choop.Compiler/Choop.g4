grammar Choop;

/*
 * Parser Rules
 */

// Entry point
root
    : ( sprite
      | module
      | global_declaration
      )*
      EOF
    ;

sprite
    : Sprite_Tag
      Identifier
      sprite_body
    ;

module
    : Module_Tag
      Identifier
      sprite_body
    ;

sprite_body
    : Brace_Open
      ( global_declaration
      | void_declaration
      )*
      Brace_Close
    ;

global_declaration
    : ( const_declaration
      | var_global_declaration
      )
    ;

const_declaration
    : Decl_Const
      Identifier
      Assign
      constant
      Terminator
    ;

var_global_declaration
    : Decl_Var
      Identifier
      Assign
      constant
      Terminator
    ;

constant
    : ( Const_True
      | Const_False
      | StringLiteral
      | NumericLiteral
      )
    ;

void_declaration
    : Atomic_Tag?
      Void_Tag
      Identifier
      Bracket_Open
      ( ( Identifier
          Separator
        )*
        Identifier
      )?
      Bracket_Close
      method_body
    ;

method_body
    : Brace_Open
      ( scoped_declaration
      )*
      Brace_Close
    ;

scoped_declaration
    : ( const_declaration
      | var_global_declaration
      )
    ;

/*
 * Lexer Rules
 */

Const_True      : 'true';
Const_False     : 'false';

Op_Equals       : '==';
Op_GT           : '>';
Op_LT           : '<';
Op_GTE          : '>=';
Op_LTE          : '<=';
Op_And          : '&&';
Op_Or           : '||';
Op_Not          : '!';
Op_Concat       : '.';
Op_Plus	        : '+';
Op_Minus        : '-';
Op_Divide       : '/';
Op_Mult         : '*';
Op_Mod          : '%';
Op_Pow          : '^';
Op_LShift       : '<<';
Op_RShift       : '>>';

Bracket_Open    : '(';
Bracket_Close   : ')';
Brace_Open      : '{';
Brace_Close     : '}';
Square_Open	    : '[';
Square_Close    : ']';
Angle_Open      : '<';
Angle_Close     : '>';
Separator       : ',';
Terminator      : ';';

Decl_Const      : 'const';
Decl_Var        : 'var';
Decl_Array      : 'array';

Assign	        : '=';
Assign_Add	    : '+=';
Assign_Sub      : '-=';
Assign_Concat   : '.=';
Assing_Inc      : '++';
Assing_Dec      : '--';

Sprite_Tag      : 'sprite';
Module_Tag      : 'module';

Void_Tag        : 'void';
Function_Tag    : 'function';
Event_Tag       : 'event';
Atomic_Tag      : 'atomic';
Inline_Tag      : 'inline';

If_Tag          : 'if';
Else_Tag        : 'else';
ElseIf_Tag      : 'else if';

Switch_Tag      : 'switch';
Case_Tag        : 'case';
Default_Tag     : 'default';

For_Tag         : 'for';
While_Tag       : 'while';
Repeat_Tag      : 'repeat';

Return_Tag      : 'return';

Identifier
    : Letter
      ( Letter
      | Digit
      )*
    ;

NumericLiteral
    : ( HexNumber
      | SciNumber
      | Decimal
      | Integer
      )
    ;

fragment
HexNumber
    : ( Op_Minus
      )?
      '0'
      [xX]
      HexDigit+
    ;

fragment
SciNumber
    : ( Decimal
      | Integer)
      'e'
      Integer
    ;

fragment
Decimal
    : ( Op_Minus
      )?
      UInteger
      '.'
      UInteger
    ;

fragment
Integer
    : ( Op_Minus
      )?
      UInteger
    ;

fragment
UInteger
    : ( Digit
      )+
    ;

StringLiteral
    : ( '"'
        CharSequence
        '"'
      | '\''
        CharSequence
        '\''
      )
    ;

fragment
CharSequence
    : ( PrintableChar
      )+
    ;

fragment
PrintableChar
    : [ -~]
    ;

BlockComment
    : '/*' .*? '*/'
      -> channel(HIDDEN)
    ;

LineComment
    : '//' ~[\r\n]*
      -> channel(HIDDEN)
    ;

fragment
Letter
    : [a-zA-Z_]
    ;

fragment
Digit
    : [0-9]
    ;

fragment
HexDigit
    : [0-9a-fA-F]
    ;

WS
    : [ \t\r\n]+
      -> skip
    ;