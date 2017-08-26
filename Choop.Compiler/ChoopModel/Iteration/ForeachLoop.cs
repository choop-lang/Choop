using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Antlr4.Runtime;
using Choop.Compiler.BlockModel;
using Choop.Compiler.ChoopModel.Declarations;
using Choop.Compiler.Helpers;

namespace Choop.Compiler.ChoopModel.Iteration
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
        /// Creates a new instance of the <see cref="ForeachLoop"/> class.
        /// </summary>
        /// <param name="variable">The name of the counter variable.</param>
        /// <param name="varType">The data type of the counter variable.</param>
        /// <param name="sourceName">The name of the source array for the loop.</param>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="errorToken">The token to report any compiler errors to.</param>
        public ForeachLoop(string variable, DataType varType, string sourceName, string fileName, IToken errorToken)
        {
            Variable = variable;
            VarType = varType;
            SourceName = sourceName;
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
            List<Block> output = new List<Block>();

            // Create scope of loop
            Scope innerScope = new Scope(context.CurrentScope);
            TranslationContext newContext = new TranslationContext(innerScope, context);

            // TODO: Inbuilt foreach block optimisation
            // TODO: Inline foreach

            // Create counter variable
            StackValue internalCounter = context.CurrentScope.CreateStackValue();
            output.AddRange(internalCounter.CreateDeclaration(1));

            // Create item variable
            StackValue itemVar = new StackValue(Variable, VarType, false);
            innerScope.StackValues.Add(itemVar);
            output.AddRange(itemVar.CreateDeclaration(VarType.GetDefault()));

            List<Block> loopContents = new List<Block>();

            GlobalListDeclaration globalList =
                context.CurrentSprite.GetList(SourceName) ?? context.Project.GetList(SourceName);
            StackValue arrayValue = null;

            if (globalList != null)
            {
                // Translate loop contents
                loopContents.Add(itemVar.CreateVariableAssignment(new Block(BlockSpecs.GetItemOfList,
                    internalCounter.CreateVariableLookup(), SourceName)));
            }
            else
            {
                // Get stackvalue for array
                arrayValue = context.CurrentScope.Search(SourceName);

                if (arrayValue == null)
                {
                    context.ErrorList.Add(new CompilerError($"Array '{SourceName}' is not defined", ErrorType.NotDefined,
                        ErrorToken, FileName));
                    return new Block[0];
                }

                loopContents.Add(itemVar.CreateVariableAssignment(arrayValue.CreateArrayLookup(internalCounter.CreateVariableLookup())));
            }

            // Increment counter
            loopContents.Add(internalCounter.CreateVariableIncrement(1));

            // Translate loop main body
            foreach (Block[] translated in Statements.Select(x => x.Translate(newContext)))
                loopContents.AddRange(translated);

            // Create loop Scratch block
            object repeats = globalList != null ? new Block(BlockSpecs.LengthOfList, SourceName) : (object)arrayValue.StackSpace;

            output.Add(new Block(BlockSpecs.Repeat, repeats, loopContents.ToArray()));

            // Clean up scope
            output.AddRange(internalCounter.CreateDestruction());
            output.AddRange(itemVar.CreateDestruction());

            return output.ToArray();
        }

        #endregion
    }
}