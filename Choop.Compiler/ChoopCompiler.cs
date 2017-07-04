using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System.Collections.ObjectModel;
using System.IO;

namespace Choop.Compiler
{
    /// <summary>
    /// Compiles Choop code.
    /// </summary>
    public class ChoopCompiler
    {
        #region Fields

        /// <summary>
        /// The builder used for creating the internal Choop representation.
        /// </summary>
        private ChoopBuilder _builder;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of the Choop project.
        /// </summary>
        public string ProjectName => _builder.Project.Name;

        /// <summary>
        /// Gets the collection of compiler errors that occured whilst compiling the file. 
        /// </summary>
        public Collection<CompilerError> CompilerErrors { get; } = new Collection<CompilerError>();

        /// <summary>
        /// Gets whether any compiler errors occured.
        /// </summary>
        public bool HasErrors => CompilerErrors.Count > 0;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates an instance of the <see cref="ChoopCompiler"/> class.
        /// </summary>
        /// <param name="name">The name of the Choop project.</param>
        public ChoopCompiler(string name)
        {
            _builder = new ChoopBuilder(name, CompilerErrors);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Compiles the code from the specified file.
        /// </summary>
        /// <param name="input">The input stream containing the code to compile.</param>
        public void AddFile(Stream input)
        {
            Compile(new AntlrInputStream(input));
        }

        /// <summary>
        /// Compiles the specified code.
        /// </summary>
        /// <param name="input">The code to compile.</param>
        public void AddCode(string input)
        {
            Compile(new AntlrInputStream(input));
        }

        /// <summary>
        /// Compiles the code from the specified input stream.
        /// </summary>
        /// <param name="input">The input stream to compile the code from.</param>
        private void Compile(ICharStream input)
        {
            // Create temporary compiler error list
            Collection<CompilerError> compilerErrorsTemp = new Collection<CompilerError>();

            // Create the lexer
            ChoopLexer lexer = new ChoopLexer(input, CompilerErrors);

            // Get the tokens from the lexer
            CommonTokenStream tokens = new CommonTokenStream(lexer);

            // Create the parser
            ChoopParser parser = new ChoopParser(tokens, compilerErrorsTemp);

            // Gets the parse tree
            ChoopParser.RootContext root = parser.root();


            // Check that no fatal syntax errors occured
            if (compilerErrorsTemp.Count > 0)
            {
                // Add compiler errors to global list
                foreach (CompilerError compilerError in compilerErrorsTemp)
                {
                    CompilerErrors.Add(compilerError);
                }

                // Don't create internal Choop representation
                return;
            }

            // Add to the global internal code representation
            ParseTreeWalker.Default.Walk(_builder, root);
        }

        #endregion
    }
}