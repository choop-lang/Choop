using Newtonsoft.Json.Linq;

namespace Choop.Compiler.BlockModel
{
    /// <summary>
    /// Represents a variable.
    /// </summary>
    public class Variable : IVariable
    {
        #region Properties

        /// <summary>
        /// Gets or sets the name of the variable.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value of the variable.
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Gets or sets whether the variable is a cloud variable. (Default is false)
        /// </summary>
        public bool Persistant { get; set; } = false;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="Variable"/> class.
        /// </summary>
        /// <param name="name">The name of the variable.</param>
        /// <param name="value">The initial value of the variable.</param>
        public Variable(string name, object value)
        {
            Name = name;
            Value = value;
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
                {"name", Name},
                {"value", new JValue(Value)},
                {"isPersistant", Persistant}
            };
        }

        #endregion
    }
}