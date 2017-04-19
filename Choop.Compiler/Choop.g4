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
      | list_global_declaration
      )
    ;

const_declaration
    : Decl_Const
      type_specifier?
      Identifier
      Assign
      constant
      Terminator
    ;

type_specifier
    : Type_Num
    | Type_String
    | Type_Bool
    ;

var_global_declaration
    : ( Decl_Var
      | type_specifier
      )
      Identifier
      ( Assign
        constant
      )?
      Terminator
    ;

array_global_declaration
    : ( Decl_Array
      | type_specifier
      )
      Square_Open
      UInteger
      Square_Close
      Identifier
      ( Assign
        array_constant
      )?
      Terminator
    ;

list_global_declaration
    : Decl_List
      ( Op_LT
        type_specifier
        Op_GT
      )?
      Square_Open
      UInteger?
      Square_Close
      Identifier
      ( Assign
        array_constant
      )?
      Terminator
    ;

array_constant
    : Brace_Open
      ( constant
        Separator
      )*
      constant
      Brace_Close
    ;

constant
    : ( Const_True
      | Const_False
      | StringLiteral
      | Op_Minus?
        HexNumber
      | Op_Minus?
        USciNumber
      | Op_Minus?
        UDecimal
      | Op_Minus?
        UInteger
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
      ( Function_Tag
      | type_specifier
      )
      Identifier
      parameter_set
      scope_body
    ;

event_handler
    : Event_Tag
      ( Event_Flag
      | Event_Key
      | Event_Click
      | Event_Backdrop
      | Event_Message
      | Event_Cloned
      | Event_Loudness
      | Event_Timer
      | Event_Video
      )
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
    : type_specifier?
      Identifier
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
      | var_assignment
      | array_assignment
      | array_full_assignment
      | return_stmt
      | if_stmt
      | switch_stmt
      | repeat_loop
      | for_loop
      | foreach_loop
      | forever_loop
      | while_loop
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
    : ( Decl_Var
      | type_specifier
      )
      Identifier
      ( Assign
        expression
      )?
      Terminator
    ;

array_declaration
    : ( Decl_Array
      | type_specifier
      )
      Square_Open
      UInteger
      Square_Close
      Identifier
      ( Assign
        array_literal
      )?
      Terminator
    ;

array_literal
    : Brace_Open
      ( expression
        Separator
      )*
      expression
      Brace_Close
    ;

method_call
    : Identifier
      Bracket_Open
      ( ( expression
          Separator
        )*
        expression
      )?
      Bracket_Close
    ;

var_assignment
    : Identifier
      assignment_suffix
      Terminator
    ;

array_assignment
    : Identifier
      Square_Open
      expression
      Square_Close
      assignment_suffix
      Terminator
    ;

array_full_assignment
    : Identifier
      Assign
      array_literal
      Terminator
    ;

assignment_suffix
    : ( ( Assign
        | Assign_Add
        | Assign_Sub
        | Assign_Concat
        )
        expression
      | ( Assign_Inc
        | Assign_Dec
        )
      )
    ;

return_stmt
    : Return_Tag
      expression?
      Terminator
    ;

if_stmt
    : If_Tag
      Bracket_Open
      expression
      Bracket_Close
      ( scope_body
        ( ElseIf_Tag
          Bracket_Open
          expression
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
      expression
      Bracket_Close
      Brace_Open
      ( ( Case_Tag
          constant
          Colon
        )+
        statement*
        Break_Tag
        Terminator
      )+
      ( Default_Tag
        Colon
        statement*
        Break_Tag
        Terminator
      )?
      Brace_Close
    ;

repeat_loop
    : Inline_Tag?
      Repeat_Tag
      Bracket_Open
      UInteger
      Bracket_Close
      scope_body
    ;

for_loop
    : For_Tag
      Bracket_Open
      var_declaration
      expression
      Terminator
      Identifier
      assignment_suffix
      Bracket_Close
      scope_body
    ;

foreach_loop
    : Foreach_Tag
      Bracket_Open
      Decl_Var
      Identifier
      In_Tag
      Identifier
      Bracket_Close
      scope_body
    ;

forever_loop
    : Forever_Tag
      scope_body
    ;

while_loop
    : While_Tag
      Bracket_Open
      expression
      Bracket_Close
      scope_body
    ;

uconstant
    : ( Const_True
      | Const_False
      | StringLiteral
      | HexNumber
      | USciNumber
      | UDecimal
      | UInteger
      )
    ;

primary_expression
    : ( uconstant
      | method_call
      | Identifier
      | Bracket_Open
        expression
        Bracket_Close
      )
    ;

unary_expression
    : ( Op_Not
      | Op_Minus
      )?
      primary_expression
    ;

expression
    :   unary_expression
    |   expression Op_Pow unary_expression
    |   expression Op_Mult unary_expression
    |   expression Op_Divide unary_expression
    |   expression Op_Mod unary_expression
    |   expression Op_Concat unary_expression
    |   expression Op_Plus unary_expression
    |   expression Op_Minus unary_expression
    |   expression Op_LShift unary_expression
    |   expression Op_RShift unary_expression
    |   expression Op_LT unary_expression
    |   expression Op_GT unary_expression
    |   expression Op_LTE unary_expression
    |   expression Op_GTE unary_expression
    |   expression Op_Equals unary_expression
    |   expression Op_NEquals unary_expression
    |   expression Op_And unary_expression
    |   expression Op_Or unary_expression
    ;

/*
 * Lexer Rules
 */

Const_True      : 'true';
Const_False     : 'false';

Assign_Add	    : '+=';
Assign_Sub      : '-=';
Assign_Concat   : '.=';
Assign_Inc      : '++';
Assign_Dec      : '--';

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
Op_NEquals      : '!=';
Op_GTE          : '>=';
Op_LTE          : '<=';
Op_GT           : '>';
Op_LT           : '<';
Op_And          : '&&';
Op_Or           : '||';
Op_Not          : '!';

Assign	        : '=';

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

Type_Num        : 'num';
Type_String     : 'string';
Type_Bool       : 'bool';

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

Event_Flag      : 'GreenFlag';
Event_Key       : 'KeyPressed';
Event_Click     : 'Clicked';
Event_Backdrop  : 'BackdropChanged';
Event_Message   : 'MessageRecieved';
Event_Cloned    : 'Cloned';
Event_Loudness  : 'LoudnessGreaterThan';
Event_Timer     : 'TimerGreaterThan';
Event_Video     : 'VideoMotionGreaterThan';

ElseIf_Tag      : 'else if';
If_Tag          : 'if';
Else_Tag        : 'else';

Switch_Tag      : 'switch';
Case_Tag        : 'case';
Default_Tag     : 'default';
Break_Tag       : 'break';

Forever_Tag     : 'forever';
Foreach_Tag     : 'foreach';
In_Tag          : 'in';
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
    : '0'
      [xX]
      HexDigit+
    ;

USciNumber
    : ( UDecimal
      | UInteger
      )
      'e'
      Op_Minus?
      UInteger
    ;

UDecimal
    : UInteger
      '.'
      UInteger
    ;

UInteger
    : Digit+
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
    : PrintableChar+
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