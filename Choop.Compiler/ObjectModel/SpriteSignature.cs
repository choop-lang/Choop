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
        private StageSignature stage;

        /// <summary>
        /// Gets or sets the name of the sprite.
        /// </summary>
        public string Name { get; set; }

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
        /// Gets the collection of scopes for the event handlers in the sprite.
        /// </summary>
        public Collection<Scope> EventHandlers { get; } = new Collection<Scope>();

        /// <summary>
        /// Gets the collection of user-defined methods in the sprite.
        /// </summary>
        public Collection<MethodSignature> Methods { get; } = new Collection<MethodSignature>();

        /// <summary>
        /// Gets the parent stage of the sprite.
        /// </summary>
        public StageSignature Stage
        {
            get { return stage; }
        }
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
        /// Finds the constant with the specified name within the sprite and stage.
        /// </summary>
        /// <param name="name">The name of the constant to search for.</param>
        /// <returns>The signature of the constant with the specified name; null if not found.</returns>
        public ConstSignature GetConstant(string name)
        {
            return GetItem(name, Constants, stage.Constants);
        }

        /// <summary>
        /// Finds the variable with the specified name within the sprite and stage.
        /// </summary>
        /// <param name="name">The name of the variable to search for.</param>
        /// <returns>The signature of the variable with the specified name; null if not found.</returns>
        public VarSignature GetVariable(string name)
        {
            return GetItem(name, Variables, stage.Variables);
        }

        /// <summary>
        /// Finds the array with the specified name within the sprite and stage.
        /// </summary>
        /// <param name="name">The name of the array to search for.</param>
        /// <returns>The signature of the array with the specified name; null if not found.</returns>
        public VarSignature GetArray(string name)
        {
            return GetItem(name, Arrays, stage.Arrays);
        }

        /// <summary>
        /// Finds the list with the specified name within the sprite and stage.
        /// </summary>
        /// <param name="name">The name of the list to search for.</param>
        /// <returns>The signature of the list with the specified name; null if not found.</returns>
        public VarSignature GetList(string name)
        {
            return GetItem(name, Lists, stage.Lists);
        }

        /// <summary>
        /// Finds the item with the specified name and signature type within the sprite and stage.
        /// </summary>
        /// <param name="name">The name of the item to search for.</param>
        /// <returns>The signature of the item with the specified name; null if not found.</returns>
        private T GetItem<T>(string name, Collection<T> locals, Collection<T> globals) where T : class, ITypedSignature
        {
            // Local
            foreach (T item in locals)
                if (item.Name.Equals(name, StageSignature.IdentifierComparisonMode))
                    return item;
            
            // Global
            foreach (T item in globals)
                if (item.Name.Equals(name, StageSignature.IdentifierComparisonMode))
                    return item;

            // Not found
            return null;
        }
        
        /// <summary>
        /// Registers the sprite with the specified stage object. For internal use only.
        /// </summary>
        /// <param name="stage">The stage to register the sprite with.</param>
        public void Register(StageSignature stage)
        {
            if (Stage == null)
            {
                // Not previously registered
                this.stage = stage;
            }
            else
            {
                // Previously registered
                throw new InvalidOperationException("Sprite already registered to another stage.");
            }
        }
        #endregion
    }
}
