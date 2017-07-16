using System.Collections.ObjectModel;
using System.Drawing;
using Newtonsoft.Json.Linq;

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
        public Collection<object> Contents { get; }

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

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="List"/> class.
        /// </summary>
        /// <param name="name">The name of the list.</param>
        public List(string name)
        {
            Name = name;
            Contents = new Collection<object>();
        }

        /// <summary>
        /// Creates a new instance of the <see cref="List"/> class.
        /// </summary>
        /// <param name="name">The name of the list.</param>
        /// <param name="contents">The collection of default values within the list.</param>
        public List(string name, params object[] contents)
        {
            Name = name;
            Contents = new Collection<object>(contents);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Serializes the current instance into a JSON object.
        /// </summary>
        /// <returns>The JSON representation of the current instance.</returns>
        public JToken ToJson()
        {
            return new JObject
            {
                {"listName", Name},
                {"contents", new JArray(Contents)},
                {"isPersistent", Persistant},
                {"x", Location.X},
                {"y", Location.Y},
                {"width", Size.Width},
                {"height", Size.Height},
                {"visible", Visible}
            };
        }

        #endregion
    }
}