using System.Collections.ObjectModel;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents a sprite, stage or module declaration.
    /// </summary>
    public interface ISpriteDeclaration : IDeclaration
    {
        #region Properties
        /// <summary>
        /// Gets the collection of constant declarations. (Not compiled)
        /// </summary>
        Collection<ConstDeclaration> Constants { get; }

        /// <summary>
        /// Gets the collection of variable declarations.
        /// </summary>
        Collection<GlobalVarDeclaration> Variables { get; }

        /// <summary>
        /// Gets the collection of list declarations.
        /// </summary>
        Collection<GlobalListDeclaration> Lists { get; }

        /// <summary>
        /// Gets the collection of event handlers.
        /// </summary>
        Collection<EventHandler> EventHandlers { get; }

        /// <summary>
        /// Gets the collection of method declarations.
        /// </summary>
        Collection<MethodDeclaration> Methods { get; }
        #endregion
    }
}
