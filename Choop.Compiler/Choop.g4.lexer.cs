using Antlr4.Runtime;
using System.Collections.Generic;

namespace Choop.Compiler
{
    partial class ChoopLexer
    {
        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="ChoopLexer"/> class. 
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <param name="errorCollection">The collection to record token recognition errors in.</param>
        public ChoopLexer(ICharStream input, ICollection<CompilerError> errorCollection) : this(input)
        {
            // Set error listener
            RemoveErrorListeners();
            AddErrorListener(new ChoopTokenErrorListener(errorCollection));
        }
        #endregion
    }
}
