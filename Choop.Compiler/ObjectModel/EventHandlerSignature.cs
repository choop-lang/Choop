namespace Choop.Compiler.ObjectModel
{
    /// <summary>
    /// Represents a signature for an event handler.
    /// </summary>
    public class EventHandlerSignature : IMethodSignature
    {
        #region Properties
        /// <summary>
        /// Gets the name of the event being handled.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the scope for the event handler.
        /// </summary>
        public Scope MainScope { get; } = new Scope();
        #endregion
    }
}
