using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Drawing;

namespace Choop.Compiler.BlockModel
{
    /// <summary>
    /// Represent a script that handles an event.
    /// </summary>
    public class EventHandler : IScript, IComponent
    {
        #region Properties
        /// <summary>
        /// Gets or sets the opcode of the script hat block.
        /// </summary>
        public string Opcode { get; set; }

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
        /// <param name="opcode">The opcode of the event block.</param>
        /// <param name="x">The x position of the script.</param>
        /// <param name="y">The y position of the script.</param>
        public EventHandler(string opcode, int x = 0, int y = 0) {
            Opcode = opcode;
            Location = new Point(x, y);
        }
        #endregion
    }
}
