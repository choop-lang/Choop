using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Choop.Compiler.BlockModel
{
    /// <summary>
    /// Represents a script tuple.
    /// </summary>
    public class ScriptTuple : IJsonConvertable
    {
        #region Properties

        /// <summary>
        /// Gets or sets the display location of the script.
        /// </summary>
        public Point Location { get; set; } = new Point(20, 20);

        /// <summary>
        /// Gets the collection of blocks inside this script.
        /// </summary>
        public Collection<Block> Blocks { get; } = new Collection<Block>();

        #endregion

        #region Methods

        /// <summary>
        /// Serializes the current instance into a JSON object.
        /// </summary>
        /// <returns>The JSON representation of the current instance.</returns>
        public JToken ToJson()
        {
            return new JArray
            {
                Location.X,
                Location.Y,
                new JArray(Blocks.Select<Block, object>(x => x.ToJson()).ToArray())
            };
        }

        #endregion
    }
}
