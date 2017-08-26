using Antlr4.Runtime;

namespace Choop.Compiler.ChoopModel.Sprites
{
    /// <summary>
    /// Represents a using statement.
    /// </summary>
    public class UsingStmt : IRule
    {
        #region Properties

        /// <summary>
        /// Gets the name of the module to import.
        /// </summary>
        public string Module { get; }

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
        /// Creates a new instance of the <see cref="UsingStmt"/> class.
        /// </summary>
        /// <param name="module">The name of the module to import.</param>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="errorToken">The token to report any compiler errors to.</param>
        public UsingStmt(string module, string fileName, IToken errorToken)
        {
            Module = module;
            FileName = fileName;
            ErrorToken = errorToken;
        }

        #endregion
    }
}