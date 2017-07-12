using System;

namespace Choop.Compiler.ChoopModel
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

        #endregion
    }
}