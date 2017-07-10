using System;
using System.Collections.ObjectModel;
using Choop.Compiler.BlockModel;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents a method call.
    /// </summary>
    /// <remarks>Can be used inside an expression or as a standalone statement.</remarks>
    public class MethodCall : IExpression, IStatement
    {
        #region Properties

        /// <summary>
        /// Gets the name of the method being called.
        /// </summary>
        public string MethodName { get; }

        /// <summary>
        /// Gets the collection of parameters to the method.
        /// </summary>
        public Collection<IExpression> Parameters { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="MethodCall"/> class.
        /// </summary>
        /// <param name="methodName">The name of the method being called.</param>
        public MethodCall(string methodName)
        {
            MethodName = methodName;
            Parameters = new Collection<IExpression>();
        }

        /// <summary>
        /// Creates a new instance of the <see cref="MethodCall"/> class.
        /// </summary>
        /// <param name="methodName">The name of the method being called.</param>
        /// <param name="parameters">The parameters for the method.</param>
        public MethodCall(string methodName, params IExpression[] parameters)
        {
            MethodName = methodName;
            Parameters = new Collection<IExpression>(parameters);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the translated code for the grammar structure.
        /// </summary>
        /// <returns>The translated code for the grammar structure.</returns>
        public Block Translate(TranslationContext context)
        {
            throw new NotImplementedException();
        }

        Block[] ICompilable<Block[]>.Translate(TranslationContext context) => new[] {Translate(context)};

        #endregion
    }
}