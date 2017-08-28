using Antlr4.Runtime;
using Choop.Compiler.BlockModel;
using Choop.Compiler.ChoopModel.Declarations;
using Choop.Compiler.Helpers;

namespace Choop.Compiler.ChoopModel.Expressions
{
    /// <summary>
    /// Represents an array value lookup expression.
    /// </summary>
    public class ArrayLookupExpression : LookupExpression
    {
        #region Properties

        /// <summary>
        /// Gets the expression for the index of the item being looked up.
        /// </summary>
        public IExpression Index { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="ArrayLookupExpression"/> class.
        /// </summary>
        /// <param name="identifierName">The name of the array being looked up.</param>
        /// <param name="index">The expression for the index of the item being looked up.</param>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="errorToken">The token to report any compiler errors to.</param>
        public ArrayLookupExpression(string identifierName, IExpression index, string fileName, IToken errorToken) : base(identifierName, fileName, errorToken)
        {
            Index = index;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ArrayLookupExpression"/> class.
        /// </summary>
        /// <param name="declaration">The array declaration.</param>
        /// <param name="index">The expression for the index of the item being looked up.</param>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="errorToken">The token to report any compiler errors to.</param>
        public ArrayLookupExpression(ITypedDeclaration declaration, IExpression index, string fileName, IToken errorToken) : base(declaration, fileName, errorToken)
        {
            Index = index;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the translated code for the grammar structure.
        /// </summary>
        /// <returns>The translated code for the grammar structure.</returns>
        public override object Translate(TranslationContext context)
        {
            ITypedDeclaration value = GetDeclaration(context);

            switch (value)
            {
                case null:
                    // Error already processed
                    return null;

                case GlobalListDeclaration _:
                    return new Block(BlockSpecs.GetItemOfList, Index.Balance().Translate(context), value.Name);

                case StackValue scopedArray:
                    if (scopedArray.StackSpace == 1)
                        return scopedArray.CreateArrayLookup(Index.Balance().Translate(context));

                    context.ErrorList.Add(new CompilerError($"'{value.Name}' is not an array",
                        ErrorType.ImproperUsage, ErrorToken, FileName));
                    return null;

                default:
                    context.ErrorList.Add(new CompilerError($"'{value.Name}' is not an array",
                        ErrorType.ImproperUsage, ErrorToken, FileName));
                    return null;
            }
        }

        #endregion
    }
}