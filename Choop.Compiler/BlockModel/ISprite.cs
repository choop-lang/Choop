using System.Collections.Generic;

namespace Choop.Compiler.BlockModel
{
    /// <summary>
    /// Represents the base of sprite.
    /// </summary>
    interface ISprite
    {
        #region Properties
        /// <summary>
        /// Gets or sets the name of the <see cref="ISprite"/>. 
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets the collection of variables in the <see cref="ISprite"/>.
        /// </summary>
        ICollection<Variable> Variables { get; }

        /// <summary>
        /// Gets the collection of lists in the <see cref="ISprite"/>.
        /// </summary>
        ICollection<List> Lists { get; }

        /// <summary>
        /// Gets the collection of scripts in the <see cref="ISprite"/>. 
        /// </summary>
        ICollection<IScript> Scripts { get; }

        /// <summary>
        /// Gets the collection of script comments in the <see cref="ISprite"/>.
        /// </summary>
        ICollection<Comment> Comments { get; }

        /// <summary>
        /// Gets the collection of sounds in the <see cref="ISprite"/>.
        /// </summary>
        ICollection<Sound> Sounds { get; }

        /// <summary>
        /// Gets the collection of costumes in the <see cref="ISprite"/>.
        /// </summary>
        ICollection<Costume> Costumes { get; }

        /// <summary>
        /// Gets or sets the zerio-based index of the current costume of the <see cref="ISprite"/>. 
        /// </summary>
        int CurrentCostume { get; set; }
        #endregion
    }
}
