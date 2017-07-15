using System;
using System.Collections.ObjectModel;
using Antlr4.Runtime;
using Choop.Compiler.BlockModel;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents a for loop.
    /// </summary>
    public class ForLoop : IHasBody, IStatement
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
        /// Gets the expression for the counter start value.
        /// </summary>
        public IExpression Start { get; }

        /// <summary>
        /// Gets the expression for the counter end value.
        /// </summary>
        public IExpression End { get; }
        
        /// <summary>
        /// Gets the expression for the counter step value.
        /// </summary>
        public IExpression Step { get; }

        /// <summary>
        /// Gets the collection of statements within the loop.
        /// </summary>
        public Collection<IStatement> Statements { get; } = new Collection<IStatement>();
        
        /// <summary>
        /// Gets the token to report any compiler errors to.
        /// </summary>
        public IToken ErrorToken { get; }

        /// <summary>
        /// Gets the file name where the grammar structure was found.
        /// </summary>
        public string FileName { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="ForLoop"/> class.
        /// </summary>
        /// <param name="variable">The name of the counter variable.</param>
        /// <param name="varType">The data type of the counter variable.</param>
        /// <param name="start">The expression for the counter start value.</param>
        /// <param name="end">The expression for the counter end value.</param>
        /// <param name="step">The expression for the counter stop value. Default is +1.</param>
        public ForLoop(string variable, DataType varType, IExpression start, IExpression end, IExpression step = null)
        {
            Variable = variable;
            VarType = varType;
            Start = start;
            End = end;
            Step = step ?? new TerminalExpression("1", DataType.Number);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the translated code for the grammar structure.
        /// </summary>
        /// <returns>The translated code for the grammar structure.</returns>
        public Block[] Translate(TranslationContext context)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}