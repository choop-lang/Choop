using System.Collections.Generic;
using System.Collections.ObjectModel;
using Choop.Compiler.ChoopModel;

namespace Choop.Compiler.TranslationUtils
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
        protected static int NextID = 1;

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
        public IHasBody Method { get; }

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
        public Scope(IHasBody method, bool @unsafe = false)
        {
            // Increment ID
            ID = NextID++;

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
            ID = NextID++;

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

            if (recursive && Parent != null)
                return Parent.Search(name); // Recursion allowed and parent exists

            // Base case, not found
            return null;
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