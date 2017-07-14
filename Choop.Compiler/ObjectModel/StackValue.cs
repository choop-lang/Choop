using Choop.Compiler.ChoopModel;
using System;
using Choop.Compiler.BlockModel;

namespace Choop.Compiler.ObjectModel
{
    /// <summary>
    /// Represents a variable or array stored on the stack.
    /// </summary>
    public class StackValue
    {
        #region Fields

        /// <summary>
        /// Whether the <see cref="StackValue"/> has been registered to a <see cref="StackSegment"/> object. 
        /// </summary>
        private bool _registered;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of the datum.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the type of the data stored within each item on the stack.
        /// </summary>
        public DataType Type { get; }

        /// <summary>
        /// Gets the relative index of the first value of the datum within the stack.
        /// </summary>
        public int StackStart { get; private set; }

        /// <summary>
        /// Gets the number of items within the stack that the datum occupies. Default is 1.
        /// </summary>
        public int StackSpace { get; }

        /// <summary>
        /// Gets the scope of the datum.
        /// </summary>
        public Scope Scope { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="StackValue"/> class. 
        /// </summary>
        /// <param name="name">The name of the datum.</param>
        /// <param name="type">The type of the data stored within each stack item.</param>
        /// <param name="stackSpace">The number of items within the stack that the datum occupies.</param>
        public StackValue(string name, DataType type, int stackSpace = 1)
        {
            Name = name;
            Type = type;
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
            if (_registered)
                throw new InvalidOperationException("StackValue already registered to a StackSegment.");

            // Not previously registered

            // Set relative stack start index
            StackStart = stack.GetNextIndex();

            // Set scope
            Scope = stack.Scope;

            // Mark as registered
            _registered = true;
        }

        /// <summary>
        /// Creates the declaration for the <see cref="StackValue"/>.
        /// </summary>
        /// <param name="initalValues">The initial value(s) for the <see cref="StackValue"/></param>
        /// <returns>The declaration for the <see cref="StackValue"/>.</returns>
        public Block[] CreateDeclaration(params object[] initalValues)
        {
            if (initalValues.Length != StackSpace)
                throw new ArgumentException("Length of initial values does not match length of datum");

            if (Scope.Unsafe)
            {
                // Variable
                if (StackSpace == 1)
                    return new[] {new Block("setVar:to:", GetUnsafeName(), initalValues[0])};

                // List
                Block[] blocks = new Block[StackSpace + 1];
                blocks[0] = new Block("deleteLine:ofList:", "all", GetUnsafeName());
                for (int i = 0; i < StackSpace; i++)
                {
                    blocks[i + 1] = new Block("append:toList:", initalValues[i], GetUnsafeName());
                }
                return blocks;
            }
            else
            {
                // Stack
                Block[] blocks = new Block[StackSpace];
                for (int i = 0; i < StackSpace; i++)
                {
                    blocks[i] = new Block("append:toList:", initalValues[i], Settings.StackIdentifier);
                }
                return blocks;
            }
        }

        /// <summary>
        /// Returns the code for a variable lookup.
        /// </summary>
        /// <returns>The code for a variable lookup.</returns>
        public Block CreateVariableLookup()
        {
            return Scope.Unsafe
                ? new Block(BlockSpecs.GetVariable, GetUnsafeName())
                : new Block(BlockSpecs.GetItemOfList, StackStart, Settings.StackIdentifier);
        }

        /// <summary>
        /// Returns the code for an array lookup.
        /// </summary>
        /// <param name="index">The expression for the index to look up.</param>
        /// <returns>The code for an array lookup.</returns>
        public Block CreateArrayLookup(object index)
        {
            return Scope.Unsafe
                ? new Block(BlockSpecs.GetItemOfList, index, GetUnsafeName())
                : new Block(BlockSpecs.GetItemOfList, new Block(BlockSpecs.Add, StackStart, index),
                    Settings.StackIdentifier);
        }

        /// <summary>
        /// Returns the code for a variable assignment.
        /// </summary>
        /// <param name="value">The expression for the value to assign to the variable.</param>
        /// <returns>The code for a variable assignment.</returns>
        public Block CreateVariableAssignment(object value)
        {
            return Scope.Unsafe
                ? new Block(BlockSpecs.SetVariableTo, GetUnsafeName(), value)
                : new Block(BlockSpecs.ReplaceItemOfList, StackStart, Settings.StackIdentifier, value);
        }

        /// <summary>
        /// Returns the code to increase the variable by the specified amount.
        /// </summary>
        /// <param name="value">The expression for the value to increment the variable by.</param>
        /// <returns>The code to increase the variable by the specified amount.</returns>
        public Block CreateVariableIncrement(object value)
        {
            return Scope.Unsafe
                ? new Block(BlockSpecs.ChangeVarBy, GetUnsafeName(), value)
                : new Block(BlockSpecs.ReplaceItemOfList, StackStart, Settings.StackIdentifier,
                    new Block(BlockSpecs.Add, CreateVariableLookup(), value));
        }

        /// <summary>
        /// Returns the code for an array assignment.
        /// </summary>
        /// <param name="value">The expression for the value to assign to the array.</param>
        /// <param name="index">The index of the item to assign.</param>
        /// <returns>The code for an array assignment.</returns>
        public Block CreateArrayAssignment(object value, object index)
        {
            return Scope.Unsafe
                ? new Block(BlockSpecs.ReplaceItemOfList, index, GetUnsafeName(), value)
                : new Block(BlockSpecs.ReplaceItemOfList, new Block(BlockSpecs.Add, StackStart, index),
                    Settings.StackIdentifier, value);
        }

        /// <summary>
        /// Creates the code to delete the variable from the stack, if necessary.
        /// </summary>
        /// <returns>The code to delete the variable from the stack or an empty array if unsafe.</returns>
        public Block[] CreateDestruction()
        {
            if (Scope.Unsafe)
                return new Block[0];

            Block[] result = new Block[StackSpace];
            for (int i = 0; i < StackSpace; i++)
            {
                result[i] = new Block(BlockSpecs.DeleteItemOfList, "last", Settings.StackIdentifier);
            }
            return result;
        }

        /// <summary>
        /// Gets the unique name to use when this value is unsafe.
        /// </summary>
        /// <returns>The unique name to use when this value is unsafe.</returns>
        public string GetUnsafeName() => $"{Scope.ID}: {Name}";

        #endregion
    }
}