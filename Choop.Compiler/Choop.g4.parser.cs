using System.Collections.Generic;
using Antlr4.Runtime;
using System.Collections.ObjectModel;

namespace Choop.Compiler
{
    sealed partial class ChoopParser
    {
        #region Fields
        /// <summary>
        /// Gets the list of event names and their corresponding opcodes.
        /// </summary>
        public static readonly IDictionary<string, string> EventNames
            = new Dictionary<string, string>
            {
                {"GreenFlag", "whenGreenFlag"},
                {"KeyPressed", "whenKeyPressed"},
                {"Clicked", "whenClicked"},
                {"BackdropChanged", "whenSceneStarts"},
                {"MessageRecieved", "whenIReceive"},
                {"Cloned", "whenCloned"},
                {"LoudnessGreaterThan", "whenSensorGreaterThan"}, // TODO: handling of hidden parameter?
                {"TimerGreaterThan", "whenSensorGreaterThan"},
                {"VideoMotionGreaterThan", "whenSensorGreaterThan"},
            };
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="ChoopParser"/> class. 
        /// </summary>
        /// <param name="input">The token stream to parse.</param>
        /// <param name="errorCollection">The collection to record compiler errors to.</param>
        public ChoopParser(ITokenStream input, Collection<CompilerError> errorCollection) : this(input)
        {
            // Set error listener
            RemoveErrorListeners();
            AddErrorListener(new ChoopParserErrorListener(errorCollection));
        }
        #endregion
    }
}
