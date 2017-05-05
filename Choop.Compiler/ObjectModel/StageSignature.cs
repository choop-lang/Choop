using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Choop.Compiler.ObjectModel
{
    /// <summary>
    /// Represents the signature for the stage.
    /// </summary>
    public class StageSignature : ISpriteSignature
    {
        #region Constants
        /// <summary>
        /// Specifies how identifier names should be compared.
        /// </summary>
        public const StringComparison IdentifierComparisonMode = StringComparison.CurrentCulture;
        #endregion
        #region Properties
        private ObservableCollection<SpriteSignature> sprites = new ObservableCollection<SpriteSignature>();

        /// <summary>
        /// Gets or sets the name of the stage. (Is always Stage)
        /// </summary>
        public string Name
        {
            get { return "Stage"; }
            set { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets the collection of global constants.
        /// </summary>
        public Collection<ConstSignature> Constants { get; } = new Collection<ConstSignature>();

        /// <summary>
        /// Gets the collection of global variables.
        /// </summary>
        public Collection<VarSignature> Variables { get; } = new Collection<VarSignature>();

        /// <summary>
        /// Gets the collection of global arrays.
        /// </summary>
        public Collection<VarSignature> Arrays { get; } = new Collection<VarSignature>();

        /// <summary>
        /// Gets the collection of global lists.
        /// </summary>
        public Collection<VarSignature> Lists { get; } = new Collection<VarSignature>();

        /// <summary>
        /// Gets the collection of scopes for the event handlers in the stage.
        /// </summary>
        public Collection<Scope> EventHandlers { get; } = new Collection<Scope>();

        /// <summary>
        /// Gets the collection of user-defined methods in the stage.
        /// </summary>
        public Collection<MethodSignature> Methods { get; } = new Collection<MethodSignature>();

        /// <summary>
        /// Gets the collection of sprites within the stage.
        /// </summary>
        public Collection<SpriteSignature> Sprites
        {
            get { return sprites; }
        }

        /// <summary>
        /// Gets the collection of modules within the stage.
        /// </summary>
        public Collection<ModuleSignature> Modules { get; } = new Collection<ModuleSignature>();
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new isntance of the <see cref="StageSignature"/> class.
        /// </summary>
        public StageSignature()
        {
            sprites.CollectionChanged += Sprites_CollectionChanged;
        }
        #endregion
        #region Methods
        private void Sprites_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                // Sprite added
                foreach (SpriteSignature sprite in e.NewItems)
                {
                    // Register sprite as child
                    sprite.Register(this);
                }
            }
        }
        #endregion
    }
}
