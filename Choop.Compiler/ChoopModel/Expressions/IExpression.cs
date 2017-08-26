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
    }
}
