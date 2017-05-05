using System.Collections.ObjectModel;

namespace Choop.Compiler.ObjectModel
{
    /// <summary>
    /// Represents the base for a sprite signature.
    /// </summary>
    public interface ISpriteSignature : ISignature
    {
        #region Properties

        /// <summary>
        /// Gets the collection of constants that are local to the sprite.
        /// </summary>
        Collection<ConstSignature> Constants { get; }

        /// <summary>
        /// Gets the collection of variables that are local to the sprite.
        /// </summary>
        Collection<VarSignature> Variables { get; }

        /// <summary>
        /// Gets the collection of arrays that are local to the sprite.
        /// </summary>
        Collection<VarSignature> Arrays { get; }

        /// <summary>
        /// Gets the collection of lists that are local to the sprite.
        /// </summary>
        Collection<VarSignature> Lists { get; }

        /// <summary>
        /// Gets the collection of scopes for the event handlers in the sprite.
        /// </summary>
        Collection<Scope> EventHandlers { get; }

        /// <summary>
        /// Gets the collection of user-defined methods in the sprite.
        /// </summary>
        Collection<MethodSignature> Methods { get; }
        #endregion
    }
}
