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
        /// Gets whether the method reports a value.
        /// </summary>
        public bool IsReporter { get; }

        /// <summary>
        /// The names of the inputs to the method, in order.
        /// </summary>
        public string[] Inputs { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="MethodSignature"/> class.
        /// </summary>
        /// <param name="name">The name of the method.</param>
        /// <param name="isReporter">Whether the method reports a value.</param>
        /// <param name="inputs">The names of inputs to the method, in order.</param>
        public MethodSignature(string name, bool isReporter, params string[] inputs)
        {
            Name = name;
            Inputs = inputs;
            IsReporter = isReporter;
        }

        #endregion
    }
}