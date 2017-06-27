grammar Choop;

/*
 * Parser Rules
 */

// Entry point
root
    : ( sprite
      | module
      | globalStmt
      )*
      EOF
    ;

sprite
    : metaAttribute?
      SpriteTag
      Identifier
      BraceOpen
      usingStmt*
      spriteBody
      BraceClose
    ;

metaAttribute
    : SquareOpen
      ( AttrMeta
        BracketOpen
        constant
        BracketClose
      )
      SquareClose
    ;

usingStmt
    : UsingTag
      Identifier
      Terminator
    ;

spriteBody
    : ( globalStmt
      | methodDeclaration
      | eventHandler
      )*
    ;

module
    : ModuleTag
      Identifier
      BraceOpen
      spriteBody
      BraceClose
    ;

globalStmt
    : globalStmtNoTerminator
      Terminator
    ;

globalStmtNoTerminator
    : constDeclaration
    | varGlobalDeclaration
    | arrayGlobalDeclaration
    | listGlobalDeclaration
    ;

constDeclaration
    : DeclConst
      typeSpecifier?
      Identifier
      Assign
      constant
    ;

typeSpecifier
    : TypeNum
    | TypeString
    | TypeBool
    ;

varGlobalDeclaration
    : ( DeclVar
      | typeSpecifier
      )
      Identifier
      ( Assign
        constant
      )?
    ;

arrayGlobalDeclaration
    : ( DeclArray
      | typeSpecifier
      )
      SquareOpen
      UInteger
      SquareClose
      Identifier
      ( Assign
        arrayConstant
      )?
    ;

listGlobalDeclaration
    : DeclList
      ( OpLT
        typeSpecifier
        OpGT
      )?
      SquareOpen
      UInteger?
      SquareClose
      Identifier
      ( Assign
        arrayConstant
      )?
    ;

arrayConstant
    : BraceOpen
      ( constant
        Separator
      )*
      constant
      BraceClose
    ;

constant
    : ConstTrue
    | ConstFalse
    | StringLiteral
    | OpMinus?
      HexNumber
    | OpMinus?
      USciNumber
    | OpMinus?
      UDecimal
    | OpMinus?
      UInteger
    ;

methodDeclaration
    : ( UnsafeTag
      | InlineTag
      | AtomicTag
      )*
      ( VoidTag
      | FunctionTag
      | typeSpecifier
      )
      Identifier
      parameterSet
      scopeBody
    ;

eventHandler
    : ( UnsafeTag
      | AtomicTag
      )*
      EventTag
      Identifier
      ( OpLT
        constant
        OpGT
      )?
      BracketOpen
      BracketClose
      scopeBody
    ;

parameterSet
    : BracketOpen
      ( parameter
        Separator
      )*
      ( ( optionalParameter
          Separator
        )*
        optionalParameter
      | parameter
      )?
      BracketClose
    ;

optionalParameter
    : parameter
      Assign
      constant
    ;

parameter
    : typeSpecifier?
      Identifier
    ;

scopeBody
    : BraceOpen
      ( statement
      )*
      BraceClose
    ;

statement
    : stmtNoTerminator
      Terminator
    | ifStmt
    | switchStmt
    | repeatLoop
    | forLoop
    | foreachLoop
    | foreverLoop
    | whileLoop
    | scopeBody
    ;

stmtNoTerminator
    : varDeclaration
    | arrayDeclaration
    | methodCall
    | varAssignment
    | arrayAssignment
    | arrayFullAssignment
    | returnStmt
    ;

varDeclaration
    : ( DeclVar
      | typeSpecifier
      )
      Identifier
      ( Assign
        expression
      )?
    ;

arrayDeclaration
    : ( DeclArray
      | typeSpecifier
      )
      SquareOpen
      UInteger
      SquareClose
      Identifier
      ( Assign
        arrayLiteral
      )?
    ;

arrayLiteral
    : BraceOpen
      ( expression
        Separator
      )*
      expression
      BraceClose
    ;

methodCall
    : Identifier
      BracketOpen
      ( ( expression
          Separator
        )*
        expression
      )?
      BracketClose
    ;

varAssignment
    : Identifier
      assignmentSuffix
    ;

arrayAssignment
    : Identifier
      SquareOpen
      expression
      SquareClose
      assignmentSuffix
    ;

arrayFullAssignment
    : Identifier
      Assign
      arrayLiteral
    ;

assignmentSuffix
    : ( Assign
      | AssignAdd
      | AssignSub
      | AssignConcat
      )
      expression
    | ( AssignInc
      | AssignDec
      )
    ;

returnStmt
    : ReturnTag
      expression?
    ;

