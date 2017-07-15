using System.Collections.ObjectModel;
using Newtonsoft.Json.Linq;

namespace Choop.Compiler.BlockModel
{
    /// <summary>
    /// Represents a single block.
    /// </summary>
    public class Block : IJsonConvertable
    {
        #region Properties

        /// <summary>
        /// Gets the opcode of the block.
        /// </summary>
        public string Opcode { get; }

        /// <summary>
        /// Gets the collection of arguments for the block.
        /// </summary>
        public Collection<object> Args { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="Block"/> class. 
        /// </summary>
        /// <param name="opcode">The opcode of the block.</param>
        /// <param name="args">The arguments for the block.</param>
        public Block(string opcode, params object[] args)
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
            JArray array = new JArray(Opcode);
            foreach (object arg in Args)
                array.Add(arg);

            return array;
        }

        #endregion
    }
}