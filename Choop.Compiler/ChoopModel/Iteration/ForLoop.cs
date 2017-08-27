using System.Collections.Generic;
using System.Collections.ObjectModel;
using Antlr4.Runtime;
using Choop.Compiler.BlockModel;
using Choop.Compiler.ChoopModel.Expressions;
using Choop.Compiler.Helpers;

namespace Choop.Compiler.ChoopModel.Iteration
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
        public TerminalExpression Step { get; }

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
        /// <param name="fileName">The name of the file.</param>
        /// <param name="errorToken">The token to report any compiler errors to.</param>
        public ForLoop(string variable, DataType varType, IExpression start, IExpression end, TerminalExpression step,
            string fileName, IToken errorToken)
        {
            Variable = variable;
            VarType = varType;
            Start = start;
            End = end;
            FileName = fileName;
            ErrorToken = errorToken;
            Step = step ?? new TerminalExpression(1, DataType.Number);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the translated code for the grammar structure.
        /// </summary>
        /// <returns>The translated code for the grammar structure.</returns>
        public IEnumerable<Block> Translate(TranslationContext context)
        {
            // Create new scope
            Scope newScope = new Scope(context.CurrentScope);

            // Create translation context
            TranslationContext newContext = new TranslationContext(newScope, context);

            // Create counter variable
            StackValue counter = new StackValue(Variable, VarType, false);
            newScope.StackValues.Add(counter);

            // Create main loop contents
            List<Block> loopContents = new List<Block>();
            foreach (IStatement statement in Statements)
                loopContents.AddRange(statement.Translate(newContext));
            loopContents.Add(counter.CreateVariableIncrement(Step.Translate(newContext)));

            // Create output
            object startTranslated = Start.Balance().Translate(context);

            List<Block> output = new List<Block>();
            output.AddRange(counter.CreateDeclaration(startTranslated));
            output.Add(new Block(BlockSpecs.Repeat,
                new CompoundExpression(
                    CompoundOperator.Divide,
                    new CompoundExpression(CompoundOperator.Minus, End,
                        startTranslated is Block
                            ? new LookupExpression(counter, FileName, ErrorToken)
                            : Start, FileName, ErrorToken),
                    Step,
                    FileName,
                    ErrorToken
                ).Translate(newContext), loopContents.ToArray()));
            output.AddRange(newScope.CreateCleanUp());

            return output;
        }

        #endregion
    }
}