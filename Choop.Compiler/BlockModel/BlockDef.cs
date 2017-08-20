using System.Collections.ObjectModel;
using Choop.Compiler.Helpers;

namespace Choop.Compiler.BlockModel
{
    /// <summary>
    /// Represents a custom block definition.
    /// </summary>
    public class BlockDef : Block
    {
        #region Properties

        /// <summary>
        /// Gets or sets the signature of the custom block.
        /// </summary>
        public string Spec
        {
            get => (string)Args[0];
            set => Args[0] = value;
        }

        /// <summary>
        /// Gets the collection of input names of the custom block.
        /// </summary>
        public Collection<string> InputNames => (Collection<string>) Args[1];

        /// <summary>
        /// Gets the collection of default values for each input of the custom block.
        /// </summary>
        public Collection<object> DefaultValues => (Collection<object>) Args[2];

        /// <summary>
        /// Gets or sets whether the custom block is atomic.
        /// </summary>
        public bool Atomic
        {
            get => (bool) Args[3];
            set => Args[3] = value;
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates  a new instance of the <see cref="BlockDef"/> class.
        /// </summary>
        public BlockDef() : base(BlockSpecs.MethodDeclaration)
        {
            Args.Add("");
            Args.Add(new Collection<string>());
            Args.Add(new Collection<object>());
            Args.Add(false);
        }

        #endregion
    }
}