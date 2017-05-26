using System;
using System.Collections.ObjectModel;

namespace Choop.Compiler.ObjectModel
{
    /// <summary>
    /// Represents the signature for a sprite.
    /// </summary>
    public class SpriteSignature : ISpriteSignature
    {
        #region Properties

        /// <summary>
        /// Gets the name of the sprite.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the collection of constants that are local to the sprite.
        /// </summary>
        public Collection<ConstSignature> Constants { get; } = new Collection<ConstSignature>();

        /// <summary>
        /// Gets the collection of variables that are local to the sprite.
        /// </summary>
        public Collection<VarSignature> Variables { get; } = new Collection<VarSignature>();

        /// <summary>
        /// Gets the collection of arrays that are local to the sprite.
        /// </summary>
        public Collection<VarSignature> Arrays { get; } = new Collection<VarSignature>();

        /// <summary>
        /// Gets the collection of lists that are local to the sprite.
        /// </summary>
        public Collection<VarSignature> Lists { get; } = new Collection<VarSignature>();

        /// <summary>
        /// Gets the collection of event handlers in the sprite.
        /// </summary>
        public Collection<EventHandlerSignature> EventHandlers { get; } = new Collection<EventHandlerSignature>();

        /// <summary>
        /// Gets the collection of user-defined methods in the sprite.
        /// </summary>
        public Collection<MethodSignature> Methods { get; } = new Collection<MethodSignature>();

        /// <summary>
        /// Gets the project which contains the sprite.
        /// </summary>
        public Project Project { get; private set; }

        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="SpriteSignature"/> class. 
        /// </summary>
        /// <param name="name">The name of the sprite.</param>
        public SpriteSignature(string name)
        {
            Name = name;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Imports the specified module.
        /// </summary>
        /// <param name="module">The module to import.</param>
        public void Import(ModuleSignature module)
        {
            // Constants
            foreach (ConstSignature constant in module.Constants)
                Constants.Add(constant);

            // Variables
            foreach (VarSignature variable in module.Variables)
                Variables.Add(variable);

            // Arrays
            foreach (VarSignature array in module.Arrays)
                Arrays.Add(array);

            // Lists
            foreach (VarSignature list in module.Lists)
                Lists.Add(list);

            // Event handlers
            foreach (EventHandlerSignature scope in module.EventHandlers)
                EventHandlers.Add(scope);

            // Methods
            foreach (MethodSignature method in module.Methods)
                Methods.Add(method);
        }
        
        /// <summary>
        /// Registers the sprite with the specified project object. For internal use only.
        /// </summary>
        /// <param name="project">The project to register the sprite with.</param>
        /// <exception cref="InvalidOperationException"></exception>
        internal void Register(Project project)
        {
            // Check if previously registered
            if (Project != null)
                throw new InvalidOperationException("Sprite already registered to another project.");
            
            // Not previously registered
            Project = project;
        }
        #endregion
    }
}
