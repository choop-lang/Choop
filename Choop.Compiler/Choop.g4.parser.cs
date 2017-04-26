using Antlr4.Runtime;
using System.Collections.Generic;

namespace Choop.Compiler
{
    partial class ChoopParser
    {
        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="ChoopParser"/> class. 
        /// </summary>
        /// <param name="input">The token stream to parse.</param>
        /// <param name="errorCollection">The collection to record compiler errors to.</param>
        public ChoopParser(ITokenStream input, ICollection<CompilerError> errorCollection) : this(input)
        {
            // Set error listener
            RemoveErrorListeners();
            AddErrorListener(new ChoopParserErrorListener(errorCollection));
        }
        #endregion
    }
}
