﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using Antlr4.Runtime;
using Choop.Compiler.BlockModel;
using Choop.Compiler.ChoopModel.Declarations;
using Choop.Compiler.ChoopModel.Methods;
using Choop.Compiler.Helpers;
using Choop.Compiler.ProjectModel;
using EventHandler = Choop.Compiler.ChoopModel.Methods.EventHandler;

namespace Choop.Compiler.ChoopModel.Sprites
{
    /// <summary>
    /// Represents a sprite declaration.
    /// </summary>
    public class SpriteDeclaration : ISpriteDeclaration, ICompilable<Sprite>
    {
        #region Properties

        /// <summary>
        /// Gets the name of the sprite.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the filepath of the sprite definition file for this sprite.
        /// </summary>
        public string DefinitionFile { get; }

        /// <summary>
        /// Gets the collection of names of modules imported by the sprite.
        /// </summary>
        public Collection<UsingStmt> ImportedModules { get; } = new Collection<UsingStmt>();

        /// <summary>
        /// Gets the collection of constant declarations. (Not compiled)
        /// </summary>
        public Collection<ConstDeclaration> Constants { get; } = new Collection<ConstDeclaration>();

        /// <summary>
        /// Gets the collection of variable declarations.
        /// </summary>
        public Collection<GlobalVarDeclaration> Variables { get; } = new Collection<GlobalVarDeclaration>();

        /// <summary>
        /// Gets the collection of list declarations.
        /// </summary>
        public Collection<GlobalListDeclaration> Lists { get; } = new Collection<GlobalListDeclaration>();

        /// <summary>
        /// Gets the collection of event handlers.
        /// </summary>
        public Collection<EventHandler> EventHandlers { get; } = new Collection<EventHandler>();

