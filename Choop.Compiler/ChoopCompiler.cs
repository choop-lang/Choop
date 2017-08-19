using System;
using System.Collections.ObjectModel;
using System.IO;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Choop.Compiler.Antlr;
using Choop.Compiler.BlockModel;
using Choop.Compiler.ChoopModel;
using Choop.Compiler.TranslationUtils;
using Newtonsoft.Json.Linq;

namespace Choop.Compiler
{
    /// <summary>
    /// Compiles Choop code.
    /// </summary>
    public class ChoopCompiler
    {
        #region Constants

        /// <summary>
        /// The file extension for Choop project files.
        /// </summary>
        public const string ChoopProjFileExt = ".chp";

        /// <summary>
        /// The file extension for Choop source files.
        /// </summary>
        public const string ChoopSourceFileExt = ".chs";

        /// <summary>
        /// The file extension for Choop definition files.
        /// </summary>
        public const string ChoopDefinitionFileExt = ".chd";

        #endregion

        #region Fields

        /// <summary>
        /// The builder used for creating the internal Choop representation.
        /// </summary>
        private readonly ChoopBuilder _builder;

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

        /// <summary>
        /// Gets whether the project has been compiled yet.
        /// </summary>
        public bool Compiled { get; private set; }

        /// <summary>
        /// Gets the internal representation of the Choop project.
        /// </summary>
        public Project ChoopProject => _builder.Project;

        /// <summary>
        /// Gets the internal representation of the Scratch project produced.
        /// </summary>
        public Stage ScratchProject { get; private set; }

        /// <summary>
        /// Gets the compiled JSON of the project.
        /// </summary>
        public JObject ProjectJson { get; private set; }

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
        /// Loads the Choop project at the specified path.
        /// </summary>
        /// <param name="projectPath">The path of the project to load.</param>
        public void LoadProject(string projectPath)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Injects the raw Choop code from the specified stream into the Choop project.
        /// </summary>
        /// <param name="input">The input stream containing the raw Choop code to compile.</param>
        /// <param name="fileName">The name of the file the source code came from, used for error messages. Optional.</param>
        public void InjectCode(Stream input, string fileName = "")
        {
            InjectCode(new AntlrInputStream(input), fileName);
        }

        /// <summary>
        /// Injects the raw Choop code from the specified string into the Choop project.
        /// </summary>
        /// <param name="input">The raw Choop code to compile.</param>
        /// <param name="fileName">The name of the file the source code came from, used for error messages. Optional.</param>
        public void InjectCode(string input, string fileName = "")
        {
            InjectCode(new AntlrInputStream(input), fileName);
        }

        /// <summary>
        /// Compiles the code from the specified input stream.
        /// </summary>
        /// <param name="input">The input stream to compile the code from.</param>
        /// <param name="fileName">The file name of the source code.</param>
        private void InjectCode(ICharStream input, string fileName)
        {
            // Check if already compiled
            if (Compiled) throw new InvalidOperationException();

            // Create temporary compiler error list
            Collection<CompilerError> compilerErrorsTemp = new Collection<CompilerError>();

            // Create the lexer
            ChoopLexer lexer = new ChoopLexer(input, CompilerErrors, fileName);

            // Get the tokens from the lexer
            CommonTokenStream tokens = new CommonTokenStream(lexer);

            // Create the parser
            ChoopParser parser = new ChoopParser(tokens, compilerErrorsTemp, fileName);

            // Gets the parse tree
            ChoopParser.RootContext root = parser.root();

            // Check that no fatal syntax errors occured
            if (compilerErrorsTemp.Count > 0)
            {
                // Add compiler errors to global list
                foreach (CompilerError compilerError in compilerErrorsTemp)
                    CompilerErrors.Add(compilerError);

                // Don't create internal Choop representation
                return;
            }

            // Add to the global internal code representation
            _builder.FileName = fileName;
            ParseTreeWalker.Default.Walk(_builder, root);
        }

        /// <summary>
        /// Injects the specified sprite into the Choop project.
        /// </summary>
        /// <param name="sprite">The sprite to inject.</param>
        public void InjectSprite(SpriteDeclaration sprite)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Injects the specified module into the Choop project.
        /// </summary>
        /// <param name="sprite">The module to inject.</param>
        public void InjectModule(ModuleDeclaration sprite)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Injects the specified image asset and return the internal file name of the asset.
        /// </summary>
        /// <param name="source">The source stream for the asset.</param>
        /// <returns>The internal file name of the asset.</returns>
        public string InjectCostume(Stream source)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Injects the specified sound asset and return the internal file name of the asset.
        /// </summary>
        /// <param name="source">The source stream for the asset.</param>
        /// <returns>The internal file name of the asset.</returns>
        public string InjectSound(Stream source)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Takes all the inputted files and compiles them into an sb2 file.
        /// </summary>
        public void Compile()
        {
            // Check not previously compiled
            if (Compiled) throw new InvalidOperationException("Project already compiled");

            // Mark as compiled
            Compiled = true;

            // Don't bother compiling if compile errors were previously detected
            if (HasErrors) return;

            // Resolve module imports
            DoModuleImport();

            // Create translation context (Superglobal level)
            TranslationContext context = new TranslationContext(CompilerErrors);

            // Translate project into BlockModel representation
            ScratchProject = ChoopProject.Translate(context);

            // Convert BlockModel representation into json
            if (!HasErrors)
                ProjectJson = (JObject) ScratchProject.ToJson();
        }

        /// <summary>
        /// Saves the output SB2 file.
        /// </summary>
        /// <param name="filepath">The file path to save the file to.</param>
        public void Save(string filepath)
        {
            // Check compiled and no errors
            if (!Compiled) throw new InvalidOperationException("Project not compiled");
            if (HasErrors) throw new InvalidOperationException("Project has compiler errors");

            // TODO: generate + save entire sb2

            using (StreamWriter writer = new StreamWriter(filepath, false))
            {
                writer.Write(ProjectJson.ToString());
            }
        }

        /// <summary>
        /// Imports all the modules in the sprite.
        /// </summary>
        private void DoModuleImport()
        {
            foreach (SpriteDeclaration sprite in _builder.Project.Sprites)
            foreach (UsingStmt usingStmt in sprite.ImportedModules)
            {
                ModuleDeclaration module = _builder.Project.GetModule(usingStmt.Module);
                if (module != null)
                    sprite.Import(module);
                else
                    CompilerErrors.Add(new CompilerError($"Module '{usingStmt.Module}' is not defined",
                        ErrorType.NotDefined, usingStmt.ErrorToken, usingStmt.FileName));
            }
        }

        #endregion
    }
}