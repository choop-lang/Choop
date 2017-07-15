using Antlr4.Runtime;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents a paramter declaration.
    /// </summary>
    public class ParamDeclaration : ITypedDeclaration, IRule
    {
        #region Properties

        /// <summary>
        /// Gets the name of the parameter.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the type of the data stored in the parameter.
        /// </summary>
        public DataType Type { get; }

        /// <summary>
        /// Gets the default value for the parameter, if specified.
        /// </summary>
        public object Default { get; }

        /// <summary>
        /// Gets the token to report any compiler errors to.
        /// </summary>
        public IToken ErrorToken { get; }

        /// <summary>
        /// Gets the file name where the grammar structure was found.
        /// </summary>
        public string FileName { get; }

        /// <summary>
        /// Gets whether the parameter is optional.
        /// </summary>
        public bool IsOptional => Default != null;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="ParamDeclaration"/> class.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="type">The data type of the parameter.</param>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="errorToken">The token to report any compiler errors to.</param>
        /// <param name="default">The default value of the parameter.</param>
        public ParamDeclaration(string name, DataType type, string fileName, IToken errorToken, object @default = null)
        {
            Name = name;
            Type = type;
            FileName = fileName;
            ErrorToken = errorToken;
            Default = @default;
        }

        #endregion
    }
}