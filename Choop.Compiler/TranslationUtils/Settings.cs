using System;
using Choop.Compiler.BlockModel;

namespace Choop.Compiler.TranslationUtils
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
        /// Gets the name of the current stack variable.
        /// </summary>
        public const string CurrentStackVar = "@CurrentStack";

        #endregion

        #region Fields

        /// <summary>
        /// The identifier to reference the scope stack.
        /// </summary>
        public static readonly Block StackIdentifier = new Block(BlockSpecs.GetParameter, StackRefParam);

        #endregion
    }
}