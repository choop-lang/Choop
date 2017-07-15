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
        /// Gets the token to report any compiler errors to.
        /// </summary>
        IToken ErrorToken { get; }

        /// <summary>
        /// Gets the file name where the grammar structure was found.
        /// </summary>
        string FileName { get; }

        #endregion
    }
}