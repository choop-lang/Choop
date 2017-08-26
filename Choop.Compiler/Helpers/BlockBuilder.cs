using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Choop.Compiler.BlockModel;
using Choop.Compiler.ChoopModel;
using Choop.Compiler.ChoopModel.Expressions;

namespace Choop.Compiler.Helpers
{
    /// <summary>
    /// Simplifies the translation from statements to blocks.
    /// </summary>
    public class BlockBuilder
    {
        #region Fields

        /// <summary>
        /// The context for translating expressions.
        /// </summary>
        private TranslationContext _context;

        /// <summary>
        /// The primary block being produced.
        /// </summary>
        private Block _block;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the opcode of the block being constructed.
        /// </summary>
        public string Opcode => _block.Opcode;

        /// <summary>
        /// Gets the collection of translated args.
        /// </summary>
        public Collection<object> Args => _block.Args;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="BlockBuilder"/>.
        /// </summary>
        /// <param name="opcode">The opcode of the block.</param>
        /// <param name="context">The current translation context.</param>
        public BlockBuilder(string opcode, TranslationContext context)
        {
            _block = new Block(opcode);
            _context = new TranslationContext(_block, context);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a parameter value to the block.
        /// </summary>
        /// <param name="translatedParam">The parameter to add, which has already been translated.</param>
        /// <returns>The current instance, after the parameter has been added.</returns>
        public BlockBuilder AddParam(object translatedParam)
        {
            Args.Add(translatedParam);

            return this;
        }

        /// <summary>
        /// Adds a parameter value to the block.
        /// </summary>
        /// <param name="expressionParam">The parameter to add, which has neither been balanced or translated.</param>
        /// <returns>The current instance, after the parameter has been added.</returns>
        public BlockBuilder AddParam(IExpression expressionParam)
        {
            Args.Add(expressionParam != null ? expressionParam.Balance().Translate(_context) : "");

            return this;
        }

        /// <summary>
        /// Adds a collection parameter values to the block.
        /// </summary>
        /// <param name="translatedParams">The parameters to add, which have already been translated.</param>
        /// <returns>The current instance, after the parameters have been added.</returns>
        public BlockBuilder AddParams(IEnumerable<object> translatedParams)
        {
            foreach (object translatedParam in translatedParams)
                Args.Add(translatedParam);

            return this;
        }

        /// <summary>
        /// Adds a collection parameter values to the block.
        /// </summary>
        /// <param name="expressionParams">The parameters to add, which have neither been balanced or translated.</param>
        /// <returns>The current instance, after the parameters have been added.</returns>
        public BlockBuilder AddParams(IEnumerable<IExpression> expressionParams)
        {
            foreach (IExpression expressionParam in expressionParams)
                Args.Add(expressionParam.Balance().Translate(_context));

            return this;
        }

        /// <summary>
        /// Returns the final translated block.
        /// </summary>
        /// <returns>The final translated block.</returns>
        public IEnumerable<Block> Create() => _context.Before.Concat(new[] {_block}).Concat(_context.After);

        #endregion
    }
}