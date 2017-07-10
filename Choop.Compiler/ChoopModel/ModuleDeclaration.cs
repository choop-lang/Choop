using System.Collections.ObjectModel;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents a module declaration.
    /// </summary>
    public class ModuleDeclaration : ISpriteDeclaration
    {
        #region Properties

        /// <summary>
        /// Gets the name of the module.
        /// </summary>
        public string Name { get; }

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

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="ModuleDeclaration"/> class.
        /// </summary>
        /// <param name="name">The name of the module.</param>
        public ModuleDeclaration(string name)
        {
            Name = name;
        }

        #endregion
    }
}