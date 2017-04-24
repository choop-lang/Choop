namespace Choop.Compiler.BlockModel
{
    /// <summary>
    /// Specifies different readout modes for a <see cref="StageMonitor"/> .
    /// </summary>
    public enum MonitorReadoutMode
    {
        /// <summary>
        /// Indicates the normal readout.
        /// </summary>
        Normal,
        /// <summary>
        /// Indicates the large readout.
        /// </summary>
        Large,
        /// <summary>
        /// Indicates the slider readout, using the specified <see cref="StageMonitor.SliderMin"/> and <see cref="StageMonitor.SliderMax"/> values. 
        /// </summary>
        Slider
    }
}
