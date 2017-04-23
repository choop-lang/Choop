using System.Drawing;

namespace Choop.Compiler.BlockModel
{
    /// <summary>
    /// Represents a component in the workspace.
    /// </summary>
    interface IComponent
    {
        #region Properties
        /// <summary>
        /// Gets or sets the location of the component.
        /// </summary>
        Point Location { get; set; }
        #endregion
    }
}
