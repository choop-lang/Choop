using System.Collections.Generic;
using System.Collections.ObjectModel;
using Antlr4.Runtime;
using Choop.Compiler.TranslationUtils;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents a sprite or stage declaration.
    /// </summary>
    public abstract class SpriteBaseDeclaration : ISpriteDeclaration, IRule
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
        /// Creates a new instance of the <see cref="SpriteBaseDeclaration"/> class.
        /// </summary>
        /// <param name="name">The name of the sprite.</param>
        /// <param name="metaFile">The file path to the metadata file for this sprite.</param>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="errorToken">The token to report any compiler errors to.</param>
        protected SpriteBaseDeclaration(string name, string metaFile, string fileName, IToken errorToken)
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
        private static T GetItem<T>(string name, IEnumerable<T> locals) where T : class, IDeclaration
        {
            // Local
            foreach (T item in locals)
                if (item.Name.Equals(name, Settings.IdentifierComparisonMode))
                    return item;

            // Not found
            return null;
        }

        #endregion
    }
}