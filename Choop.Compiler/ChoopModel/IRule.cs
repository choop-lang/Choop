using Antlr4.Runtime;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents an internal representation of a grammar rule.
    /// </summary>
    public interface IRule
    {
        #region Properties

        /// <summary>
        /// Gets the token to mark any syntax errors detected during translation at.
        /// </summary>
        IToken ErrorToken { get; }

        /// <summary>
        /// Gets the file name where the rule was specified.
        /// </summary>
        string FileName { get; }

        #endregion
    }
}