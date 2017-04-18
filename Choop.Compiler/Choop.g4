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
    : attribute*
      Sprite_Tag
      Identifier
      sprite_body
    ;

module
    : Module_Tag
      Identifier
      sprite_body
    ;

attribute
    : Square_Open
      ( ( Attr_Resources
        | Attr_Location
        | Attr_Size
        | Attr_Rotation
        | Attr_Visible
        | Attr_Costume
        | Attr_RotStyle
        | Attr_Draggable
        )
        Bracket_Open
        ( constant
          Separator
        )?
        constant
        Bracket_Close
      | Attr_Import
        Bracket_Open
        Identifier
        Bracket_Close
      )
      Square_Close
    ;

sprite_body
    : Brace_Open
      ( global_declaration
      | void_declaration
      | function_declaration
      | event_handler
      )*
      Brace_Close
    ;

global_declaration
    : ( const_declaration
      | var_global_declaration
      | array_global_declaration
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
      ( Assign
        constant
      )?
      Terminator
    ;

array_global_declaration
    : Decl_Array
      Square_Open
      UInteger
      Square_Close
      Identifier
      ( Assign
        Brace_Open
        ( constant
          Separator
        )*
        constant
        Brace_Close
      )?
      Terminator
    ;

constant
    : ( Const_True
      | Const_False
      | StringLiteral
      | HexNumber
      | SciNumber
      | Decimal
      | NegInteger
      | UInteger
      )
    ;

void_declaration
    : Atomic_Tag?
      Void_Tag
      Identifier
      parameter_set
      scope_body
    ;

function_declaration
    : Atomic_Tag?
      Function_Tag
      Identifier
      parameter_set
      scope_body
    ;

event_handler
    : Event_Tag
      Identifier
      ( Op_LT
        constant
        Op_GT
      )?
      Bracket_Open
      Bracket_Close
      scope_body
    ;

parameter_set
    : Bracket_Open
      ( parameter
        Separator
      )*
      ( ( optional_parameter
          Separator
        )*
        optional_parameter
      | parameter
      )?
      Bracket_Close
    ;

optional_parameter
    : parameter
      Assign
      constant
    ;

parameter
    : Identifier
    ;

scope_body
    : Brace_Open
      ( statement
      )*
      Brace_Close
    ;

statement
    : ( scoped_declaration
      | method_call
        Terminator
      | assignment
      | return_stmt
      | if_stmt
      | switch_stmt
      | scope_body
      )
    ;


scoped_declaration
    : ( const_declaration
      | var_declaration
      | array_declaration
      )
    ;

var_declaration
    : Decl_Var
      Identifier
      ( Assign
        primary_expression
      )?
      Terminator
    ;

array_declaration
    : Decl_Array
      Square_Open
      UInteger
      Square_Close
      Identifier
      ( Assign
        Brace_Open
        ( primary_expression
          Separator
        )*
        primary_expression
        Brace_Close
      )?
      Terminator
    ;



method_call
    : Identifier
      Bracket_Open
      ( ( primary_expression
          Separator
        )*
        primary_expression
      )?
      Bracket_Close
    ;

assignment
    : Identifier
      ( Square_Open
        primary_expression
        Square_Close
      )?
      ( ( Assign
        | Assign_Add
        | Assign_Sub
        | Assign_Concat
        )
        primary_expression
      | ( Assign_Inc
        | Assign_Dec
        )
      | Assign

      )
      Terminator
    ;

return_stmt
    : Return_Tag
      primary_expression?
      Terminator
    ;

if_stmt
    : If_Tag
      Bracket_Open
      primary_expression
      Bracket_Close
      ( scope_body
        ( ElseIf_Tag
          Bracket_Open
          primary_expression
          Bracket_Close
          scope_body
        )*
        ( Else_Tag
          scope_body
        )?
      | statement
      )
    ;

switch_stmt
    : Switch_Tag
      Bracket_Open
      primary_expression
      Bracket_Close
      Brace_Open
      ( Case_Tag
        ( constant
          Separator
        )*
        constant
        Colon
        ( statement
        )+
      )+
      ( Default_Tag
        Colon
        ( statement
        )+
      )
      Brace_Close
    ;

primary_expression
    : ( constant
      | method_call
      | Identifier
      )
    ;

/*
 * Lexer Rules
 */

Const_True      : 'true';
Const_False     : 'false';

Op_Concat       : '.';
Op_Plus	        : '+';
Op_Minus        : '-';
Op_Divide       : '/';
Op_Mult         : '*';
Op_Mod          : '%';
Op_Pow          : '^';
Op_LShift       : '<<';
Op_RShift       : '>>';
Op_Equals       : '==';
Op_GTE          : '>=';
Op_LTE          : '<=';
Op_GT           : '>';
Op_LT           : '<';
Op_And          : '&&';
Op_Or           : '||';
Op_Not          : '!';

Bracket_Open    : '(';
Bracket_Close   : ')';
Brace_Open      : '{';
Brace_Close     : '}';
Square_Open	    : '[';
Square_Close    : ']';
Separator       : ',';
Terminator      : ';';
Colon           : ':';

Decl_Const      : 'const';
Decl_Var        : 'var';
Decl_Array      : 'array';
Decl_List       : 'list';

Assign	        : '=';
Assign_Add	    : '+=';
Assign_Sub      : '-=';
Assign_Concat   : '.=';
Assign_Inc      : '++';
Assign_Dec      : '--';

Sprite_Tag      : 'sprite';
Module_Tag      : 'module';

Attr_Resources  : 'ResourcesFile';
Attr_Import     : 'Import';
Attr_Location   : 'Location';
Attr_Size       : 'Size';
Attr_Rotation   : 'Rotation';
Attr_Visible    : 'Visible';
Attr_Costume    : 'Costume';
Attr_RotStyle   : 'RotationStyle';
Attr_Draggable  : 'Draggable';

Void_Tag        : 'void';
Function_Tag    : 'function';
Event_Tag       : 'event';
Atomic_Tag      : 'atomic';
Inline_Tag      : 'inline';

ElseIf_Tag      : 'else if';
If_Tag          : 'if';
Else_Tag        : 'else';

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

HexNumber
    : ( Op_Minus
      )?
      '0'
      [xX]
      HexDigit+
    ;

SciNumber
    : ( Decimal
      | UInteger
      | NegInteger
      )
      'e'
      ( UInteger
      | NegInteger
      )
    ;

Decimal
    : ( Op_Minus
      )?
      UInteger
      '.'
      UInteger
    ;

NegInteger
    : Op_Minus
      UInteger
    ;

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
    : [ !#-&(-~]
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