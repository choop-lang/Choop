using System;
using System.Collections.ObjectModel;
using Choop.Compiler.BlockModel;
using Choop.Compiler.ObjectModel;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents a foreach loop.
    /// </summary>
    public class ForeachLoop : IHasBody, IStatement
    {
        #region Properties
        /// <summary>
        /// Gets the name of the counter variable.
        /// </summary>
        public string Variable { get; }

        /// <summary>
        /// Gets the data type of the counter variable.
        /// </summary>
        public DataType VarType { get; }

        /// <summary>
        /// Gets the name of the source array for the foreach loop.
        /// </summary>
        public string SourceName { get; }

        /// <summary>
        /// Gets the collection of statements within the loop.
        /// </summary>
        public Collection<IStatement> Statements { get; } = new Collection<IStatement>();

        /// <summary>
        /// Gets the scope of the loop.
        /// </summary>
        public Scope Scope { get; }
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="ForeachLoop"/> class.
        /// </summary>
        /// <param name="variable">The name of the counter variable.</param>
        /// <param name="varType">The data type of the counter variable.</param>
        /// <param name="sourceName">The name of the source array for the loop.</param>
        /// <param name="parentScope">The parent scope of the declaration.</param>
        public ForeachLoop(string variable, DataType varType, string sourceName, Scope parentScope)
        {
            Variable = variable;
            VarType = varType;
            SourceName = sourceName;
            Scope = new Scope(parentScope);
        }
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
