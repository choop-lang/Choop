namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Specifies the possible terminal value formats.
    /// </summary>
    public enum TerminalType
    {
        /// <summary>
        /// Indicates a boolean value.
        /// </summary>
        Bool,

        /// <summary>
        /// Indicates an escaped string value.
        /// </summary>
        String,

        /// <summary>
        /// Indicates a hexadecimal value.
        /// </summary>
        Hex,

        /// <summary>
        /// Indicates a value written in scientific notation.
        /// </summary>
        Scientific,

        /// <summary>
        /// Indicates a decimal value.
        /// </summary>
        Decimal,

        /// <summary>
        /// Indicates an integer value.
        /// </summary>
        Int
    }
}