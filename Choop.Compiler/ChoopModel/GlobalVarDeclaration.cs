using Choop.Compiler.BlockModel;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents a global variable declaration.
    /// </summary>
    public class GlobalVarDeclaration : IVarDeclaration<TerminalExpression>, ICompilable<Variable>
    {
        #region Properties

        /// <summary>
        /// Gets the name of the variable.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the type of the data stored in the variable.
        /// </summary>
        public DataType Type { get; }

        /// <summary>
        /// Gets the initial value stored in the variable.
        /// </summary>
        public TerminalExpression Value { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="GlobalVarDeclaration"/> class.
        /// </summary>
        /// <param name="name">The name of the variable.</param>
        /// <param name="type">The data type of the variable.</param>
        /// <param name="value">The initial value of the variable.</param>
        public GlobalVarDeclaration(string name, DataType type, TerminalExpression value)
        {
            Name = name;
            Type = type;
            Value = value;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the translated code for the grammar structure.
        /// </summary>
        /// <returns>The translated code for the grammar structure.</returns>
        public Variable Translate(TranslationContext context)
        {
            return new Variable(Name, Value.Literal);
        }

        #endregion
    }
}