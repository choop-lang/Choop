using System;

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

        /// <summary>
        /// Returns the default value for the specified data type.
        /// </summary>
        /// <param name="type">The type to get the default value for.</param>
        /// <returns>The default value for the specified data type.</returns>
        public static object GetDefault(this DataType type)
        {
            switch (type)
            {
                case DataType.Number:
                    return 0;
                case DataType.Boolean:
                    return false;
                case DataType.String:
                case DataType.Object:
                    return "";
                default:
                    throw new InvalidOperationException("Unknown data type");
            }
        }

        #endregion
    }
}