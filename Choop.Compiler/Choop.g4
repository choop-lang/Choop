grammar Choop;

/*
 * Parser Rules
 */

// Entry point
root
	: ( sprite
	  | module
	  )*
	  EOF
	;

sprite
	: Sprite_Tag Identifier block
	;

module
	: Module_Tag Identifier block
	;

block
	: Brace_Open Brace_Close
	;

/*
 * Lexer Rules
 */

Const_True		: 'true';
Const_False		: 'false';
Const_Pi		: 'pi';
Const_E			: 'e';

Op_Equals		: '==';
Op_GT			: '>';
Op_LT			: '<';
Op_GTE			: '>=';
Op_LTE			: '<=';
Op_And			: '&&';
Op_Or			: '||';
Op_Not			: '!';
Op_Concat		: '.';
Op_Plus			: '+';
Op_Minus		: '-';
Op_Divide		: '/';
Op_Mult			: '*';
Op_Mod			: '%';
Op_Pow			: '^';
Op_LShift		: '<<';
Op_RShift		: '>>';

Bracket_Open	: '(';
Bracket_Close	: ')';
Brace_Open		: '{';
Brace_Close		: '}';
Square_Open		: '[';
Square_Close	: ']';
Angle_Open		: '<';
Angle_Close		: '>';
Separator		: ',';
Terminator		: ';';

Decl_Const		: 'const';
Decl_Var		: 'var';
Decl_Array		: 'array';

Assign			: '=';
Assign_Add		: '+=';
Assign_Sub		: '-=';
Assign_Concat	: '.=';
Assing_Inc		: '++';
Assing_Dec		: '--';

Sprite_Tag		: 'sprite';
Module_Tag		: 'module';

Void_Tag		: 'void';
Function_Tag	: 'function';
Event_Tag		: 'event';
Atomic_Tag		: 'atomic';
Inline_Tag		: 'inline';

If_Tag			: 'if';
Else_Tag		: 'else';
ElseIf_Tag		: 'else if';

Switch_Tag		: 'switch';
Case_Tag		: 'case';
Default_Tag		: 'default';

For_Tag			: 'for';
While_Tag		: 'while';
Repeat_Tag		: 'repeat';

Return_Tag		: 'return';

Identifier
    :   Nondigit
		(	Nondigit
		|	Digit
		)*
    ;

BlockComment
    :   '/*' .*? '*/'
        -> skip
    ;

LineComment
    :   '//' ~[\r\n]*
        -> skip
    ;

Nondigit
    :   [a-zA-Z_]
    ;

Digit
	: [0-9]
	;

WS
    :   [ \t\r\n]+
        -> skip
    ;