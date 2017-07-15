using System.Collections.ObjectModel;
using System.Drawing;

namespace Choop.Compiler.BlockModel
{
    /// <summary>
    /// Represents a script.
    /// </summary>
    public interface IScript : IJsonConvertable
    {
        #region Properties

        /// <summary>
        /// Gets or sets the opcode of the script hat block.
        /// </summary>
        string Opcode { get; set; }

        /// <summary>
        /// Gets or sets the display location of the script.
        /// </summary>
        Point Location { get; set; }

        /// <summary>
        /// Gets the collection of blocks inside this script.
        /// </summary>
        Collection<Block> Blocks { get; }

        #endregion
    }
}