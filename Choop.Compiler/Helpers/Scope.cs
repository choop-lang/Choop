using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Choop.Compiler.BlockModel;
using Choop.Compiler.ChoopModel;

namespace Choop.Compiler.Helpers
{
    /// <summary>
    /// Represents a scope for variables.
    /// </summary>
    public class Scope
    {
        #region Fields

        /// <summary>
        /// The next scope ID to use.
        /// </summary>
        private static int _nextScopeID = 1;

        /// <summary>
        /// The next stak value ID to use.
        /// </summary>
        private static int _nextStackID = 1;

        #endregion

        #region Properties

        private readonly List<Scope> _childScopes = new List<Scope>();

        /// <summary>
        /// Gets the collection of values stored on the stack.
        /// </summary>
        public StackSegment StackValues { get; }

        /// <summary>
        /// Gets the unique ID for the scope, for use in unsafe mode.
        /// </summary>
        /// <remarks>ID is unique, not random.</remarks>
        public int ID { get; }

        /// <summary>
        /// Gets whether the scope has unsafe variables or not.
        /// </summary>
        public bool Unsafe { get; }

        /// <summary>
        /// Gets the parent scope of the current instance.
        /// </summary>
        public Scope Parent { get; }

        /// <summary>
        /// Gets the method that contains the scope.
        /// </summary>
        public IMethod Method { get; }

        /// <summary>
        /// Gets the collection of child scopes.
        /// </summary>
        public ReadOnlyCollection<Scope> ChildScopes => _childScopes.AsReadOnly();

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="Scope"/> class.
        /// </summary>
        /// <param name="method">Gets the method that contains the scope.</param>
        /// <param name="unsafe">Whether the scope contains unsafe variables.</param>
        public Scope(IMethod method, bool @unsafe = false)
        {
            // Increment ID
            ID = _nextScopeID++;

            // Create stack segment
            StackValues = new StackSegment(this);

            // Misc
            Method = method;
            Unsafe = @unsafe;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Scope"/> class with the specified parent. 
        /// </summary>
        /// <param name="parent">The parent scope of the current instance.</param>
        /// <param name="unsafe">Whether the scope has unsafe variables or not.</param>
        public Scope(Scope parent, bool @unsafe = false)
        {
            // Increment ID
            ID = _nextScopeID++;

            // Register as child of parent
            Parent = parent;
            parent.AddChild(this);

            // Create stack segment
            // (Must happen after setting parent)
            StackValues = new StackSegment(this);

            // Misc
            Method = parent.Method;
            Unsafe = parent.Unsafe || @unsafe;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Searches for the stack variable with the specified name.
        /// </summary>
        /// <param name="name">The name of the variable to search for.</param>
        /// <param name="recursive">Whether to recursively search through parent scopes for the variable.</param>
        /// <returns>The <see cref="StackValue"/> object representing the variable if found; null if not found.</returns>
        public StackValue Search(string name, bool recursive = true)
        {
            // Loop through stack values in this scope
            foreach (StackValue value in StackValues)
                if (value.Name.Equals(name, Settings.IdentifierComparisonMode))
                    return value; // Match found

            if (!recursive || Parent == null) return null;

            // Parent exists and recursion enabled, so search
            return Parent.Search(name);
        }

        /// <summary>
        /// Creates a <see cref="StackValue"/> instance with a unique name, to be used for a compiler generated variable.
        /// </summary>
        /// <param name="stackSpace">The number of items the value should take up on the stack.</param>
        /// <returns>The created <see cref="StackValue"/>.</returns>
        public StackValue CreateStackValue(int stackSpace = 1)
        {
            StackValue value = new StackValue("@" + _nextStackID++, DataType.Object, stackSpace);
            StackValues.Add(value);
            return value;
        }

        /// <summary>
        /// Creates the code to clean up the stack at the end of a scope.
        /// </summary>
        /// <returns>The code to clean up the stack at the end of a scope.</returns>
        public IEnumerable<Block> CreateCleanUp()
        {
            return StackValues.SelectMany(stackValue => stackValue.CreateDestruction());
        }

        /// <summary>
        /// Adds a child scope to the collection of scopes.
        /// </summary>
        /// <param name="child">The scope to add as a child scope.</param>
        protected void AddChild(Scope child)
        {
            _childScopes.Add(child);
        }

        #endregion
    }
}