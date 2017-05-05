using Choop.Compiler.ChoopModel;
using System;

namespace Choop.Compiler.ObjectModel
{
    /// <summary>
    /// Represents a variable or array stored on the stack.
    /// </summary>
    public class StackValue : ITypedSignature
    {
        #region Fields
        /// <summary>
        /// Whether the <see cref="StackValue"/> has been registered to a <see cref="StackSegment"/> object. 
        /// </summary>
        private bool Registered = false;
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets the name of the datum.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the data stored within each item on the stack.
        /// </summary>
        public DataType Type { get; set; }

        /// <summary>
        /// Gets or sets the relative index of the first value of the datum within the stack.
        /// </summary>
        public int StackStart { get; private set; }

        /// <summary>
        /// Gets or sets the number of items within the stack that the datum occupies. Default is 1.
        /// </summary>
        public int StackSpace { get; } = 1;
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="StackValue"/> class. 
        /// </summary>
        public StackValue()
        {
            
        }

        /// <summary>
        /// Creates a new instance of the <see cref="StackValue"/> class. 
        /// </summary>
        /// <param name="stackSpace">The number of items within the stack that the datum occupies.</param>
        public StackValue(int stackSpace)
        {
            StackSpace = stackSpace;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Registers the <see cref="StackValue"/> with the specified <see cref="StackSegment"/>. For internal use only.  
        /// </summary>
        /// <param name="stack">The <see cref="StackSegment"/> to register the <see cref="StackValue"/> to.</param>
        public void UpdateInfo(StackSegment stack)
        {
            // Check if previously registered
            if (!Registered)
            {
                // Not previously registered

                // Set relative stack start index
                StackStart = stack.GetNextIndex();

                // Mark as registered
                Registered = true;
            }
            else
            {
                // Already registered

                // Throw an exception - cannot be added twice
                throw new InvalidOperationException("StackValue already registered to a StackSegment.");
            }
        }
        #endregion
    }
}
