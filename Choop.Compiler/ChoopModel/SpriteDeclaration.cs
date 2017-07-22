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
        /// Finds the method which has the specified name and can be called with the specified number of parameters.
        /// </summary>
        /// <param name="name">The name of the method.</param>
        /// <param name="params">The number of specified parameters.</param>
        /// <returns>The declaration of the method if found; otherwise null.</returns>
        public MethodDeclaration GetMethod(string name, int @params) => Methods.FirstOrDefault(
            method => method.Name.Equals(name, Settings.IdentifierComparisonMode) &&
                      (@params == method.Params.Count ||
                       @params < method.Params.Count && method.Params[@params + 1].IsOptional));

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
            // Create new sprite instance
            // TODO use metafile
            Sprite sprite = new Sprite
            {
                Name = Name
            };

            // TODO: Import modules

            // Create translation context
            TranslationContext newContext = new TranslationContext(this, context);

            // Variables
            foreach (GlobalVarDeclaration globalVarDeclaration in Variables)
                sprite.Variables.Add(globalVarDeclaration.Translate(newContext));

            // Lists
            foreach (GlobalListDeclaration globalListDeclaration in Lists)
                sprite.Lists.Add(globalListDeclaration.Translate(newContext));

            // Events
            foreach (EventHandler eventHandler in EventHandlers)
            {
                ScriptTuple[] translated = eventHandler.Translate(newContext);
                sprite.Scripts.Add(translated[0]);
                sprite.Scripts.Add(translated[1]);
            }

            // Methods
            foreach (MethodDeclaration methodDeclaration in Methods)
                sprite.Scripts.Add(methodDeclaration.Translate(newContext));

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