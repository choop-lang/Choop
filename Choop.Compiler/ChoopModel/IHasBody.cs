using System.Collections.ObjectModel;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents a grammar structure that has contains a body of statements.
    /// </summary>
    public interface IHasBody
    {
        #region Properties

        /// <summary>
        /// Gets the collection of statements within the body.
        /// </summary>
        Collection<IStatement> Statements { get; }

        #endregion
    }
}