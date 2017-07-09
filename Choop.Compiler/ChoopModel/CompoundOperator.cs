namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Specifies the possible compound operators.
    /// </summary>
    public enum CompundOperator
    {
        /// <summary>
        /// Indicates the ^ operator.
        /// </summary>
        Pow,

        /// <summary>
        /// Indicates the * operator.
        /// </summary>
        Multiply,

        /// <summary>
        /// Indicates the / operator.
        /// </summary>
        Divide,

        /// <summary>
        /// Indicates the % operator.
        /// </summary>
        Mod,

        /// <summary>
        /// Indicates the . operator.
        /// </summary>
        Concat,

        /// <summary>
        /// Indicates the + operator.
        /// </summary>
        Plus,

        /// <summary>
        /// Indicates the - operator.
        /// </summary>
        Minus,

        /// <summary>
        /// Indicates the &lt;&lt; operator.
        /// </summary>
        LShift,

        /// <summary>
        /// Indicates the &gt;&gt; operator.
        /// </summary>
        RShift,

        /// <summary>
        /// Indicates the &lt; operator.
        /// </summary>
        LessThan,

        /// <summary>
        /// Indicates the &gt; operator.
        /// </summary>
        GreaterThan,

        /// <summary>
        /// Indicates the &lt;= operator.
        /// </summary>
        LessThanEq,

        /// <summary>
        /// Indicates the &gt;= operator.
        /// </summary>
        GreaterThanEq,

        /// <summary>
        /// Indicates the == operator.
        /// </summary>
        Equal,

        /// <summary>
        /// Indicates the != operator.
        /// </summary>
        NotEqual,

        /// <summary>
        /// Indicates the &amp;&amp; operator.
        /// </summary>
        And,

        /// <summary>
        /// Indicates the || operator.
        /// </summary>
        Or
    }
}