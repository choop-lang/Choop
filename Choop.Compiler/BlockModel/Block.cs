using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Choop.Compiler.BlockModel
{
    /// <summary>
    /// Represents a single block.
    /// </summary>
    class Block
    {
        #region Properties
        /// <summary>
        /// Gets or sets the opcode of the block.
        /// </summary>
        public string Opcode { get; set; }

        /// <summary>
        /// Gets the collection of arguments for the block.
        /// </summary>
        public ICollection<object> Args { get; } = new Collection<object>();
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="Block"/> class. 
        /// </summary>
        /// <param name="opcode">The opcode of the block.</param>
        public Block(string opcode)
        {
            Opcode = opcode;
        }
        #endregion
    }
}
