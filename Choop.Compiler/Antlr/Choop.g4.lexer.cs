using System.Collections.ObjectModel;
using Antlr4.Runtime;

namespace Choop.Compiler.Antlr
{
    sealed partial class ChoopLexer
    {
        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="ChoopLexer"/> class. 
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <param name="errorCollection">The collection to record token recognition errors in.</param>
        /// <param name="fileName">The name of the file currently being compiled.</param>
        public ChoopLexer(ICharStream input, Collection<CompilerError> errorCollection, string fileName) : this(input)
        {
            // Set error listener
            RemoveErrorListeners();
            AddErrorListener(new ChoopTokenErrorListener(errorCollection, fileName));
        }

        #endregion
    }
}