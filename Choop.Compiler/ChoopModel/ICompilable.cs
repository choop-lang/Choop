using Choop.Compiler.Helpers;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents a Choop grammar structure that can be translated to Scratch blocks.
    /// </summary>
    /// <typeparam name="T">The output type of the translated code.</typeparam>
    public interface ICompilable<out T> : IRule
    {
        #region Methods

        /// <summary>
        /// Gets the translated code for the grammar structure.
        /// </summary>
        /// <returns>The translated code for the grammar structure.</returns>
        T Translate(TranslationContext context);

        #endregion
    }
}