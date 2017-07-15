using System;
using Choop.Compiler.BlockModel;
using Choop.Compiler.ChoopModel;

namespace Choop.Compiler.TranslationUtils
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

        #endregion
    }
}