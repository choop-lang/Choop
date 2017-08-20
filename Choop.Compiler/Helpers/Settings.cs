using System;
using Choop.Compiler.BlockModel;

namespace Choop.Compiler.Helpers
{
    /// <summary>
    /// Specifies constants for use in the Choop model.
    /// </summary>
    public static class Settings
    {
        #region Constants

        /// <summary>
        /// Specifies how identifier names should be compared.
        /// </summary>
        public const StringComparison IdentifierComparisonMode = StringComparison.CurrentCulture;

        /// <summary>
        /// Gets the name of the stage.
        /// </summary>
        public const string StageName = "Stage";

        /// <summary>
        /// Gets the name of the stack reference parameter.
        /// </summary>
        public const string StackRefParam = "@stackRef";

        /// <summary>
        /// Gets the name of the stack offset parameter.
        /// </summary>
        public const string StackOffsetParam = "@stackOffset";

        /// <summary>
        /// Gets the name of the current stack variable.
        /// </summary>
        public const string CurrentStackVar = "@CurrentStack";

        /// <summary>
        /// The file extension for Choop project files.
        /// </summary>
        public const string ChoopProjFileExt = ".chp";

        /// <summary>
        /// The file extension for Choop source files.
        /// </summary>
        public const string ChoopSourceFileExt = ".chs";

        /// <summary>
        /// The file extension for Choop definition files.
        /// </summary>
        public const string ChoopDefinitionFileExt = ".chd";

        /// <summary>
        /// The path of the project settings file.
        /// </summary>
        public const string ProjectSettingsFile = "project" + ChoopProjFileExt;

        /// <summary>
        /// The name of the json file in an SB2 archive.
        /// </summary>
        public const string ScratchJsonFile = "project.json";

        /// <summary>
        /// The file extension for PNG files.
        /// </summary>
        public const string PngExtension = ".png";

        /// <summary>
        /// The file extension for SVG files.
        /// </summary>
        public const string SvgExtension = ".svg";

        /// <summary>
        /// The file extension for WAV files.
        /// </summary>
        public const string WavExtension = ".wav";

        #endregion

        #region Fields

        /// <summary>
        /// The identifier to reference the scope stack.
        /// </summary>
        public static readonly Block StackIdentifier = new Block(BlockSpecs.GetParameter, StackRefParam);

        /// <summary>
        /// The identifier to reference the stack offset.
        /// </summary>
        public static readonly Block StackOffsetIdentifier = new Block(BlockSpecs.GetParameter, StackOffsetParam);

        #endregion
    }
}