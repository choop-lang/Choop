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
        /// <param name="message">The error message.</param>
        /// <param name="line">The line number where the error occured.</param>
        /// <param name="col">The column number where the error occured.</param>
        public CompilerError(string message, int line, int col)
        {
            Message = message;
            Line = line;
            Col = col;
        }
        #endregion
    }
}
