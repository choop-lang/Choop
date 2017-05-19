using System.Linq;
using Choop.Compiler.BlockModel;
using Choop.Compiler.ObjectModel;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents a global list or array declaration.
    /// </summary>
    public class GlobalListDeclaration : IGlobalVarDeclaration<TerminalExpression[], VarSignature>, IArrayDeclaration, ICompilable<List>
    {
        #region Properties

        /// <summary>
        /// Gets the name of the list.
        /// </summary>
        public string Name => Signature.Name;

        /// <summary>
        /// Gets the type of the data stored in each list item.
        /// </summary>
        public DataType Type => Signature.Type;

        /// <summary>
        /// Gets the length of the list.
        /// </summary>
        public int Length => Value.Length;

        /// <summary>
        /// Gets the initial values stored in the list.
        /// </summary>
        public TerminalExpression[] Value { get; }

        /// <summary>
        /// Gets whether the list acts as an array.
        /// </summary>
        public bool IsArray { get; }

        /// <summary>
        /// Gets the signature of the list.
        /// </summary>
        public VarSignature Signature { get; }

        IExpression[] IVarDeclaration<IExpression[]>.Value => Value.Cast<IExpression>().ToArray();
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="GlobalListDeclaration"/> class.
        /// </summary>
        /// <param name="value">The initial values of the list.</param>
        /// <param name="isArray">Whether the list acts as an array.</param>
        /// <param name="signature">The signature of the list.</param>
        public GlobalListDeclaration(TerminalExpression[] value, bool isArray, VarSignature signature)
        {
            Value = value;
            IsArray = isArray;
            Signature = signature;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Gets the translated code for the grammar structure.
        /// </summary>
        /// <returns>The translated code for the grammar structure.</returns>
        public List Translate()
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}