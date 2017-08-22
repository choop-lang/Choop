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
    : MetaAttr = metaAttribute?
      SpriteTag
      Name = Identifier
      BraceOpen
      usingStmt*
      spriteBody
      BraceClose
    ;

metaAttribute
    : SquareOpen
      ( AttrMeta
        BracketOpen
        FileName = StringLiteral
        BracketClose
      )
      SquareClose
    ;

usingStmt
    : UsingTag
      Module = Identifier
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
      Name = Identifier
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
      Type = typeSpecifier?
      Name = Identifier
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
	  | Type = typeSpecifier
	  )
      Name = Identifier
      ( Assign
        constant
      )?
    ;

arrayGlobalDeclaration
    : ( DeclArray
      | Type = typeSpecifier
      )
      SquareOpen
      Bounds = UInteger
      SquareClose
      Name = Identifier
      ( Assign
        arrayConstant
      )?
    ;

listGlobalDeclaration
    : DeclList
      ( OpLT
        Type = typeSpecifier
        OpGT
      )?
      SquareOpen
      Bounds = UInteger?
      SquareClose
      Name = Identifier
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
    : ConstTrue     #constantTrue
    | ConstFalse    #constantFalse
    | StringLiteral #constantString
    | HexNumber     #constantHex
    | OpMinus?
      USciNumber    #constantSci
    | OpMinus?
      UDecimal      #constantDec
    | OpMinus?
      UInteger      #constantInt
    ;

methodDeclaration
    : ( UnsafeTag
      | InlineTag
      | AtomicTag
      )*
      ( Void = VoidTag
      | FunctionTag
      | Type = typeSpecifier
      )
      Name = Identifier
      parameterSet
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

parameter
    : Type = typeSpecifier?
      Name = Identifier
    ;

optionalParameter
    : Type = typeSpecifier?
      Name = Identifier
      Assign
      constant
    ;

eventHandler
    : eventHead
      scopeBody
    ;

eventHead
    : ( UnsafeTag
      | AtomicTag
      )*
      EventTag
      Event = Identifier
      ( OpLT
        constant
        OpGT
      )?
      BracketOpen
      BracketClose
    ;

scopeBody
    : BraceOpen
      statement*
      BraceClose
	| stmtNoScope
    ;

statement
    : stmtNoScope
    | stmtScope
    ;

stmtNoScope
	: stmtNoTerminator
      Terminator          #stmtDefault
    | ifStmt              #stmtIf
    | switchStmt          #stmtSwitch
    | repeatLoop          #stmtRepeat
    | forLoop             #stmtFor
    | foreachLoop         #stmtForEach
    | foreverLoop         #stmtForever
    | whileLoop           #stmtWhile
	;

stmtNoTerminator
    : varDeclaration      #stmtVarDecl
    | arrayDeclaration    #stmtArrayDecl
    | methodCall          #stmtMethodCall
    | varAssignment       #stmtAssignVar
    | arrayAssignment     #stmtAssignArray
    | arrayFullAssignment #stmtReassignArray
    | returnStmt          #stmtReturn
    ;

varDeclaration
    : UnsafeTag?
	  ( DeclVar
      | Type = typeSpecifier
      )
      Name = Identifier
      ( Assign
        expression
      )?
    ;

arrayDeclaration
    : UnsafeTag?
	  ( DeclArray
      | Type = typeSpecifier
      )
      SquareOpen
      Bounds = UInteger
      SquareClose
      Name = Identifier
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
    : Method = Identifier
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
      assignOp
      expression #assignVar
	| Identifier
	  AssignInc  #assignVarInc
	| Identifier
	  AssignDec  #assignVarDec
    ;

arrayAssignment
    : Identifier
      SquareOpen
      expression
      SquareClose
      assignOp
      expression  #assignArray
	| Identifier
      SquareOpen
      expression
      SquareClose
      AssignInc   #assignArrayInc
	| Identifier
      SquareOpen
      expression
      SquareClose
      AssignDec   #assignArrayDec
    ;

arrayFullAssignment
    : Name = Identifier
      Assign
      arrayLiteral
    ;

assignOp
    : Assign
    | AssignAdd
    | AssignSub
    | AssignConcat
    ;

returnStmt
    : ReturnTag
      Expression = expression?
    ;

ifStmt
    : ifHead
      scopeBody
      ( elseIfHead
        scopeBody
      )*
      elseBlock?
    ;

ifHead
	: IfTag
      BracketOpen
      expression
      BracketClose
	;

elseIfHead
	: ElseIfTag
      BracketOpen
      expression
      BracketClose
	;

