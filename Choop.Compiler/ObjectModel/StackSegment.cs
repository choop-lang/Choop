using System.Collections;
using System.Collections.Generic;

namespace Choop.Compiler.ObjectModel
{
    /// <summary>
    /// Represents a segment of the stack for a single scope.
    /// </summary>
    public class StackSegment : IReadOnlyCollection<StackValue>
    {
        #region Fields
        /// <summary>
        /// The underlying list instance.
        /// </summary>
        private readonly List<StackValue> _base = new List<StackValue>();
        #endregion
        #region Properties
        /// <summary>
        /// Gets the relative start index of segment within the complete stack.
        /// </summary>
        public int StartIndex { get; }

        /// <summary>
        /// Gets the number of elements contained within the <see cref="StackSegment"/>. 
        /// </summary>
        public int Count => _base.Count;

        /// <summary>
        /// Gets the value at the specified position.
        /// </summary>
        /// <param name="index">The index of the item to get.</param>
        /// <returns>The iitem at the specified position.</returns>
        public StackValue this[int index] => _base[index];

        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new instnace of the <see cref="StackSegment"/> class. 
        /// </summary>
        public StackSegment()
        {
           
        }

        /// <summary>
        /// Creates a new instnace of the <see cref="StackSegment"/> class. 
        /// </summary>
        /// <param name="startIndex">The relative start index of segment within the complete stack.</param>
        public StackSegment(int startIndex)
        {
            StartIndex = startIndex;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Adds an item to the <see cref="StackSegment"/>. 
        /// </summary>
        /// <param name="item">The item to add to the <see cref="StackSegment"/>.</param>
        public void Add(StackValue item)
        {
            // Register item to stack
            item.UpdateInfo(this);

            // Add to base
            _base.Add(item);
        }

        /// <summary>
        /// Returns the relative start index for the next stack item to add.
        /// </summary>
        /// <returns>The relative start index for the next stack item.</returns>
        public int GetNextIndex()
        {
            // If stack segment currently empty, use stack segment start index
            if (_base.Count == 0)
                return StartIndex;

            // Stack segment already contains values, calculate start index

            // Get last item in stack segment
            StackValue last = _base[_base.Count - 1];

            // Return value
            return last.StackStart + last.StackSpace;
        }

        /// <summary>
        /// Removes all items from the <see cref="StackSegment"/>.
        /// </summary>
        public void Clear() => _base.Clear();

        /// <summary>
        /// Determines whether the <see cref="StackSegment"/> contains a specific value.
        /// </summary>
        /// <param name="item">The item to locate in the <see cref="StackSegment"/>.</param>
        /// <returns>true if item is found in the <see cref="StackSegment"/>; otherwise, false.</returns>
        public bool Contains(StackValue item) => _base.Contains(item);

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="StackSegment"/>.
        /// </summary>
        /// <returns>An enumerator that iterates through the <see cref="StackSegment"/>.</returns>
        public IEnumerator<StackValue> GetEnumerator() => _base.GetEnumerator();

        /// <summary>
        /// Searches for the specified object and returns the zero-based index of the first occurence within the entire <see cref="StackSegment"/>. 
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="StackSegment"/>.</param>
        public int IndexOf(StackValue item) => _base.IndexOf(item);

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that iterates through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator() => _base.GetEnumerator();

        #endregion
    }
}
