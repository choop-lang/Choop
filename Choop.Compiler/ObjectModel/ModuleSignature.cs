using System.Collections.ObjectModel;

namespace Choop.Compiler.ObjectModel
{
    /// <summary>
    /// Represents the signature for a module.
    /// </summary>
    public class ModuleSignature : ISpriteSignature
    {
        #region Properties
        /// <summary>
        /// Gets the name of the module.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the collection of constants that are local to the module.
        /// </summary>
        public Collection<ConstSignature> Constants { get; } = new Collection<ConstSignature>();

        /// <summary>
        /// Gets the collection of variables that are local to the module.
        /// </summary>
        public Collection<VarSignature> Variables { get; } = new Collection<VarSignature>();

        /// <summary>
        /// Gets the collection of arrays that are local to the module.
        /// </summary>
        public Collection<VarSignature> Arrays { get; } = new Collection<VarSignature>();

        /// <summary>
        /// Gets the collection of lists that are local to the module.
        /// </summary>
        public Collection<VarSignature> Lists { get; } = new Collection<VarSignature>();

        /// <summary>
        /// Gets the collection of event handlers in the module.
        /// </summary>
        public Collection<EventHandlerSignature> EventHandlers { get; } = new Collection<EventHandlerSignature>();

        /// <summary>
        /// Gets the collection of user-defined methods in the module.
        /// </summary>
        public Collection<MethodSignature> Methods { get; } = new Collection<MethodSignature>();
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="ModuleSignature"/> class.
        /// </summary>
        /// <param name="name">The name of the module.</param>
        public ModuleSignature(string name)
        {
            Name = name;
        }
        #endregion
    }
}
