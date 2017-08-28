using System;
using System.Collections.Generic;
using System.Linq;
using Choop.Compiler.BlockModel;
using Choop.Compiler.ChoopModel;
using Choop.Compiler.ChoopModel.Expressions;

namespace Choop.Compiler.Helpers
{
    /// <summary>
    /// Represents a variable or array stored on the stack.
    /// </summary>
    public class StackValue : ITypedDeclaration
    {
        #region Fields

        /// <summary>
        /// Whether the <see cref="StackValue"/> has been registered to a <see cref="StackSegment"/> object. 
        /// </summary>
        private bool _registered;

        /// <summary>
        /// Whether this single StackValue is unsafe, compared to the rest of the scope.
        /// </summary>
        private readonly bool _uniqueUnsafe;

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
        /// Gets whether this stack value is unsafe.
        /// </summary>
        public bool Unsafe => _uniqueUnsafe || Scope.Unsafe;

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
        /// <param name="unsafe">Whether this scope value has been marked as unsafe.</param>
        /// <param name="stackSpace">The number of items within the stack that the datum occupies.</param>
        public StackValue(string name, DataType type, bool @unsafe, int stackSpace = 1)
        {
            Name = name;
            Type = type;
            StackSpace = stackSpace;
            _uniqueUnsafe = @unsafe;
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
        /// <param name="context">The context for translating the expressions.</param>
        /// <param name="initalValues">The initial value(s) for the <see cref="StackValue"/></param>
        /// <returns>The declaration for the <see cref="StackValue"/>.</returns>
        public IEnumerable<Block> CreateDeclaration(TranslationContext context, params IExpression[] initalValues)
        {
            // Validate inital values
            if (initalValues.Length != StackSpace)
                throw new ArgumentException("Length of initial values does not match length of datum");

            // Use stack
            if (!Unsafe)
                return initalValues.SelectMany(x => new BlockBuilder(BlockSpecs.AddToList, context)
                    .AddParam(x, Type)
                    .AddParam(Settings.StackIdentifier)
                    .Create());

            // Unsafe variable
            if (StackSpace == 1)
            {
                // TODO: include var def in json
                //context.CurrentSprite.Variables.Add(new GlobalVarDeclaration(GetUnsafeName(), Type, "", null, null));
                return new BlockBuilder(BlockSpecs.SetVariableTo, context)
                    .AddParam(GetUnsafeName())
                    .AddParam(initalValues[0], Type)
                    .Create();
            }

            // Unsafe list
            // TODO: include list def in json
            //context.CurrentSprite.Lists.Add(new GlobalListDeclaration(GetUnsafeName(), Type, true, null, null));
            return initalValues.SelectMany((x, i) => new BlockBuilder(BlockSpecs.ReplaceItemOfList, context)
                .AddParam(i)
                .AddParam(GetUnsafeName())
                .AddParam(x, Type)
                .Create());
        }

        /// <summary>
        /// Creates the declaration for the <see cref="StackValue"/>.
        /// </summary>
        /// <param name="initalValues">The initial value(s) for the <see cref="StackValue"/></param>
        /// <returns>The declaration for the <see cref="StackValue"/>.</returns>
        public IEnumerable<Block> CreateDeclaration(params object[] initalValues)
        {
            if (initalValues.Length != StackSpace)
                throw new ArgumentException("Length of initial values does not match length of datum");

            if (Unsafe)
            {
                // Variable
                if (StackSpace == 1)
                    return new[] {new Block(BlockSpecs.SetVariableTo, GetUnsafeName(), initalValues[0])};

                // List
                Block[] blocks = new Block[StackSpace + 1];
                blocks[0] = new Block(BlockSpecs.DeleteItemOfList, "all", GetUnsafeName());
                for (int i = 0; i < StackSpace; i++)
                    blocks[i + 1] = new Block(BlockSpecs.AddToList, initalValues[i], GetUnsafeName());
                return blocks;
            }
            else
            {
                // Stack
                Block[] blocks = new Block[StackSpace];
                for (int i = 0; i < StackSpace; i++)
                    blocks[i] = new Block(BlockSpecs.AddToList, initalValues[i], Settings.StackIdentifier);
                return blocks;
            }
        }

        /// <summary>
        /// Returns the code for a variable lookup.
        /// </summary>
        /// <returns>The code for a variable lookup.</returns>
        public Block CreateVariableLookup()
        {
            if (Unsafe)
                return new Block(BlockSpecs.GetVariable, GetUnsafeName());

            return new Block(BlockSpecs.GetItemOfList,
                new Block(BlockSpecs.Add, Settings.StackOffsetIdentifier, StackStart),
                Settings.StackIdentifier);
        }

        /// <summary>
        /// Returns the code for an array lookup.
        /// </summary>
        /// <param name="index">The expression for the index to look up.</param>
        /// <returns>The code for an array lookup.</returns>
        public Block CreateArrayLookup(object index)
        {
            if (Unsafe)
                return new Block(BlockSpecs.GetItemOfList, index, GetUnsafeName());

            return new Block(BlockSpecs.GetItemOfList,
                new Block(BlockSpecs.Add, Settings.StackOffsetIdentifier,
                    new Block(BlockSpecs.Add, StackStart, index)),
                Settings.StackIdentifier);
        }

        /// <summary>
        /// Returns the code for a variable assignment.
        /// </summary>
        /// <param name="value">The translated expression for the value to be assigned.</param>
        /// <returns>The code for a variable assignment.</returns>
        public Block CreateVariableAssignment(object value)
        {
            if (Unsafe)
                return new Block(BlockSpecs.SetVariableTo, GetUnsafeName(), value);

            return new Block(BlockSpecs.ReplaceItemOfList,
                new Block(BlockSpecs.Add, Settings.StackOffsetIdentifier, StackStart), Settings.StackIdentifier,
                value);
        }

        /// <summary>
        /// Returns the code for a variable assignment.
        /// </summary>
        /// <param name="context">The context to translate the expression with.</param>
        /// <param name="value">The expression for the value to be assigned.</param>
        /// <returns>The code for a variable assignment.</returns>
        public IEnumerable<Block> CreateVariableAssignment(TranslationContext context, IExpression value)
        {
            if (Unsafe)
                return new BlockBuilder(BlockSpecs.SetVariableTo, context)
                    .AddParam(GetUnsafeName())
                    .AddParam(value, Type)
                    .Create();

            return new BlockBuilder(BlockSpecs.ReplaceItemOfList, context)
                .AddParam(new Block(BlockSpecs.Add, Settings.StackOffsetIdentifier, StackStart))
                .AddParam(value, Type)
                .Create();
        }

        /// <summary>
        /// Returns the code to increase the variable by the specified amount.
        /// </summary>
        /// <param name="value">The expression for the value to increment the variable by.</param>
        /// <returns>The code to increase the variable by the specified amount.</returns>
        public Block CreateVariableIncrement(object value)
        {
            // TODO: Optimise for when value is negative
            return Unsafe
                ? new Block(BlockSpecs.ChangeVarBy, GetUnsafeName(), value)
                : new Block(BlockSpecs.ReplaceItemOfList,
                    new Block(BlockSpecs.Add, Settings.StackOffsetIdentifier, StackStart), Settings.StackIdentifier,
                    new Block(BlockSpecs.Add, CreateVariableLookup(), value));
        }

        /// <summary>
        /// Returns the code to increase the variable by the specified amount.
        /// </summary>
        /// <param name="context">The context to translate the expression with.</param>
        /// <param name="value">The expression for the value to increment by.</param>
        /// <returns>The code to increase the variable by the specified amount.</returns>
        public IEnumerable<Block> CreateVariableIncrement(TranslationContext context, IExpression value)
        {
            if (Unsafe)
                return new BlockBuilder(BlockSpecs.ChangeVarBy, context)
                    .AddParam(GetUnsafeName())
                    .AddParam(value, Type)
                    .Create();

            return new BlockBuilder(BlockSpecs.ReplaceItemOfList, context)
                .AddParam(new Block(BlockSpecs.Add, Settings.StackOffsetIdentifier, StackStart))
                .AddParam(new CompoundExpression(CompoundOperator.Plus,
                    new LookupExpression(this, string.Empty, null),
                    value, string.Empty, null))
                .Create();
        }

        /// <summary>
        /// Returns the code for an array assignment.
        /// </summary>
        /// <param name="value">The expression for the value to assign to the array.</param>
        /// <param name="index">The index of the item to assign.</param>
        /// <returns>The code for an array assignment.</returns>
        public Block CreateArrayAssignment(object value, object index)
        {
            if (Unsafe)
                return new Block(BlockSpecs.ReplaceItemOfList, index, GetUnsafeName(), value);

            return new Block(BlockSpecs.ReplaceItemOfList,
                new Block(BlockSpecs.Add, Settings.StackOffsetIdentifier,
                    new Block(BlockSpecs.Add, StackStart, index)),
                Settings.StackIdentifier, value);
        }

        /// <summary>
        /// Returns the code for an array assignment.
        /// </summary>
        /// <param name="context">The context for translation.</param>
        /// <param name="value">The expression for the value to assign to the array.</param>
        /// <param name="index">The index of the item to assign.</param>
        /// <returns>The code for an array assignment.</returns>
        public IEnumerable<Block> CreateArrayAssignment(TranslationContext context, IExpression value, IExpression index)
        {
            if (Unsafe)
                return new BlockBuilder(BlockSpecs.ReplaceItemOfList, context)
                    .AddParam(index)
                    .AddParam(GetUnsafeName())
                    .AddParam(value, Type)
                    .Create();

            return new BlockBuilder(BlockSpecs.ReplaceItemOfList, context)
                .AddParam(ctx => new Block(BlockSpecs.Add, Settings.StackOffsetIdentifier,
                    new CompoundExpression(CompoundOperator.Plus, new TerminalExpression(StackStart), index,
                        string.Empty, null).Balance().Translate(ctx)))
                .AddParam(value, Type)
                .Create();
        }

        /// <summary>
        /// Creates the code to delete the variable from the stack, if necessary.
        /// </summary>
        /// <returns>The code to delete the variable from the stack or an empty array if unsafe.</returns>
        public IEnumerable<Block> CreateDestruction()
        {
            if (Unsafe)
                return Enumerable.Empty<Block>();

            return Enumerable.Repeat(
                new Block(BlockSpecs.DeleteItemOfList, "last", Settings.StackIdentifier), StackSpace);
        }

        /// <summary>
        /// Returns the unique name to use when this value is unsafe.
        /// </summary>
        private string GetUnsafeName() => $"{Scope.ID}: {Name}";

        #endregion
    }
}