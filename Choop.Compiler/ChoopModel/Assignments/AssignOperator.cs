namespace Choop.Compiler.ChoopModel.Assignments
{
    /// <summary>
    /// Specifies the possible operators for an assignment.
    /// </summary>
    public enum AssignOperator
    {
        /// <summary>
        /// Indicates the = operator.
        /// </summary>
        Equals,

        /// <summary>
        /// Indicates the += operator.
        /// </summary>
        AddEquals,

        /// <summary>
        /// Indicates the -= operator.
        /// </summary>
        MinusEquals,

        /// <summary>
        /// Indicates the .= operator.
        /// </summary>
        DotEquals,

        /// <summary>
        /// Indicates the ++ operator.
        /// </summary>
        PlusPlus,

        /// <summary>
        /// Indicates the -- operator.
        /// </summary>
        MinusMinus
    }
}