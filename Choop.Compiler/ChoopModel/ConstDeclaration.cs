using Antlr4.Runtime;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents the a constant declaration.
    /// </summary>
    public class ConstDeclaration : IVarDeclaration<TerminalExpression>, IRule
    {
        #region Properties

        /// <summary>
        /// Gets the name of the constant.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the type of the data stored in the constant.
        /// </summary>
        public DataType Type { get; }

        /// <summary>
        /// Gets the value of the constant.
        /// </summary>
        public TerminalExpression Value { get; }

        /// <summary>
        /// Gets the token to report any compiler errors to.
        /// </summary>
        public IToken ErrorToken { get; }

        /// <summary>
        /// Gets the file name where the grammar structure was found.
        /// </summary>
        public string FileName { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="ConstDeclaration"/> class.
        /// </summary>
        /// <param name="name">The name of the constant.</param>
        /// <param name="type">The data type of the constant.</param>
        /// <param name="value">The initial value of the constant.</param>
        public ConstDeclaration(string name, DataType type, TerminalExpression value)
        {
            Name = name;
            Type = type;
            Value = value;
        }

        #endregion
    }
}