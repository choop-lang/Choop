using Choop.Compiler.Helpers;

namespace Choop.Compiler.ChoopModel.Expressions
{
    /// <summary>
    /// Represents an expression that can be evaluated to produce a value.
    /// </summary>
    public interface IExpression : ICompilable<object>
    {
        /// <summary>
        /// Balances the binary trees within the expression.
        /// </summary>
        /// <returns>The balanced expression.</returns>
        IExpression Balance();

        /// <summary>
        /// Returns the output type of the translated expression.
        /// </summary>
        /// <param name="context">The current translation state.</param>
        DataType GetReturnType(TranslationContext context);
    }
}
