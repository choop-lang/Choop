namespace Choop.Compiler.BlockModel
{
    /// <summary>
    /// Represents a variable.
    /// </summary>
    class Variable : IVariable
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
        public Variable()
        {

        }
        #endregion
    }
}
