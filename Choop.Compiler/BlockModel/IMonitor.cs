using System.Drawing;

namespace Choop.Compiler.BlockModel
{
    /// <summary>
    /// Represents a monitor on the stage.
    /// </summary>
    interface IMonitor
    {
        #region Properties
        /// <summary>
        /// Gets or sets the location of the monitor.
        /// </summary>
        Point Location { get; set; }

        /// <summary>
        /// Gets or sets whether the monitor is visible.
        /// </summary>
        bool Visible { get; set; }
        #endregion
    }
}
