namespace Choop.Compiler.ObjectModel
{
    /// <summary>
    /// Represents the signature for a constant.
    /// </summary>
    public class ConstSignature : VarSignature
    {
        #region Properties
        /// <summary>
        /// Gets or sets the value of the constant.
        /// </summary>
        public object Value { get; set; }
        #endregion
    }
}
