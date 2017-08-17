using System;
using System.Collections.ObjectModel;
using System.Linq;
using Antlr4.Runtime;
using Choop.Compiler.BlockModel;
using Choop.Compiler.TranslationUtils;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents a switch case statement.
    /// </summary>
    public class SwitchStmt : IStatement
    {
        #region Properties

        /// <summary>
        /// Gets the expression for the variable to switch on.
        /// </summary>
        public IExpression Variable { get; }

        /// <summary>
        /// Gets the collection of code blocks within the switch statement.
        /// </summary>
        /// <remarks>The code blocks should be in order, with the primary case first and the default case (if specified) last.</remarks>
        public Collection<ConditionalBlock> Blocks { get; } = new Collection<ConditionalBlock>();

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
        /// Creates a new instance of the <see cref="SwitchStmt"/> class.
        /// </summary>
        /// <param name="variable">The expression for the switch variable.</param>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="errorToken">The token to report any compiler errors to.</param>
        public SwitchStmt(IExpression variable, string fileName, IToken errorToken)
        {
            Variable = variable;
            FileName = fileName;
            ErrorToken = errorToken;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the translated code for the grammar structure.
        /// </summary>
        /// <returns>The translated code for the grammar structure.</returns>
        public Block[] Translate(TranslationContext context)
        {
            // Create variable holder
            StackValue variable = context.CurrentScope.CreateStackValue();
            Block[] declaration = variable.CreateDeclaration(context, Variable);

            // Translate main switch
            return declaration.Concat(BuildIfElse(context, new LookupExpression(variable, FileName, ErrorToken), 0))
                .ToArray();
        }

        /// <summary>
        /// Recursively builds the tranlsated if-else statement.
        /// </summary>
        /// <param name="context">The context of the translation.</param>
        /// <param name="variable">The translated variable to compare to.</param>
        /// <param name="element">The current block.</param>
        /// <returns>The translated if-else statement.</returns>
        private Block[] BuildIfElse(TranslationContext context, IExpression variable, int element)
        {
            if (element == Blocks.Count)
                throw new IndexOutOfRangeException("Element is out of range");

            if (element + 1 != Blocks.Count)
                return new BlockBuilder(BlockSpecs.IfThenElse, context)
                    .AddParam(BuildCondition(Blocks[element].Conditions, variable))
                    .AddParam(Blocks[element].Translate(context))
                    .AddParam(BuildIfElse(context, variable, element + 1))
                    .Create().ToArray();

            if (Blocks[element].IsDefault)
                return Blocks[element].Translate(context);

            return new BlockBuilder(BlockSpecs.IfThen, context)
                .AddParam(BuildCondition(Blocks[element].Conditions, variable))
                .AddParam(Blocks[element].Translate(context))
                .Create().ToArray();
        }

        /// <summary>
        /// Recursively builds the condition for a case block.
        /// </summary>
        /// <param name="conditions">The collection of input conditions.</param>
        /// <param name="variable">The translated variable to compare to.</param>
        /// <param name="index">The current index.</param>
        /// <returns>The expression for the condition combining all input conditions.</returns>
        private IExpression BuildCondition(Collection<Condition> conditions,
            IExpression variable, int index = 0) =>
            index == conditions.Count - 1
                ? new CompoundExpression(conditions[index].ComparisonOperator, variable, conditions[index].Expression,
                    FileName, ErrorToken)
                : new CompoundExpression(CompoundOperator.Or,
                    new CompoundExpression(conditions[index].ComparisonOperator, variable, conditions[index].Expression,
                        FileName, ErrorToken),
                    BuildCondition(conditions, variable, index + 1), FileName, ErrorToken);

        #endregion
    }
}