using System.Collections.ObjectModel;
using System.Drawing;
using Newtonsoft.Json.Linq;

namespace Choop.Compiler.BlockModel
{
    /// <summary>
    /// Represent a script that handles an event.
    /// </summary>
    public class EventHandler : IScript, IComponent
    {
        #region Properties

        /// <summary>
        /// Gets or sets the opcode of the script hat block.
        /// </summary>
        public string Opcode { get; set; }

        /// <summary>
        /// Gets the collection of arguments supplied to the script hat block.
        /// </summary>
        public Collection<object> Args { get; }

        /// <summary>
        /// Gets or sets the display location of the script.
        /// </summary>
        public Point Location { get; set; }

        /// <summary>
        /// Gets the collection of blocks inside this script.
        /// </summary>
        public Collection<Block> Blocks { get; } = new Collection<Block>();

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="EventHandler"/> class.
        /// </summary>
        /// <param name="opcode">The opcode of the event block.</param>
        /// <param name="args">The arguments of the event block.</param>
        public EventHandler(string opcode, params object[] args)
        {
            Opcode = opcode;
            Args = new Collection<object>(args);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Serializes the current instance into a JSON object.
        /// </summary>
        /// <returns>The JSON representation of the current instance.</returns>
        public JToken ToJson()
        {
            JArray eventBlock = new JArray
            {
                Opcode
            };

            foreach (object arg in Args)
                eventBlock.Add(arg);

            JArray blocks = new JArray
            {
                eventBlock
            };

            foreach (Block block in Blocks)
                blocks.Add(block.ToJson());

            return new JArray(
                Location.X,
                Location.Y,
                blocks);
        }

        #endregion
    }
}