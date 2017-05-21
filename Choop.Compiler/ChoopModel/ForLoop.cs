using System;
using System.Collections.ObjectModel;
using Choop.Compiler.BlockModel;
using Choop.Compiler.ObjectModel;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents a for loop.
    /// </summary>
    public class ForLoop : IHasBody, IStatement
    {
        #region Properties
        /// <summary>
        /// Gets the counter declaration for the loop.
        /// </summary>
        public ScopedVarDeclaration CounterDeclaration { get; }

        /// <summary>
        /// Gets the expression for the stopping condition.
        /// </summary>
        public IExpression Condition { get; }

        /// <summary>
        /// Gets the counter increment statement.
        /// </summary>
        public VarAssignStmt IncrementStmt { get; }

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
        /// Creates a new instance of the <see cref="ForLoop"/> class.
        /// </summary>
        /// <param name="counterDeclaration">The declaration for the counter variable.</param>
        /// <param name="condition">The stopping condition for the loop.</param>
        /// <param name="incrementStmt">The counter increment statement.</param>
        /// <param name="parentScope">The parent scope of the declaration.</param>
        public ForLoop(ScopedVarDeclaration counterDeclaration, IExpression condition, VarAssignStmt incrementStmt, Scope parentScope)
        {
            CounterDeclaration = counterDeclaration;
            Condition = condition;
            IncrementStmt = incrementStmt;
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
