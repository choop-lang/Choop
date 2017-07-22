using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        public Block(string opcode)
        {
            Opcode = opcode;
            Args = new Collection<object>();
        }

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
                array.Add(RecurisveTranslate(arg));

            return array;
        }

        /// <summary>
        /// Recursively translates an object into json.
        /// </summary>
        /// <param name="arg">The object to translate.</param>
        /// <returns>The translated Json arg.</returns>
        private static JToken RecurisveTranslate(object arg)
        {
            // Try Json convertible
            IJsonConvertable jsonArg = arg as IJsonConvertable;
            if (jsonArg != null)
                return jsonArg.ToJson();

            // Try collection
            IEnumerable<object> arrayArg = arg as IEnumerable<object>;
            if (arrayArg != null)
                return new JArray(arrayArg.Select(x => (object)RecurisveTranslate(x)).ToArray());

            // Try object value
            return new JValue(arg);
        }

        #endregion
    }
}