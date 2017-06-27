using System;
using System.Collections.ObjectModel;
using Choop.Compiler.BlockModel;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents an event handler.
    /// </summary>
    public class EventHandler : IDeclaration, ICompilable<Tuple<BlockModel.EventHandler, BlockDef>>, IHasBody
    {
        #region Properties
        /// <summary>
        /// Gets whether the event handler is unsafe.
        /// </summary>
        public bool Unsafe { get; }

        /// <summary>
        /// Gets whether the event handler is atomic.
        /// </summary>
        public bool Atomic { get; }

        /// <summary>
        /// Gets the name of the event being handled.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the parameter for the event handler, if necessary.
        /// </summary>
        public TerminalExpression Parameter { get; }
        
        /// <summary>
        /// Gets the collection of statements within the method.
        /// </summary>
        public Collection<IStatement> Statements { get; } = new Collection<IStatement>();
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="EventHandler"/> class.
        /// </summary>
        /// <param name="name">The name of the event being handled.</param>
        /// <param name="parameter">The parameter for the event handler, if necessary.</param>
        /// <param name="unsafe">Whether the event handler is unsafe.</param>
        /// <param name="atomic">Whether the event handler is atomic.</param>
        public EventHandler(string name, TerminalExpression parameter, bool @unsafe, bool atomic)
        {
            Name = name;
            Parameter = parameter;
            Unsafe = @unsafe;
            Atomic = atomic;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Gets the translated code for the grammar structure.
        /// </summary>
        /// <returns>The translated code for the grammar structure.</returns>
        public Tuple<BlockModel.EventHandler, BlockDef> Translate()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
