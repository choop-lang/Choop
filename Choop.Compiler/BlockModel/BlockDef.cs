using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Drawing;

namespace Choop.Compiler.BlockModel
{
    /// <summary>
    /// Represents a custom block definition.
    /// </summary>
    public class BlockDef : IScript, IComponent
    {
        #region Properties
        /// <summary>
        /// Gets the opcode of the custom block definition..
        /// </summary>
        public string Opcode {
            get { return "procDef"; }
            set { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets or sets the signature of the custom block.
        /// </summary>
        public string Spec { get; set; }

        /// <summary>
        /// Gets the collection of input names of the custom block.
        /// </summary>
        public ICollection<string> InputNames { get; } = new Collection<string>();

        /// <summary>
        /// Gets the collection of default values for each input of the custom block.
        /// </summary>
        public ICollection<object> DefaultValues { get; } = new Collection<object>();

        /// <summary>
        /// Gets or sets whether the custom block is atomic.
        /// </summary>
        public bool Atomic { get; set; }

        /// <summary>
        /// Gets or sets the display location of the script.
        /// </summary>
        public Point Location { get; set; }

        /// <summary>
        /// Gets the collection of blocks inside this script.
        /// </summary>
        public ICollection<Block> Blocks { get; } = new Collection<Block>();
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="EventHandler"/> class.
        /// </summary>
        /// <param name="atomic">Whether the block is atomic.</param>
        /// <param name="x">The x position of the script.</param>
        /// <param name="y">The y position of the script.</param>
        public BlockDef(bool atomic = false, int x = 0, int y = 0)
        {
            Location = new Point(x, y);
            Spec = "";
            Atomic = atomic;
        }
        #endregion
    }
}
