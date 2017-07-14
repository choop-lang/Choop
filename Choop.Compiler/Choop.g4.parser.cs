using Antlr4.Runtime;
using System.Collections.ObjectModel;

namespace Choop.Compiler
{
    sealed partial class ChoopParser
    {
        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="ChoopParser"/> class. 
        /// </summary>
        /// <param name="input">The token stream to parse.</param>
        /// <param name="errorCollection">The collection to record compiler errors to.</param>
        public ChoopParser(ITokenStream input, Collection<CompilerError> errorCollection, string fileName) : this(input)
        {
            // Set error listener
            RemoveErrorListeners();
            AddErrorListener(new ChoopParserErrorListener(errorCollection, fileName));
        }

        #endregion
    }
}