using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
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

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="ForeachLoop"/> class.
        /// </summary>
        /// <param name="variable">The name of the counter variable.</param>
        /// <param name="varType">The data type of the counter variable.</param>
        /// <param name="sourceName">The name of the source array for the loop.</param>
        public ForeachLoop(string variable, DataType varType, string sourceName)
        {
            Variable = variable;
            VarType = varType;
            SourceName = sourceName;
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
            Scope innerScope = new Scope(context.ParentScope);

            // Create counter variable
            StackValue internalCounter = new StackValue("@counter", DataType.Number);
            innerScope.StackValues.Add(internalCounter);
            output.AddRange(internalCounter.CreateDeclaration(1));

            // Create item variable
            StackValue itemVar = new StackValue(Variable, VarType);
            innerScope.StackValues.Add(itemVar);
            output.AddRange(itemVar.CreateDeclaration(VarType.GetDefault()));

            // TODO: optimise to use inbuilt foreach in case of global arrays and unsafe arrays

            // Get stackvalue for array
            StackValue arrayValue = null; // TODO
            
            // Translate loop contents
            List<Block> loopContents = new List<Block>
            {
                itemVar.CreateVariableAssignment(arrayValue.CreateArrayLookup(internalCounter.CreateVariableLookup())),
                itemVar.CreateVariableIncrement(1)
            };
            
            foreach (Block[] translated in Statements.Select(x => x.Translate()))
                loopContents.AddRange(translated);

            // Create loop Scratch block
            output.Add(new Block(BlockSpecs.Repeat, new Block(BlockSpecs.LengthOfList, SourceName), loopContents));

            // Clean up scope
            output.AddRange(internalCounter.CreateDestruction());
            output.AddRange(itemVar.CreateDestruction());

            return output.ToArray();
        }

        public Block[] Translate()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}