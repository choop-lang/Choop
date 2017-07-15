using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Antlr4.Runtime;
using Choop.Compiler.BlockModel;
using Choop.Compiler.TranslationUtils;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents an entire Choop project.
    /// </summary>
    public class Project : ISpriteDeclaration, ICompilable<ProjectInfo>
    {
        #region Properties

        /// <summary>
        /// Gets the name of the project.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the collection of superglobal constant declarations. (Not compiled)
        /// </summary>
        public Collection<ConstDeclaration> Constants { get; } = new Collection<ConstDeclaration>();

        /// <summary>
        /// Gets the collection of superglobal variable declarations.
        /// </summary>
        public Collection<GlobalVarDeclaration> Variables { get; } = new Collection<GlobalVarDeclaration>();

        /// <summary>
        /// Gets the collection of superglobal list declarations.
        /// </summary>
        public Collection<GlobalListDeclaration> Lists { get; } = new Collection<GlobalListDeclaration>();

        /// <summary>
        /// Gets or sets the stage sprite in the project.
        /// </summary>
        public StageDeclaration Stage { get; set; }

        /// <summary>
        /// Gets the collection of sprites in the project.
        /// </summary>
        public Collection<SpriteDeclaration> Sprites { get; } = new Collection<SpriteDeclaration>();

        /// <summary>
        /// Gets the collection of modules in the project.
        /// </summary>
        public Collection<ModuleDeclaration> Modules { get; } = new Collection<ModuleDeclaration>();

        /// <summary>
        /// Unused. Gets the collection of event handlers.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Collection<EventHandler> EventHandlers => null;

        /// <summary>
        /// Unused. Gets the collection of method declarations.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Collection<MethodDeclaration> Methods => null;

        /// <summary>
        /// Gets the token to report any compiler errors to.
        /// </summary>
        /// <exception cref="NotSupportedException"></exception>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public IToken ErrorToken => throw new NotSupportedException();

        /// <summary>
        /// Gets the file name where the grammar structure was found.
        /// </summary>
        /// <exception cref="NotSupportedException"></exception>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string FileName => throw new NotSupportedException();

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="Project"/> class.
        /// </summary>
        /// <param name="name">The name of the project.</param>
        public Project(string name)
        {
            Name = name;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Finds the constant with the specified name within the project superglobals.
        /// </summary>
        /// <param name="name">The name of the constant to search for.</param>
        /// <returns>The declaration of the constant with the specified name; null if not found.</returns>
        public ConstDeclaration GetConstant(string name) => GetItem(name, Constants);

        /// <summary>
        /// Finds the variable with the specified name within the project superglobals.
        /// </summary>
        /// <param name="name">The name of the variable to search for.</param>
        /// <returns>The declaration of the variable with the specified name; null if not found.</returns>
        public GlobalVarDeclaration GetVariable(string name) => GetItem(name, Variables);

        /// <summary>
        /// Finds the list or array with the specified name within the project superglobals.
        /// </summary>
        /// <param name="name">The name of the list to search for.</param>
        /// <returns>The declaration of the list with the specified name; null if not found.</returns>
        public GlobalListDeclaration GetList(string name) => GetItem(name, Lists);

        /// <summary>
        /// Finds the sprite with the specified name.
        /// </summary>
        /// <param name="name">The name of the sprite to search for.</param>
        /// <returns>The declaration of the sprite with the specified name; null if not found.</returns>
        public SpriteDeclaration GetSprite(string name) => GetItem(name, Sprites);

        /// <summary>
        /// Finds the module with the specified name.
        /// </summary>
        /// <param name="name">The name of the module to search for.</param>
        /// <returns>The declaration of the module with the specified name; null if not found.</returns>
        public ModuleDeclaration GetModule(string name) => GetItem(name, Modules);

        /// <summary>
        /// Finds the declaration with the specified name at the project level.
        /// </summary>
        /// <param name="name">The name of the declaration to search for.</param>
        /// <returns>The declaration with the specified name; null if not found.</returns>
        public IDeclaration GetDeclaration(string name) => GetItem(name, Constants) ??
                                                           GetItem(name, Variables) ??
                                                           GetItem(name, Lists) ??
                                                           GetItem(name, Sprites) ??
                                                           (IDeclaration) GetItem(name, Modules);

        /// <summary>
        /// Finds the item with the specified name and type within the project superglobals.
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

        /// <summary>
        /// Gets the translated code for the grammar structure.
        /// </summary>
        /// <returns>The translated code for the grammar structure.</returns>
        public ProjectInfo Translate(TranslationContext context)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}