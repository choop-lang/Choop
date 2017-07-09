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
            // Create scope of loop
            Scope innerScope = new Scope(context.ParentScope);

            // Create counter variable
            StackValue internalCounter = new StackValue("@counter", DataType.Number);
            innerScope.StackValues.Add(internalCounter);
            Block[] counterDeclaration = internalCounter.CreateDeclaration(1);

            // Create item variable
            StackValue itemVar = new StackValue(Variable, VarType);
            innerScope.StackValues.Add(itemVar);
            Block[] itemVarDeclaration = itemVar.CreateDeclaration(1);
            
            // Create loop Scratch block
            // TODO: Translate loop contents
            Block loop = new Block("doRepeat", new Block("lineCountOfList:", SourceName));

            // Clean up scope
            Block[] deleteCounter = internalCounter.CreateDestruction();
            Block[] deleteItemVar = itemVar.CreateDestruction();

            Block[] result = new Block[counterDeclaration.Length + itemVarDeclaration.Length + 1 +
                                       deleteCounter.Length + deleteItemVar.Length];

            int start = 0;
            Array.Copy(counterDeclaration, 0, result, start, counterDeclaration.Length);
            start += counterDeclaration.Length;
            Array.Copy(itemVarDeclaration, 0, result, start, itemVarDeclaration.Length);
            start += itemVarDeclaration.Length;
            result[start] = loop;
            start += 1;
            Array.Copy(deleteCounter, 0, result, start, deleteCounter.Length);
            start += deleteCounter.Length;
            Array.Copy(deleteItemVar, 0, result, start, deleteItemVar.Length);

            return result;
        }

        public Block[] Translate()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
