using System;
using System.Collections.ObjectModel;
using Choop.Compiler.BlockModel;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents a full array assignment statement.
    /// </summary>
    public class ArrayReAssignStmt : IStatement
    {
        #region Properties

        /// <summary>
        /// Gets the name of the array being assigned.
        /// </summary>
        public string ArrayName { get; }

        /// <summary>
        /// Gets the new array contents.
        /// </summary>
        public Collection<IExpression> Items { get; } = new Collection<IExpression>();

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="VarAssignStmt"/> class.
        /// </summary>
        /// <param name="arrayName">The name of the array being assigned.</param>
        public ArrayReAssignStmt(string arrayName)
        {
            ArrayName = arrayName;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the translated code for the grammar structure.
        /// </summary>
        /// <returns>The translated code for the grammar structure.</returns>
        /// <remarks>
        /// Uses the delete and add method, as demonstrated in this project:
        /// https://scratch.mit.edu/projects/118629266/
        /// </remarks>
        public Block[] Translate()
        {
            Block[] blocks = new Block[1 + Items.Count];
            blocks[0] = new Block(BlockSpecs.DeleteItemOfList, "all", ArrayName);
            for (int i = 0; i < Items.Count; i++)
                blocks[i + 1] = new Block(BlockSpecs.AddToList, Items[i].Translate(), ArrayName);

            return blocks;
        }

        #endregion
    }
}