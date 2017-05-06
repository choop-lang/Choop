﻿using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Choop.Compiler.ObjectModel
{
    /// <summary>
    /// Represents a scope for variables.
    /// </summary>
    public class Scope
    {
        #region Properties
        private List<Scope> childScopes = new List<Scope>();

        /// <summary>
        /// Gets the collection of values stored on the stack.
        /// </summary>
        public StackSegment StackValues { get; } = new StackSegment();

        /// <summary>
        /// Gets the parent scope of the current instance.
        /// </summary>
        public Scope Parent { get; }

        /// <summary>
        /// Gets the collection of child scopes.
        /// </summary>
        public ReadOnlyCollection<Scope> ChildScopes
        {
            get { return childScopes.AsReadOnly(); }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="Scope"/> class. 
        /// </summary>
        public Scope()
        {

        }

        /// <summary>
        /// Creates a new instance of the <see cref="Scope"/> class with the specified parent. 
        /// </summary>
        /// <param name="parent">The parent scope of the current instance.</param>
        public Scope(Scope parent)
        {
            // Set stack segment start index
            StackValues = new StackSegment(parent.StackValues.GetNextIndex());

            // Register as child of parent
            Parent = parent;
            parent.AddChild(this);
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
            {
                if (value.Name.Equals(name, Project.IdentifierComparisonMode))
                    return value; // Match found
            }

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
            childScopes.Add(child);
        }
        #endregion
    }
}
