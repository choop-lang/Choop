namespace Choop.Compiler
{
    /// <summary>
    /// Represents a compiler error that occured whilst compiling a Choop file.
    /// </summary>
    public class CompilerError
    {
        #region Properties
        /// <summary>
        /// Gets the message of the <see cref="CompilerError"/>. 
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Gets the line number at which the <see cref="CompilerError"/> occured. 
        /// </summary>
        public int Line { get; }

        /// <summary>
        /// Gets the char position within the line at which the <see cref="CompilerError"/> occured. 
        /// </summary>
        public int Col { get; }

        /// <summary>
        /// Gets the starting character index of the token that caused the compiler error.
        /// </summary>
        public int StartIndex { get; }

        /// <summary>
        /// Gets the last character index of the token that caused the compiler error.
        /// </summary>
        public int StopIndex { get; }

        /// <summary>
        /// Gets the text contents of the token that caused the compiler error.
        /// </summary>
        public string TokenText { get; }
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="CompilerError"/> class. 
        /// </summary>
        public CompilerError()
        {

        }

        /// <summary>
        /// Creates a new instance of the <see cref="CompilerError"/> class. 
        /// </summary>
        /// <param name="message">The error message to display.</param>
        /// <param name="line">The line number where the error occured.</param>
        /// <param name="col">The column number where the error occured.</param>
        /// <param name="startIndex">The starting character index of the token that caused the error.</param>
        /// <param name="stopIndex">The last character index of the token that caused the error.</param>
        /// <param name="tokenText">The text of the token that caused the error.</param>
        public CompilerError(string message, int line, int col, int startIndex, int stopIndex, string tokenText)
        {
            Message = message;
            Line = line;
            Col = col;
            StartIndex = startIndex;
            StopIndex = stopIndex;
            TokenText = tokenText;
        }
        #endregion
    }
}