elseBlock
	: ElseTag
      scopeBody
	;

switchStmt
    : switchHead
      BraceOpen
      ( caseHead
        caseBody
      )+
      ( defaultCaseHead
        caseBody
      )?
      BraceClose
    ;

switchHead
	: SwitchTag
      BracketOpen
      expression
      BracketClose
	;

caseHead
	: ( CaseTag
        caseExpression
        Colon
      )+
	;

caseExpression
	:           constant #caseEqual
	| OpGT      constant #caseGT
	| OpLT      constant #caseLT
	| OpGTE     constant #caseGTE
	| OpLTE     constant #caseLTE
	| OpNEquals constant #caseNE
	;

defaultCaseHead
	: DefaultTag
      Colon
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
    : repeatHead
      scopeBody
    ;

repeatHead
	: Inline = InlineTag?
      RepeatTag
      BracketOpen
      expression
      BracketClose
	;

forLoop
    : forHead
      scopeBody
    ;

forHead
	: ForTag
      BracketOpen
	  ( DeclVar
      | VariableType = typeSpecifier
	  )
	  Variable = Identifier
	  Assign
	  expression
	  ToTag
	  expression
	  ( StepTag
	    Step = constant
	  )?
      BracketClose
	;

foreachLoop
    : ForeachTag
      BracketOpen
      ( DeclVar
      | Type = typeSpecifier
      )
      Variable = Identifier
      InTag
      List = Identifier
      BracketClose
      scopeBody
    ;

foreverLoop
    : ForeverTag
      scopeBody
    ;

whileLoop
    : whileHead
      scopeBody
    ;

whileHead
	: WhileTag
      BracketOpen
      expression
      BracketClose
	;

stmtScope
	: Unsafe = UnsafeTag?
	  scopeBody
	;

uconstant
    : ConstTrue         #uConstantTrue
    | ConstFalse        #uConstantFalse
    | StringLiteral     #uConstantString
    | HexNumber         #uConstantHex
    | USciNumber        #uConstantSci
    | UDecimal          #uConstantDec
    | UInteger          #uConstantInt
    ;

primaryExpression
    : uconstant         #primaryConstant
    | methodCall        #primaryMethodCall
    | Identifier        #primaryVarLookup
    | Identifier
      SquareOpen
      expression
      SquareClose       #primaryArrayLookup
    | BracketOpen
      expression
      BracketClose      #primaryBracket
    | NameOfTag
	  BracketOpen
	  Name = Identifier
	  BracketClose      #primaryNameOf
    ;

unaryExpression
    : OpNot
      primaryExpression  #unaryNot
    | OpMinus
      primaryExpression  #unaryMinus
    | primaryExpression  #unaryDefault
    ;

expression
    : unaryExpression                       #expressionDefault
    | expression OpPow unaryExpression      #expressionPow
    | expression OpMult unaryExpression     #expressionMult
    | expression OpDivide unaryExpression   #expressionDivide
    | expression OpMod unaryExpression      #expressionMod
    | expression OpConcat unaryExpression   #expressionConcat
    | expression OpPlus unaryExpression     #expressionPlus
    | expression OpMinus unaryExpression    #expressionMinus
    | expression OpLShift unaryExpression   #expressionLShift
    | expression OpRShift unaryExpression   #expressionRShift
    | expression OpLT unaryExpression       #expressionLT
    | expression OpGT unaryExpression       #expressionGT
    | expression OpLTE unaryExpression      #expressionLTE
    | expression OpGTE unaryExpression      #expressionGTE
    | expression OpEquals unaryExpression   #expressionEquals
    | expression OpNEquals unaryExpression  #expressionNEquals
    | expression OpAnd unaryExpression      #expressionAnd
    | expression OpOr unaryExpression       #expressionOr
    ;

/*
 * Lexer Rules
 */

ConstTrue      : 'true';
ConstFalse     : 'false';

AssignAdd      : '+=';
AssignSub      : '-=';
AssignConcat   : '.=';
AssignInc      : '++';
AssignDec      : '--';

OpConcat       : '.';
OpPlus         : '+';
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

Assign         : '=';

BracketOpen    : '(';
BracketClose   : ')';
BraceOpen      : '{';
BraceClose     : '}';
SquareOpen     : '[';
SquareClose    : ']';
Separator      : ',';
Terminator     : ';';
Colon          : ':';

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
NameOfTag      : 'nameof';

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
ToTag          : 'to';
StepTag        : 'step';
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
    : '"'
      CharSequence
      '"'
    | '\''
      CharSequence
      '\''
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