ifStmt
    : IfTag
      BracketOpen
      expression
      BracketClose
      ( scopeBody
        ( ElseIfTag
          BracketOpen
          expression
          BracketClose
          scopeBody
        )*
        ( ElseTag
          scopeBody
        )?
      | statement
      )
    ;

switchStmt
    : SwitchTag
      BracketOpen
      expression
      BracketClose
      BraceOpen
      ( ( CaseTag
          constant
          Colon
        )+
        caseBody
      )+
      ( ( DefaultTag
          Colon
        )
        caseBody
      )?
      BraceClose
    ;

caseBody
    : statement*
      ( BreakTag
        Terminator
      | returnStmt
        Terminator
      )
    ;

repeatLoop
    : InlineTag?
      RepeatTag
      BracketOpen
      UInteger
      BracketClose
      scopeBody
    ;

forLoop
    : ForTag
      BracketOpen
      varDeclaration
      Terminator
      expression
      Terminator
      varAssignment
      BracketClose
      scopeBody
    ;

foreachLoop
    : ForeachTag
      BracketOpen
      ( DeclVar
      | typeSpecifier
      )
      Identifier
      InTag
      Identifier
      BracketClose
      scopeBody
    ;

foreverLoop
    : ForeverTag
      scopeBody
    ;

whileLoop
    : WhileTag
      BracketOpen
      expression
      BracketClose
      scopeBody
    ;

uconstant
    : ConstTrue
    | ConstFalse
    | StringLiteral
    | HexNumber
    | USciNumber
    | UDecimal
    | UInteger
    ;

primaryExpression
    : uconstant
    | methodCall
    | Identifier
    | Identifier
      SquareOpen
      expression
      SquareClose
    | BracketOpen
      expression
      BracketClose
    ;

unaryExpression
    : ( OpNot
      | OpMinus
      )?
      primaryExpression
    ;

expression
    : unaryExpression
    | expression OpPow unaryExpression
    | expression OpMult unaryExpression
    | expression OpDivide unaryExpression
    | expression OpMod unaryExpression
    | expression OpConcat unaryExpression
    | expression OpPlus unaryExpression
    | expression OpMinus unaryExpression
    | expression OpLShift unaryExpression
    | expression OpRShift unaryExpression
    | expression OpLT unaryExpression
    | expression OpGT unaryExpression
    | expression OpLTE unaryExpression
    | expression OpGTE unaryExpression
    | expression OpEquals unaryExpression
    | expression OpNEquals unaryExpression
    | expression OpAnd unaryExpression
    | expression OpOr unaryExpression
    ;

/*
 * Lexer Rules
 */

ConstTrue      : 'true';
ConstFalse     : 'false';

AssignAdd	    : '+=';
AssignSub      : '-=';
AssignConcat   : '.=';
AssignInc      : '++';
AssignDec      : '--';

OpConcat       : '.';
OpPlus	        : '+';
OpMinus        : '-';
OpDivide       : '/';
OpMult         : '*';
OpMod          : '%';
OpPow          : '^';
OpLShift       : '<<';
OpRShift       : '>>';
OpEquals       : '==';
OpNEquals      : '!=';
OpGTE          : '>=';
OpLTE          : '<=';
OpGT           : '>';
OpLT           : '<';
OpAnd          : '&&';
OpOr           : '||';
OpNot          : '!';

Assign	        : '=';

BracketOpen    : '(';
BracketClose   : ')';
BraceOpen      : '{';
BraceClose     : '}';
SquareOpen	    : '[';
SquareClose    : ']';
Separator       : ',';
Terminator      : ';';
Colon           : ':';

DeclConst      : 'const';
DeclVar        : 'var';
DeclArray      : 'array';
DeclList       : 'list';

TypeNum        : 'num';
TypeString     : 'string';
TypeBool       : 'bool';

SpriteTag      : 'sprite';
ModuleTag      : 'module';

AttrMeta       : 'MetaFile';

UsingTag       : 'using';

VoidTag        : 'void';
FunctionTag    : 'function';
EventTag       : 'event';
UnsafeTag      : 'unsafe';
InlineTag      : 'inline';
AtomicTag      : 'atomic';

ElseIfTag      : 'else if';
IfTag          : 'if';
ElseTag        : 'else';

SwitchTag      : 'switch';
CaseTag        : 'case';
DefaultTag     : 'default';
BreakTag       : 'break';

ForeverTag     : 'forever';
ForeachTag     : 'foreach';
InTag          : 'in';
ForTag         : 'for';
WhileTag       : 'while';
RepeatTag      : 'repeat';

ReturnTag      : 'return';

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
      OpMinus?
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
    : '\\'
      ['"n\\]
    | [ !#-&(-[\]-~]
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