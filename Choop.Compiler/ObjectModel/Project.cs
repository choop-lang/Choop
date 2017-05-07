using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Choop.Compiler.ObjectModel
{
    /// <summary>
    /// Represents a Choop project.
    /// </summary>
    public class Project
    {
        #region Constants
        /// <summary>
        /// Specifies how identifier names should be compared.
        /// </summary>
        public const StringComparison IdentifierComparisonMode = StringComparison.CurrentCulture;
        #endregion
        #region Properties
        private readonly ObservableCollection<SpriteSignature> _sprites = new ObservableCollection<SpriteSignature>();

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
        /// Gets the collection of sprites within the project.
        /// </summary>
        public Collection<SpriteSignature> Sprites => _sprites;

        /// <summary>
        /// Gets the collection of modules within the project.
        /// </summary>
        public Collection<ModuleSignature> Modules { get; } = new Collection<ModuleSignature>();
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="Project"/> class.
        /// </summary>
        public Project()
        {
            _sprites.CollectionChanged += Sprites_CollectionChanged;
        }
        #endregion
        #region Methods
        private void Sprites_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action != NotifyCollectionChangedAction.Add) return;

            // Sprite added
            foreach (SpriteSignature sprite in e.NewItems)
                sprite.Register(this);
        }
        #endregion
    }
}
