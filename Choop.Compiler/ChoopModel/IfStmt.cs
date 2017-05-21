using System;
using System.Collections.ObjectModel;
using Choop.Compiler.BlockModel;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents an if-else statement.
    /// </summary>
    public class IfStmt : IStatement
    {
        #region Properties
        /// <summary>
        /// Gets the collection of code blocks within the if-else statement.
        /// </summary>
        /// <remarks>The code blocks should be in order, with the primary case first and the default case (if specified) last.</remarks>
        public Collection<ConditionalBlock> Blocks { get; } = new Collection<ConditionalBlock>();
        #endregion
        #region Methods
        /// <summary>
        /// Gets the translated code for the grammar structure.
        /// </summary>
        /// <returns>The translated code for the grammar structure.</returns>
        public Block[] Translate()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
