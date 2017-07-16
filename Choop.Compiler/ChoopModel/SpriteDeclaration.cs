using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using Antlr4.Runtime;
using Choop.Compiler.BlockModel;
using Choop.Compiler.TranslationUtils;

namespace Choop.Compiler.ChoopModel
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
        /// Gets the filepath of the Sprite metadata file for this sprite.
        /// </summary>
        public string MetaFile { get; }

        /// <summary>
        /// Gets the collection of names of modules imported by the sprite.
        /// </summary>
        public Collection<string> ImportedModules { get; } = new Collection<string>();

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
        /// <param name="metaFile">The file path to the metadata file for this sprite.</param>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="errorToken">The token to report any compiler errors to.</param>
        public SpriteDeclaration(string name, string metaFile, string fileName, IToken errorToken)
        {
            Name = name;
            MetaFile = metaFile;
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
        /// Finds the method which has the specified name and is compatible with the specified parameter types.
        /// </summary>
        /// <param name="name">The name of the method.</param>
        /// <param name="paramTypes">The types of each of the supplied parameters, in order.</param>
        /// <returns>The declaration of the method if found; otherwise null.</returns>
        public MethodDeclaration GetMethod(string name, params DataType[] paramTypes)
        {
            foreach (MethodDeclaration method in Methods)
            {
                // Check name matches
                if (!method.Name.Equals(name, Settings.IdentifierComparisonMode)) continue;
                // Check valid amount of parameters
                if (paramTypes.Length > method.Params.Count) continue;

                // Default to valid
                bool valid = true;

                // Check each parameter
                for (int i = 0; i < method.Params.Count; i++)
                {
                    if (i < paramTypes.Length)
                    {
                        // Check parameter types are compatible
                        if (method.Params[i].Type.IsCompatible(paramTypes[i])) continue;

                        // Not compatible
                        valid = false;
                        break;
                    }

                    // These parameters weren't specified, so they must be optional
                    if (method.Params[i].IsOptional) continue;

                    // Not optional
                    valid = false;
                    break;
                }

                // Return method if valid
                if (valid)
                    return method;
            }

            // Not found
            return null;
        }

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
            // Create new sprite instance
            // TODO use metafile
            Sprite sprite = new Sprite
            {
                Name = Name
            };

            // TODO: Import modules

            // Variables
            foreach (GlobalVarDeclaration globalVarDeclaration in Variables)
                sprite.Variables.Add(globalVarDeclaration.Translate(context));

            // Lists
            foreach (GlobalListDeclaration globalListDeclaration in Lists)
                sprite.Lists.Add(globalListDeclaration.Translate(context));

            // Events
            foreach (EventHandler eventHandler in EventHandlers)
            {
                Tuple<BlockModel.EventHandler, BlockDef> translated = eventHandler.Translate(context);
                sprite.Scripts.Add(translated.Item1);
                sprite.Scripts.Add(translated.Item2);
            }

            // Methods
            foreach (MethodDeclaration methodDeclaration in Methods)
                sprite.Scripts.Add(methodDeclaration.Translate(context));

            // Insert default costume
            // TODO use metafile
            sprite.Costumes.Add(new Costume
            {
                Name = "costume1",
                Id = 1,
                Md5 = "09dc888b0b7df19f70d81588ae73420e.svg",
                BitmapResolution = 1,
                RotationCenter = new Point(47, 55)
            });

            return sprite;
        }

        #endregion
    }
}