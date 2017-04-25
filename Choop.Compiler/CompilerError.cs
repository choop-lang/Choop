namespace Choop.Compiler
{
    /// <summary>
    /// Represents a compiler error that occured whilst compiling a Choop file.
    /// </summary>
    public class CompilerError
    {
        #region Properties
        /// <summary>
        /// Gets or sets the message of the <see cref="CompilerError"/>. 
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the line number at which the <see cref="CompilerError"/> occured. 
        /// </summary>
        public int Line { get; set; }

        /// <summary>
        /// Gets or sets the char position with the line at which the <see cref="CompilerError"/> occured. 
        /// </summary>
        public int Col { get; set; }

        /// <summary>
        /// Gets or sets the starting character index of the token that caused the compiler error.
        /// </summary>
        public int StartIndex { get; set; }

        /// <summary>
        /// Gets or sets the last character index of the token that caused the compiler error.
        /// </summary>
        public int StopIndex { get; set; }

        /// <summary>
        /// Gets or sets the text contents of the token that caused the compiler error.
        /// </summary>
        public string TokenText { get; set; }
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
