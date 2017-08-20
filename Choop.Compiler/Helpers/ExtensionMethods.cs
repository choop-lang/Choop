using System;
using System.Security.Cryptography;
using System.Text;
using Choop.Compiler.Antlr;
using Choop.Compiler.BlockModel;
using Choop.Compiler.ChoopModel;

namespace Choop.Compiler.Helpers
{
    /// <summary>
    /// Provides various extension methods for the Choop compiler unit.
    /// </summary>
    internal static class ExtensionMethods
    {
        #region Methods

        // TypeSpecifierContext

        /// <summary>
        /// Converts a <see cref="ChoopParser.TypeSpecifierContext"/> instance into a <see cref="DataType"/> value.
        /// </summary>
        /// <param name="typeSpecifierAst">The <see cref="ChoopParser.TypeSpecifierContext"/> instance to convert. Can be null.</param>
        /// <returns>The equivalent <see cref="DataType"/> for the <see cref="ChoopParser.TypeSpecifierContext"/> instance.</returns>
        internal static DataType ToDataType(this ChoopParser.TypeSpecifierContext typeSpecifierAst)
        {
            if (typeSpecifierAst == null)
                return DataType.Object;

            switch (typeSpecifierAst.start.Type)
            {
                case ChoopParser.TypeNum:
                    return DataType.Number;
                case ChoopParser.TypeString:
                    return DataType.String;
                case ChoopParser.TypeBool:
                    return DataType.Boolean;
                default:
                    throw new ArgumentException("Unknown type", nameof(typeSpecifierAst));
            }
        }

        // AssignOpContext

        /// <summary>
        /// Converts a <see cref="ChoopParser.AssignOpContext"/> instance into a <see cref="AssignOperator"/> value.
        /// </summary>
        /// <param name="assignOpAst">The <see cref="ChoopParser.AssignOpContext"/> instance to convert.</param>
        /// <returns>The equivalent <see cref="AssignOperator"/> for the <see cref="ChoopParser.AssignOpContext"/> instance.</returns>
        internal static AssignOperator ToAssignOperator(this ChoopParser.AssignOpContext assignOpAst)
        {
            if (assignOpAst == null) throw new ArgumentNullException(nameof(assignOpAst));

            switch (assignOpAst.start.Type)
            {
                case ChoopParser.Assign:
                    return AssignOperator.Equals;
                case ChoopParser.AssignAdd:
                    return AssignOperator.AddEquals;
                case ChoopParser.AssignSub:
                    return AssignOperator.MinusEquals;
                case ChoopParser.AssignConcat:
                    return AssignOperator.DotEquals;
                default:
                    throw new ArgumentException("Unknown type", nameof(assignOpAst));
            }
        }

        // RotationType

        /// <summary>
        /// Converts a <see cref="RotationType"/> object into its string form for JSON serialization.
        /// </summary>
        /// <param name="rotationType">The rotation type to convert.</param>
        /// <returns>The JSON string representing the rotation type.</returns>
        public static string ToSerializedString(this RotationType rotationType)
        {
            switch (rotationType)
            {
                case RotationType.LeftRight:
                    return "leftRight";
                case RotationType.Normal:
                    return "normal";
                case RotationType.None:
                    return "none";
                default:
                    throw new ArgumentOutOfRangeException(nameof(rotationType), rotationType, null);
            }
        }

        // Byte[]

        /// <summary>
        /// Returns the md5 checksum of a byte array.
        /// </summary>
        /// <param name="bytes">The bytes to create the checksum of.</param>
        /// <returns>The md5 checksum as a hexadecimal string.</returns>
        public static string GetMd5Checksum(this byte[] bytes)
        {
            // Hash the bytes
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] hash = md5.ComputeHash(bytes);

            // Convert to base 16 string
            StringBuilder sb = new StringBuilder();
            foreach (byte b in hash)
                sb.Append(b.ToString("x2").ToLower());

            return sb.ToString();
        }

        // DataType

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

        /// <summary>
        /// Returns the input notation for the specified <see cref="DataType"/> instance.
        /// </summary>
        /// <param name="type">The data type to get the input notation for.</param>
        /// <returns>The input notation for the specified <see cref="DataType"/> instance.</returns>
        public static string ToInputNotation(this DataType type)
        {
            switch (type)
            {
                case DataType.Object:
                    return BlockSpecs.InputString;
                case DataType.String:
                    return BlockSpecs.InputString;
                case DataType.Number:
                    return BlockSpecs.InputNum;
                case DataType.Boolean:
                    return BlockSpecs.InputBool;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        #endregion
    }
}