using Newtonsoft.Json.Linq;

namespace Choop.Compiler.BlockModel
{
    /// <summary>
    /// Represents an object that can be serialized to JSON.
    /// </summary>
    public interface IJsonConvertable
    {
        #region Methods

        /// <summary>
        /// Serializes the current instance into a JSON object.
        /// </summary>
        /// <returns>The JSON representation of the current instance.</returns>
        JToken ToJson();

        #endregion
    }
}