        /// <summary>
        /// Gets the collection of method declarations.
        /// </summary>
        public Collection<MethodDeclaration> Methods { get; } = new Collection<MethodDeclaration>();

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
        /// Creates a new instance of the <see cref="SpriteDeclaration"/> class.
        /// </summary>
        /// <param name="name">The name of the sprite.</param>
        /// <param name="definitionFile">The file path to the definition file for this sprite.</param>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="errorToken">The token to report any compiler errors to.</param>
        public SpriteDeclaration(string name, string definitionFile, string fileName, IToken errorToken)
        {
            Name = name;
            DefinitionFile = definitionFile;
            FileName = fileName;
            ErrorToken = errorToken;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Imports the specified module into the sprite.
        /// </summary>
        /// <param name="module">The module to import.</param>
        public void Import(ModuleDeclaration module)
        {
            // Constants
            foreach (ConstDeclaration constant in module.Constants)
                Constants.Add(constant);

            // Variables
            foreach (GlobalVarDeclaration variable in module.Variables)
                Variables.Add(variable);

            // Lists
            foreach (GlobalListDeclaration list in module.Lists)
                Lists.Add(list);

            // Event handlers
            foreach (EventHandler scope in module.EventHandlers)
                EventHandlers.Add(scope);

            // Methods
            foreach (MethodDeclaration method in module.Methods)
                Methods.Add(method);
        }

        /// <summary>
        /// Finds the method which has the specified name and can be called with the specified number of parameters.
        /// </summary>
        /// <param name="name">The name of the method.</param>
        /// <param name="params">The number of specified parameters.</param>
        /// <returns>The declaration of the method if found; otherwise null.</returns>
        public MethodDeclaration GetMethod(string name, int @params) => Methods.FirstOrDefault(
            method => method.Name.Equals(name, Settings.IdentifierComparisonMode) &&
                      (@params == method.Params.Count ||
                       @params < method.Params.Count && method.Params[@params].IsOptional));

        /// <summary>
        /// Gets a declaration that isn't a method with the specified name.
        /// </summary>
        /// <param name="name">The name of the declaration to search for.</param>
        /// <returns>The declaration with the specified name, null if not found.</returns>
        public ITypedDeclaration GetDeclaration(string name) => GetConstant(name) ??
                                                                GetVariable(name) ??
                                                                (ITypedDeclaration) GetList(name);

        /// <summary>
        /// Finds the constant with the specified name within the sprite.
        /// </summary>
        /// <param name="name">The name of the constant to search for.</param>
        /// <returns>The declaration of the constant with the specified name; null if not found.</returns>
        public ConstDeclaration GetConstant(string name) => GetItem(name, Constants);

        /// <summary>
        /// Finds the global variable with the specified name within the sprite.
        /// </summary>
        /// <param name="name">The name of the variable to search for.</param>
        /// <returns>The declaration of the variable with the specified name; null if not found.</returns>
        public GlobalVarDeclaration GetVariable(string name) => GetItem(name, Variables);

        /// <summary>
        /// Finds the global list or array with the specified name within the sprite.
        /// </summary>
        /// <param name="name">The name of the list to search for.</param>
        /// <returns>The declaration of the list with the specified name; null if not found.</returns>
        public GlobalListDeclaration GetList(string name) => GetItem(name, Lists);

        /// <summary>
        /// Finds the item with the specified name and type within the sprite.
        /// </summary>
        /// <param name="name">The name of the item to search for.</param>
        /// <param name="locals">The collection of local items to search inside.</param>
        /// <returns>The declaration of the item with the specified name; null if not found.</returns>
        private static T GetItem<T>(string name, IEnumerable<T> locals)
            where T : class, IDeclaration => locals.FirstOrDefault(
            item => item.Name.Equals(name, Settings.IdentifierComparisonMode));

        /// <summary>
        /// Gets the translated code for the grammar structure.
        /// </summary>
        /// <returns>The translated code for the grammar structure.</returns>
        public Sprite Translate(TranslationContext context)
        {
            // Find definition file
            if (!context.ProjectAssets.SpriteDefinitionFiles.TryGetValue(DefinitionFile,
                out SpriteSettings definitionFile))
            {
                context.ErrorList.Add(new CompilerError($"Definition file '{DefinitionFile}' could not be found",
                    ErrorType.FileNotFound, ErrorToken, FileName));
                return null;
            }

            // Create new sprite instance
            Sprite sprite = new Sprite
            {
                Name = Name,
                CurrentCostume = definitionFile.CostumeIndex,
                Direction = definitionFile.Direction,
                Draggable = definitionFile.Draggable,
                LibraryIndex = context.Project.Sprites.Count,
                Location = definitionFile.Location,
                RotationStyle = definitionFile.RotationStyle,
                Scale = definitionFile.Scale,
                Visible = definitionFile.Visible
            };

            // Create translation context
            TranslationContext newContext = new TranslationContext(this, context);

            // Variables
            foreach (GlobalVarDeclaration globalVarDeclaration in Variables)
                sprite.Variables.Add(globalVarDeclaration.Translate(newContext));

            // Lists
            foreach (GlobalListDeclaration globalListDeclaration in Lists)
                sprite.Lists.Add(globalListDeclaration.Translate(newContext));

            // Events
            foreach (ScriptTuple[] translated in EventHandlers.Select(x => x.Translate(newContext)))
            {
                sprite.Scripts.Add(translated[0]);
                sprite.Scripts.Add(translated[1]);
            }

            // Methods
            foreach (MethodDeclaration methodDeclaration in Methods)
                sprite.Scripts.Add(methodDeclaration.Translate(newContext));

            // Costumes
            foreach (Asset costume in definitionFile.Costumes)
            {
                // Find costume file
                if (!context.ProjectAssets.CostumeFiles.TryGetValue(costume.Path, out LoadedAsset costumeData))
                {
                    context.ErrorList.Add(new CompilerError($"Costume '{costume.Path}' could not be found",
                        ErrorType.FileNotFound, null, DefinitionFile));
                    break;
                }

                // Create costume

                // TODO resolution
                dynamic rotationCenter = costume.Attributes.rotationCenter;
                sprite.Costumes.Add(new Costume
                {
                    Name = costume.Name,
                    Id = costumeData.Id,
                    BitmapResolution = 1,
                    Md5 = costumeData.Contents.GetMd5Checksum() + costumeData.Extension,
                    RotationCenter = new Point((int)rotationCenter.x.Value, (int)rotationCenter.y.Value)
                });
            }

            // Sounds
            foreach (Asset sound in definitionFile.Sounds)
            {
                // Find sound file
                if (!context.ProjectAssets.SoundFiles.TryGetValue(sound.Path, out LoadedAsset soundData))
                {
                    context.ErrorList.Add(new CompilerError($"Sound '{sound.Path}' could not be found",
                        ErrorType.FileNotFound, null, DefinitionFile));
                    break;
                }

                // Create sound
                int sampleRate = BitConverter.ToInt32(soundData.Contents, 24);
                int bytesPerSample = BitConverter.ToInt16(soundData.Contents, 34) / 8;
                int sampleCount = BitConverter.ToInt32(soundData.Contents, 40) / bytesPerSample;

                sprite.Sounds.Add(new Sound
                {
                    Name = sound.Name,
                    Id = soundData.Id,
                    Md5 = soundData.Contents.GetMd5Checksum() + soundData.Extension,
                    Rate = sampleRate,
                    SampleCount = sampleCount
                });
            }

            return sprite;
        }

        #endregion
    }
}