﻿using System;
using System.Collections.ObjectModel;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Choop.Compiler.Antlr;
using Choop.Compiler.BlockModel;
using Choop.Compiler.ChoopModel;
using Choop.Compiler.ChoopModel.Sprites;
using Choop.Compiler.Helpers;
using Choop.Compiler.Interfaces;
using Choop.Compiler.ProjectModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Choop.Compiler
{
    /// <summary>
    /// Compiles Choop code.
    /// </summary>
    public class ChoopCompiler : IDisposable
    {
        #region Fields

        /// <summary>
        /// The builder used for creating the internal Choop representation.
        /// </summary>
        private readonly ChoopBuilder _builder;

        /// <summary>
        /// The file provider.
        /// </summary>
        private readonly IFileProvider _fileProvider;

        /// <summary>
        /// The collection of loaded assets.
        /// </summary>
        private readonly AssetCollection _assets = new AssetCollection();

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of the Choop project.
        /// </summary>
        public string ProjectName
        {
            get => _builder.Project.Name;
            set => _builder.Project.Name = value;
        }

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
        /// <param name="projectPath">The path of the project to load.</param>
        /// <param name="fileProvider">The provider for files.</param>
        public ChoopCompiler(string projectPath, IFileProvider fileProvider)
        {
            _builder = new ChoopBuilder(CompilerErrors);
            _fileProvider = fileProvider;

            LoadProject(projectPath);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Loads the Choop project at the specified path.
        /// </summary>
        /// <param name="projectPath">The path of the project to load.</param>
        private void LoadProject(string projectPath)
        {
            // Init file provider
            _fileProvider.OpenProject(projectPath);

            // Get project.chp file
            using (StreamReader projectReader = new StreamReader(_fileProvider.GetFileReadStream(Settings.ProjectSettingsFile)))
                ChoopProject.Settings = JsonConvert.DeserializeObject<ProjectSettings>(projectReader.ReadToEnd());

            foreach (ChoopFile file in ChoopProject.Settings.Files)
            {
                switch (file.BuildAction)
                {
                    case BuildAction.Ignore:
                        // No action
                        break;

                    case BuildAction.SourceCode:
                        using (Stream stream = _fileProvider.GetFileReadStream(file.Path))
                            InjectCode(stream, file.Path);
                        break;

                    case BuildAction.SpriteDefinition:
                        using (Stream stream = _fileProvider.GetFileReadStream(file.Path))
                            InjectSpriteDefinition(file.Path, stream);
                        break;

                    case BuildAction.CostumeAsset:
                        using (Stream stream = _fileProvider.GetFileReadStream(file.Path))
                            InjectCostume(file.Path, stream);
                        break;

                    case BuildAction.SoundAsset:
                        using (Stream stream = _fileProvider.GetFileReadStream(file.Path))
                            InjectSound(file.Path, stream);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            // Close file provider
            _fileProvider.CloseProject();
        }

        /// <summary>
        /// Injects the raw Choop code from the specified stream into the Choop project.
        /// </summary>
        /// <param name="input">The input stream containing the raw Choop code to compile.</param>
        /// <param name="fileName">The name of the file the source code came from, used for error messages. Optional.</param>
        public void InjectCode(Stream input, string fileName = null)
        {
            InjectCode(new AntlrInputStream(input), fileName);
        }

        /// <summary>
        /// Injects the raw Choop code from the specified string into the Choop project.
        /// </summary>
        /// <param name="input">The raw Choop code to compile.</param>
        /// <param name="fileName">The name of the file the source code came from, used for error messages. Optional.</param>
        public void InjectCode(string input, string fileName = null)
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
            if (Compiled) throw new InvalidOperationException("Project already compiled");

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
        /// Injects the specified sprite definition file.
        /// </summary>
        /// <param name="path">The file path used to reference the definition file.</param>
        /// <param name="source">The source stream of the definition file.</param>
        public void InjectSpriteDefinition(string path, Stream source)
        {
            // Check if already compiled
            if (Compiled) throw new InvalidOperationException("Project already compiled");

            using (StreamReader reader = new StreamReader(source))
            {
                _assets.SpriteDefinitionFiles.Add(path,
                    JsonConvert.DeserializeObject<SpriteSettings>(reader.ReadToEnd()));
            }
        }

        /// <summary>
        /// Injects the specified image asset.
        /// </summary>
        /// <param name="path">The file path used to reference the asset.</param>
        /// <param name="source">The source stream for the asset.</param>
        public void InjectCostume(string path, Stream source)
        {
            // Check if already compiled
            if (Compiled) throw new InvalidOperationException("Project already compiled");

            using (MemoryStream ms = new MemoryStream())
            {
                source.CopyTo(ms);
                _assets.CostumeFiles.Add(path,
                    new LoadedAsset(ms.ToArray(), Path.GetExtension(path), _assets.CostumeFiles.Count + 1));
            }
        }

        /// <summary>
        /// Injects the specified sound asset.
        /// </summary>
        /// <param name="path">The file path used to reference the asset.</param>
        /// <param name="source">The source stream for the asset.</param>
        public void InjectSound(string path, Stream source)
        {
            // Check if already compiled
            if (Compiled) throw new InvalidOperationException("Project already compiled");

            using (MemoryStream ms = new MemoryStream())
            {
                source.CopyTo(ms);
                _assets.SoundFiles.Add(path,
                    new LoadedAsset(ms.ToArray(), Path.GetExtension(path), _assets.SoundFiles.Count));
            }
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
            TranslationContext context = new TranslationContext(CompilerErrors, _assets);

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

            // Create zip file
            using (FileStream fs = File.Create(filepath))
            using (ZipArchive archive = new ZipArchive(fs, ZipArchiveMode.Create, true))
            {
                // Save project.json
                using (Stream jsonStream = archive.CreateEntry(Settings.ScratchJsonFile, CompressionLevel.Optimal)
                    .Open())
                using (StreamWriter writer = new StreamWriter(jsonStream))
                    writer.Write(ProjectJson.ToString());

                // Save pen layer
                using (Stream penLayerStream = archive
                    .CreateEntry(ScratchProject.PenLayer + Settings.PngExtension, CompressionLevel.Fastest).Open())
                    ChoopProject.Settings.PenLayerImage.Save(penLayerStream, ImageFormat.Png);

                // Save costumes
                foreach (LoadedAsset costumeFile in _assets.CostumeFiles.Values)
                {
                    using (Stream costumeStream = archive.CreateEntry(costumeFile.Id + costumeFile.Extension).Open())
                    using (BinaryWriter writer = new BinaryWriter(costumeStream))
                        writer.Write(costumeFile.Contents);
                }

                // Save sounds
                foreach (LoadedAsset soundFile in _assets.SoundFiles.Values)
                {
                    using (Stream soundStream = archive.CreateEntry(soundFile.Id + soundFile.Extension).Open())
                    using (BinaryWriter writer = new BinaryWriter(soundStream))
                        writer.Write(soundFile.Contents);
                }
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

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _fileProvider?.Dispose();
        }

        #endregion
    }
}