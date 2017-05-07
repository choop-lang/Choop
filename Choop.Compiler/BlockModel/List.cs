using System.Collections.ObjectModel;
using System.Drawing;

namespace Choop.Compiler.BlockModel
{
    /// <summary>
    /// Represents a list and it's monitor.
    /// </summary>
    public class List : IVariable, IMonitor, IComponent
    {
        #region Properties
        /// <summary>
        /// Gets or sets the name of the list.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the collection of values in the list.
        /// </summary>
        public Collection<object> Contents { get; } = new Collection<object>();

        /// <summary>
        /// Gets or sets whether the list is a cloud list. (Default is false)
        /// </summary>
        public bool Persistant { get; set; } = false;

        /// <summary>
        /// Gets or sets the location of the list monitor.
        /// </summary>
        public Point Location { get; set; } = Point.Empty;

        /// <summary>
        /// Gets or sets the size of the list monitor.
        /// </summary>
        public Size Size { get; set; } = Size.Empty;

        /// <summary>
        /// Gets or sets whether the list monitor is visible. (Default is false)
        /// </summary>
        public bool Visible { get; set; } = false;
        #endregion
    }
}
