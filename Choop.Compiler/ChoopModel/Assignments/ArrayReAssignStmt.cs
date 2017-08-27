﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using Antlr4.Runtime;
using Choop.Compiler.BlockModel;
using Choop.Compiler.ChoopModel.Expressions;
using Choop.Compiler.Helpers;

namespace Choop.Compiler.ChoopModel.Assignments
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
        /// Creates a new instance of the <see cref="VarAssignStmt"/> class.
        /// </summary>
        /// <param name="arrayName">The name of the array being assigned.</param>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="errorToken">The token to report any compiler errors to.</param>
        public ArrayReAssignStmt(string arrayName, string fileName, IToken errorToken)
        {
            ArrayName = arrayName;
            FileName = fileName;
            ErrorToken = errorToken;
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
        public Block[] Translate(TranslationContext context)
        {
            // TODO get declaration, stack values

            List<Block> blocks = new List<Block>(1 + Items.Count)
            {
                new Block(BlockSpecs.DeleteItemOfList, "all", ArrayName)
            };

            foreach (IExpression item in Items)
                blocks.AddRange(new BlockBuilder(BlockSpecs.AddToList, context)
                    .AddParam(item)
                    .AddParam(ArrayName)
                    .Create());

            return blocks.ToArray();
        }

        #endregion
    }
}