grammar Choop;

/*
 * Parser Rules
 */

// Entry point
compilation_unit
	: EOF
	;

sprite
	:	{  }
	;

/*
 * Lexer Rules
 */

Identifier
    :   [\w]*
    ;

Whitespace
    :   [ \t]+
        -> skip
    ;

Newline
    :   (   '\r' '\n'?
        |   '\n'
        )
        -> skip
    ;

BlockComment
    :   '/*' .*? '*/'
        -> skip
    ;

LineComment
    :   '//' ~[\r\n]*
        -> skip
    ;