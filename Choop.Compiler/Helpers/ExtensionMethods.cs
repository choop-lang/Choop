using System;
using System.Security.Cryptography;
using System.Text;
using Antlr4.Runtime;
using Choop.Compiler.Antlr;
using Choop.Compiler.BlockModel;
using Choop.Compiler.ChoopModel;
using Choop.Compiler.ChoopModel.Assignments;
using Choop.Compiler.ChoopModel.Expressions;

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

        // AssignOperator

        /// <summary>
        /// Creates a compiler warning if the specified assignment operator cannot be used with the specified data type.
        /// </summary>
        /// <param name="operator">The assignment operator.</param>
        /// <param name="type">The target data type.</param>
        /// <param name="context">The translation context.</param>
        /// <param name="filename">The current file name.</param>
        /// <param name="errorToken">The token to report errors at.</param>
        public static void TestCompatible(this AssignOperator @operator, DataType type, TranslationContext context, string filename, IToken errorToken)
        {
            switch (@operator)
            {
                case AssignOperator.Equals:
                    return;

                case AssignOperator.AddEquals:
                case AssignOperator.MinusEquals:
                case AssignOperator.PlusPlus:
                case AssignOperator.MinusMinus:

                    if (!DataType.Number.IsCompatible(type))
                        context.ErrorList.Add(new CompilerError(
                            $"Cannot use operator '{@operator}' on a value of type '{type}'",
                            ErrorType.TypeMismatch, errorToken, filename));
                    return;

                case AssignOperator.DotEquals:

                    if (!DataType.String.IsCompatible(type))
                        context.ErrorList.Add(new CompilerError(
                            $"Cannot use operator '{@operator}' on a value of type '{type}'",
                            ErrorType.TypeMismatch, errorToken, filename));
                    return;

                default:
                    throw new ArgumentOutOfRangeException(nameof(@operator), @operator, null);
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

        // TerminalType

        /// <summary>
        /// Returns the simplified output type of the specified terminal type.
        /// </summary>
        /// <param name="type">The terminal type to get the output type of.</param>
        public static DataType GetOutputType(this TerminalType type)
        {
            switch (type)
            {
                case TerminalType.Bool:
                    return DataType.Boolean;

                case TerminalType.String:
                    return DataType.String;

                case TerminalType.Hex:
                case TerminalType.Scientific:
                case TerminalType.Decimal:
                case TerminalType.Int:
                    return DataType.Number;

                default:
                    throw new ArgumentOutOfRangeException(nameof(type));
            }
        }

        #endregion
    }
}