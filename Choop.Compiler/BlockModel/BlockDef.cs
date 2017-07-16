using System;
using System.Collections.ObjectModel;
using System.Drawing;
using Choop.Compiler.TranslationUtils;
using Newtonsoft.Json.Linq;

namespace Choop.Compiler.BlockModel
{
    /// <summary>
    /// Represents a custom block definition.
    /// </summary>
    public class BlockDef : IScript, IComponent
    {
        #region Properties

        /// <summary>
        /// Gets the opcode of the custom block definition..
        /// </summary>
        public string Opcode
        {
            get { return BlockSpecs.MethodDeclaration; }
            set { throw new NotSupportedException(); }
        }

        /// <summary>
        /// Gets or sets the signature of the custom block.
        /// </summary>
        public string Spec { get; set; }

        /// <summary>
        /// Gets the collection of input names of the custom block.
        /// </summary>
        public Collection<string> InputNames { get; } = new Collection<string>();

        /// <summary>
        /// Gets the collection of default values for each input of the custom block.
        /// </summary>
        public Collection<object> DefaultValues { get; } = new Collection<object>();

        /// <summary>
        /// Gets or sets whether the custom block is atomic.
        /// </summary>
        public bool Atomic { get; set; }

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
            JArray blocks = new JArray
            {
                new JArray(
                    Opcode,
                    Spec,
                    new JArray(InputNames),
                    new JArray(DefaultValues),
                    Atomic
                )
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