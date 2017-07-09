using System.Collections.ObjectModel;

namespace Choop.Compiler.BlockModel
{
    /// <summary>
    /// Represents a single block.
    /// </summary>
    public class Block
    {
        #region Properties

        /// <summary>
        /// Gets the opcode of the block.
        /// </summary>
        public string Opcode { get; }

        /// <summary>
        /// Gets the collection of arguments for the block.
        /// </summary>
        public Collection<object> Args { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="Block"/> class. 
        /// </summary>
        /// <param name="opcode">The opcode of the block.</param>
        /// <param name="args">The arguments for the block.</param>
        public Block(string opcode, params object[] args)
        {
            Opcode = opcode;
            Args = new Collection<object>(args);
        }

        #endregion
    }
}