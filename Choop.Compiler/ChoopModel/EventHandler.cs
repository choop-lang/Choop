using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Antlr4.Runtime;
using Choop.Compiler.BlockModel;
using Choop.Compiler.TranslationUtils;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents an event handler.
    /// </summary>
    public class EventHandler : IDeclaration, ICompilable<ScriptTuple[]>, IHasBody
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

        /// <summary>
        /// Gets the token to report any compiler errors to.
        /// </summary>
        public IToken ErrorToken { get; }

        /// <summary>
        /// Gets the file name where the grammar structure was found.
        /// </summary>
        public string FileName { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="EventHandler"/> class.
        /// </summary>
        /// <param name="name">The name of the event being handled.</param>
        /// <param name="parameter">The parameter for the event handler, if necessary.</param>
        /// <param name="unsafe">Whether the event handler is unsafe.</param>
        /// <param name="atomic">Whether the event handler is atomic.</param>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="errorToken">The token to report any compiler errors to.</param>
        public EventHandler(string name, TerminalExpression parameter, bool @unsafe, bool atomic, string fileName,
            IToken errorToken)
        {
            Name = name;
            Parameter = parameter;
            Unsafe = @unsafe;
            Atomic = atomic;
            FileName = fileName;
            ErrorToken = errorToken;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the translated code for the grammar structure.
        /// </summary>
        /// <returns>The translated code for the grammar structure.</returns>
        public ScriptTuple[] Translate(TranslationContext context)
        {
            // Create event scope
            Scope newScope = new Scope(this);

            string internalName = $"{newScope.ID}: {Name} %n"; // Internal name used for custom block

            ScriptTuple eventHandler = new ScriptTuple();

            switch (Name)
            {
                case "GreenFlag":
                    eventHandler.Blocks.Add(new Block(BlockSpecs.WhenGreenFlagClicked));
                    break;
                case "KeyPressed":
                    eventHandler.Blocks.Add(new Block(BlockSpecs.WhenKeyPressed, Parameter.Translate(context)));
                    break;
                case "Clicked":
                    eventHandler.Blocks.Add(new Block(BlockSpecs.WhenSpriteClicked));
                    break;
                case "BackdropChanged":
                    eventHandler.Blocks.Add(new Block(BlockSpecs.WhenBackdropSwitchesTo, Parameter.Translate(context)));
                    break;
                case "MessageReceived":
                    eventHandler.Blocks.Add(new Block(BlockSpecs.WhenIReceive, Parameter.Translate(context)));
                    break;
                case "Cloned":
                    eventHandler.Blocks.Add(new Block(BlockSpecs.WhenSpriteCloned));
                    break;
                case "TimerGreaterThan":
                    eventHandler.Blocks.Add(new Block(BlockSpecs.WhenSensorGreaterThan, "timer",
                        Parameter.Translate(context)));
                    break;
                case "LoudnessGreaterThan":
                    eventHandler.Blocks.Add(new Block(BlockSpecs.WhenSensorGreaterThan, "loudness",
                        Parameter.Translate(context)));
                    break;
                case "VideoMotionGreaterThan":
                    eventHandler.Blocks.Add(new Block(BlockSpecs.WhenSensorGreaterThan, "video motion",
                        Parameter.Translate(context)));
                    break;
                default:
                    context.ErrorList.Add(new CompilerError($"Event '{Name}' is not supported", ErrorType.NotDefined, ErrorToken, FileName));
                    return new ScriptTuple[2];
            }

            // Increment CurrentStack
            eventHandler.Blocks.Add(new Block(BlockSpecs.ChangeVarBy, Settings.CurrentStackVar, 1));
            // Clear (or create) stack
            eventHandler.Blocks.Add(new Block(BlockSpecs.DeleteItemOfList, "all",
                new Block(BlockSpecs.GetVariable, Settings.CurrentStackVar)));
            // Call internal method
            eventHandler.Blocks.Add(new Block(BlockSpecs.CustomMethodCall, internalName,
                new Block(BlockSpecs.GetVariable, Settings.CurrentStackVar)));
            // Decrement CurrentStack
            eventHandler.Blocks.Add(new Block(BlockSpecs.ChangeVarBy, Settings.CurrentStackVar, -1));

            // Create internal method
            ScriptTuple internalMethod = new ScriptTuple();

            BlockDef definition = new BlockDef
            {
                Spec = internalName,
                Atomic = Atomic
            };
            definition.InputNames.Add(Settings.StackRefParam);
            definition.DefaultValues.Add(DataType.Number.GetDefault());

            internalMethod.Blocks.Add(definition);

            // Translate event code
            TranslationContext newContext = new TranslationContext(newScope, context);
            foreach (IStatement statement in Statements)
                foreach (Block block in statement.Translate(newContext))
                    internalMethod.Blocks.Add(block);

            // Return results
            return new[]
            {
                eventHandler,
                internalMethod
            };
        }

        #endregion
    }
}