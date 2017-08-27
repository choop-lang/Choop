using System;
using Antlr4.Runtime;
using Choop.Compiler.BlockModel;
using Choop.Compiler.ChoopModel.Declarations;
using Choop.Compiler.ChoopModel.Methods;
using Choop.Compiler.Helpers;

namespace Choop.Compiler.ChoopModel.Expressions
{
    /// <summary>
    /// Represents a value lookup expression.
    /// </summary>
    public class LookupExpression : IExpression
    {
        #region Properties

        /// <summary>
        /// Gets the name of the identifier being looked up.
        /// </summary>
        public string IdentifierName { get; }

        /// <summary>
        /// Gets the token to report any compiler errors to.
        /// </summary>
        public IToken ErrorToken { get; }

        /// <summary>
        /// Gets the file name where the grammar structure was found.
        /// </summary>
        public string FileName { get; }

        /// <summary>
        /// Gets the variable being looked up.
        /// </summary>
        protected ITypedDeclaration Variable { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="LookupExpression"/> class.
        /// </summary>
        /// <param name="identifierName">The name of the identifier being looked up.</param>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="errorToken">The token to report any compiler errors to.</param>
        public LookupExpression(string identifierName, string fileName, IToken errorToken)
        {
            IdentifierName = identifierName;
            FileName = fileName;
            ErrorToken = errorToken;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="LookupExpression"/> class.
        /// </summary>
        /// <param name="variable">The variable being looked up.</param>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="errorToken">The token to report any compiler errors to.</param>
        public LookupExpression(ITypedDeclaration variable, string fileName, IToken errorToken)
        {
            Variable = variable;
            IdentifierName = variable.Name;
            FileName = fileName;
            ErrorToken = errorToken;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Balances the binary trees within the expression.
        /// </summary>
        /// <returns>The balanced expression.</returns>
        public virtual IExpression Balance() => this;

        /// <summary>
        /// Returns the output type of the translated expression.
        /// </summary>
        /// <param name="context">The current translation state.</param>
        public DataType GetReturnType(TranslationContext context) =>
            (context.GetDeclaration(IdentifierName) as ITypedDeclaration)?.Type ?? DataType.Object;

        /// <summary>
        /// Gets the translated code for the grammar structure.
        /// </summary>
        /// <returns>The translated code for the grammar structure.</returns>
        public virtual object Translate(TranslationContext context)
        {
            if (Variable == null)
            {
                IDeclaration identifier = context.GetDeclaration(IdentifierName);

                if (identifier is StackValue || identifier is ParamDeclaration || identifier is GlobalVarDeclaration ||
                    identifier is ConstDeclaration)
                {
                    Variable = (ITypedDeclaration)identifier;
                }
                else
                {
                    context.ErrorList.Add(new CompilerError($"'{IdentifierName}' is not a variable",
                        ErrorType.ImproperUsage, ErrorToken, FileName));
                    return null;
                }
            }

            switch (Variable)
            {
                case StackValue stackValue:
                    return stackValue.CreateVariableLookup();

                case ParamDeclaration _:
                    return new Block(BlockSpecs.GetParameter, Variable.Name);

                case GlobalVarDeclaration _:
                    return new Block(BlockSpecs.GetVariable, Variable.Name);

                case ConstDeclaration constDeclaration:
                    return constDeclaration.Value.Value;

                default:
                    // Should not happen - throw error
                    throw new Exception("Unknown identifier type");
            }
        }

        #endregion
    }
}