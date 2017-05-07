namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Provides extension methods for the <see cref="DataType"/> enumeration. 
    /// </summary>
    public static class DataTypeExtension
    {
        #region Methods
        /// <summary>
        /// Returns whether the specified data type is able to be stored within the current data type.
        /// </summary>
        /// <param name="type">The current data type.</param>
        /// <param name="other">The data type to store.</param>
        /// <returns>Whether the specified data type is able to be stored within the current data type.</returns>
        public static bool IsCompatible(this DataType type, DataType other)
        {
            return type == DataType.Object || type == other;
        }
        #endregion
    }
}
