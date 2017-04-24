using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace Choop.Compiler
{
    /// <summary>
    /// Compiles Choop code.
    /// </summary>
    public class ChoopCompiler
    {
        #region Properties
        private ICollection<CompilerError> compileErrors = null;

        /// <summary>
        /// Gets the collection of compiler errors that occured whilst compiling the file. 
        /// </summary>
        public ICollection<CompilerError> CompileErrors
        {
            get { return compileErrors; }
        }

        /// <summary>
        /// Gets whether any compiler errors occured.
        /// </summary>
        public bool HasErrors
        {
            get { return CompileErrors.Count > 0; }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="ChoopCompiler"/> class. 
        /// </summary>
        public ChoopCompiler()
        {

        }
        #endregion
        #region Methods
        /// <summary>
        /// Compiles the code from the specified input stream.
        /// </summary>
        /// <param name="input">The input stream containing the code to compile.</param>
        public void Compile(Stream input) {
            Compile(new AntlrInputStream(input));
        }

        /// <summary>
        /// Compiles the specified code.
        /// </summary>
        /// <param name="input">The code to compile.</param>
        public void Compile(string input)
        {
            Compile(new AntlrInputStream(input));
        }

        /// <summary>
        /// Compiles the code from the specified input stream.
        /// </summary>
        /// <param name="input">The input stream to compile the code from.</param>
        private void Compile(AntlrInputStream input)
        {
            // Create the lexer
            ChoopLexer lexer = new ChoopLexer(input);

            // Get the tokens from the lexer
            CommonTokenStream tokens = new CommonTokenStream(lexer);

            // Create the parser
            ChoopParser parser = new ChoopParser(tokens);

            // Clear compile errors
            compileErrors = new Collection<CompilerError>();

            // Set error listener
            parser.RemoveErrorListeners();
            parser.AddErrorListener(new ChoopErrorListener(compileErrors));

            // Gets the parse tree
            ChoopParser.RootContext root = parser.root();

            // Create the listener which will translate the code
            ChoopListener translator = new ChoopListener();

            // Walk along the tree, causing it to be translated
            ParseTreeWalker.Default.Walk(translator, root);
        }
        #endregion
    }
}
