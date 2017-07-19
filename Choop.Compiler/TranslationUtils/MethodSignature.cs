namespace Choop.Compiler.TranslationUtils
{
    /// <summary>
    /// Represents the signature for an inbuilt common Scratch block.
    /// </summary>
    public class MethodSignature
    {
        #region Properties

        /// <summary>
        /// Gets the name of the method.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the number of inputs to the method.
        /// </summary>
        public int Inputs { get; }

        /// <summary>
        /// Gets whether the method reports a value.
        /// </summary>
        public bool IsReporter { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="MethodSignature"/> class.
        /// </summary>
        /// <param name="name">The name of the method.</param>
        /// <param name="inputs">The number of inputs to the method.</param>
        /// <param name="isReporter">Whether the method reports a value.</param>
        public MethodSignature(string name, int inputs, bool isReporter)
        {
            Name = name;
            Inputs = inputs;
            IsReporter = isReporter;
        }

        #endregion
    }
}