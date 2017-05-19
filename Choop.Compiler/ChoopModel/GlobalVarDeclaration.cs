using Choop.Compiler.BlockModel;
using Choop.Compiler.ObjectModel;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents a global variable declaration.
    /// </summary>
    public class GlobalVarDeclaration : IGlobalVarDeclaration<TerminalExpression, VarSignature>, ICompilable<Variable>
    {
        #region Properties

        /// <summary>
        /// Gets the name of the variable.
        /// </summary>
        public string Name => Signature.Name;

        /// <summary>
        /// Gets the type of the data stored in the variable.
        /// </summary>
        public DataType Type => Signature.Type;

        /// <summary>
        /// Gets the initial value stored in the variable.
        /// </summary>
        public TerminalExpression Value { get; }

        /// <summary>
        /// Gets the signature of the variable.
        /// </summary>
        public VarSignature Signature { get; }
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="GlobalVarDeclaration"/> class.
        /// </summary>
        /// <param name="value">The initial value of the variable.</param>
        /// <param name="signature">The signature of the variable.</param>
        public GlobalVarDeclaration(TerminalExpression value, VarSignature signature)
        {
            Value = value;
            Signature = signature;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Gets the translated code for the grammar structure.
        /// </summary>
        /// <returns>The translated code for the grammar structure.</returns>
        public Variable Translate()
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}