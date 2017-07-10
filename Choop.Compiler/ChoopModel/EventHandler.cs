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
        public Tuple<BlockModel.EventHandler, BlockDef> Translate(TranslationContext context)
        {
            BlockModel.EventHandler eventHandler;
            switch (Name)
            {
                case "GreenFlag":
                    eventHandler = new BlockModel.EventHandler(BlockSpecs.WhenGreenFlagClicked);
                    break;
                case "KeyPressed":
                    eventHandler = new BlockModel.EventHandler(BlockSpecs.WhenKeyPressed, Parameter.Translate());
                    break;
                case "Clicked":
                    eventHandler = new BlockModel.EventHandler(BlockSpecs.WhenSpriteClicked);
                    break;
                case "BackdropChanged":
                    eventHandler = new BlockModel.EventHandler(BlockSpecs.WhenBackdropSwitchesTo, Parameter.Translate());
                    break;
                case "MessageRecieved":
                    // TODO fix documentation typo
                    eventHandler = new BlockModel.EventHandler(BlockSpecs.WhenIReceive, Parameter.Translate());
                    break;
                case "Cloned":
                    eventHandler = new BlockModel.EventHandler(BlockSpecs.WhenSpriteCloned);
                    break;
                case "TimerGreaterThan":
                    eventHandler = new BlockModel.EventHandler(BlockSpecs.WhenSensorGreaterThan, "timer", Parameter.Translate());
                    break;
                case "LoudnessGreaterThan":
                    eventHandler = new BlockModel.EventHandler(BlockSpecs.WhenSensorGreaterThan, "loudness", Parameter.Translate());
                    break;
                case "VideoMotionGreaterThan":
                    eventHandler = new BlockModel.EventHandler(BlockSpecs.WhenSensorGreaterThan, "video motion", Parameter.Translate());
                    break;
                default:
                    throw new ArgumentException("Invalid event name", nameof(Name));
            }

            // TODO
            //eventHandler.Blocks.Add();

            return new Tuple<BlockModel.EventHandler, BlockDef>(eventHandler, null);
        }

        #endregion
    }
}