using System.Drawing;

namespace Choop.Compiler.BlockModel
{
    /// <summary>
    /// Represents a variable monitor.
    /// </summary>
    public class StageMonitor : IMonitor, IComponent
    {
        #region Properties
        /// <summary>
        /// Gets or sets the name of the stage or sprite to which the monitor refers.
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// Gets or sets the type of the monitor.
        /// </summary>
        public MonitorCmd Cmd { get; set; }

        /// <summary>
        /// Gets or sets the parameter for the monitor, referring to the type of the monitor.
        /// </summary>
        public object Param { get; set; }

        /// <summary>
        /// Gets or sets the color of the monitor.
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Gets or sets the label of the monitor.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the readout mode for the monitor. (Default is normal)
        /// </summary>
        public MonitorReadoutMode Mode { get; set; } = MonitorReadoutMode.Normal;

        /// <summary>
        /// Gets or sets the minimum value of the monitor's slider. (Default is 0)
        /// </summary>
        public double SliderMin { get; set; } = 0;

        /// <summary>
        /// Gets or sets the maximum value of the monitor's slider. (Default is 100)
        /// </summary>
        public double SliderMax { get; set; } = 100;

        /// <summary>
        /// Gets or sets whether the monitor's slider should only allow integer values. (Default is true)
        /// </summary>
        public bool Discrete { get; set; } = true;

        /// <summary>
        /// Gets or sets the location of the monitor.
        /// </summary>
        public Point Location { get; set; } = Point.Empty;

        /// <summary>
        /// Gets or sets whether the monitor is visible. (Default is false)
        /// </summary>
        public bool Visible { get; set; } = true;
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="StageMonitor"/> class. 
        /// </summary>
        public StageMonitor() {

        }
        #endregion
    }
}
