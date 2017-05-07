using Antlr4.Runtime;
using System.Collections.ObjectModel;

namespace Choop.Compiler
{
    sealed partial class ChoopLexer
    {
        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="ChoopLexer"/> class. 
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <param name="errorCollection">The collection to record token recognition errors in.</param>
        public ChoopLexer(ICharStream input, Collection<CompilerError> errorCollection) : this(input)
        {
            // Set error listener
            RemoveErrorListeners();
            AddErrorListener(new ChoopTokenErrorListener(errorCollection));
        }
        #endregion
    }
}
