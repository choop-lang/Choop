﻿using System.Collections.Generic;
using System.Drawing;

namespace Choop.Compiler.BlockModel
{
    /// <summary>
    /// Represents a script.
    /// </summary>
    interface IScript
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
        ICollection<Block> Blocks { get; }
        #endregion
    }
}
