namespace Choop.Compiler.ObjectModel
{
    /// <summary>
    /// Represents the signature for a method parameter.
    /// </summary>
    public class ParamSignature : VarSignature
    {
        #region Properties
        /// <summary>
        /// Gets or sets whether the parameter is optional.
        /// </summary>
        public bool Optional { get; set; }
        #endregion
    }
}
