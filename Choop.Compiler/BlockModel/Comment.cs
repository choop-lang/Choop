using System.Drawing;

namespace Choop.Compiler.BlockModel
{
    /// <summary>
    /// Represents a comment.
    /// </summary>
    public class Comment : IComponent
    {
        #region Properties
        /// <summary>
        /// Gets or sets the location of the comment.
        /// </summary>
        public Point Location { get; set; }

        /// <summary>
        /// Gets or sets the size of the comment.
        /// </summary>
        public Size Size { get; set; }

        /// <summary>
        /// Gets or sets whether the comment is expanded or collapsed.
        /// </summary>
        public bool Open { get; set; }

        /// <summary>
        /// Gets or sets the ID of the block the comment is attached to. -1 indicates no attachment.
        /// </summary>
        public int BlockID { get; set; }

        /// <summary>
        /// Gets or sets the text inside the comment.
        /// </summary>
        public string Text { get; set; }
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="Comment"/> class.
        /// </summary>
        public Comment()
        {
        }
        #endregion
    }
}
