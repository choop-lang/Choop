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
        public Collection<IExpression> Parameters { get; } = new Collection<IExpression>();
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="MethodCall"/> class.
        /// </summary>
        public MethodCall(string methodName)
        {
            MethodName = methodName;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Gets the translated code for the grammar structure.
        /// </summary>
        /// <returns>The translated code for the grammar structure.</returns>
        public Block Translate()
        {
            throw new NotImplementedException();
        }
        
        Block[] ICompilable<Block[]>.Translate() => new[] { Translate() };
        #endregion
    }
}
