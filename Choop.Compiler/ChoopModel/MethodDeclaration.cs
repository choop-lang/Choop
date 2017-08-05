using System.Collections.ObjectModel;
using System.Linq;
using Antlr4.Runtime;
using Choop.Compiler.BlockModel;
using Choop.Compiler.TranslationUtils;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents a function or void declaration.
    /// </summary>
    public class MethodDeclaration : IMethod, ITypedDeclaration, ICompilable<ScriptTuple>
    {
        #region Properties

        /// <summary>
        /// Gets whether the method is unsafe.
        /// </summary>
        public bool Unsafe { get; }

        /// <summary>
        /// Gets whether the method should be inlined.
        /// </summary>
        public bool Inline { get; }

        /// <summary>
        /// Gets whether the method should be atomic.
        /// </summary>
        public bool Atomic { get; }

        /// <summary>
        /// Gets the name of the method.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the return type of the method.
        /// </summary>
        public DataType Type { get; }

        /// <summary>
        /// Gets whether the method will return a value.
        /// </summary>
        public bool HasReturn { get; }

        /// <summary>
        /// Gets the collection of parameters declared in the method.
        /// </summary>
        public Collection<ParamDeclaration> Params { get; } = new Collection<ParamDeclaration>();

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
        /// Creates a new instance of the <see cref="MethodDeclaration"/> class.
        /// </summary>
        /// <param name="name">The name of the method.</param>
        /// <param name="type">The return type of the method, if applicable.</param>
        /// <param name="hasReturn">Whether the method returns a value.</param>
        /// <param name="unsafe">Whether the method is unsafe.</param>
        /// <param name="inline">Whether the method should be inlined.</param>
        /// <param name="atomic">Whether the method should be atomic.</param>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="errorToken">The token to report any compiler errors to.</param>
        public MethodDeclaration(string name, DataType type, bool hasReturn, bool @unsafe, bool inline, bool atomic,
            string fileName, IToken errorToken)
        {
            Name = name;
            Type = type;
            HasReturn = hasReturn;
            Unsafe = @unsafe;
            Inline = inline;
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
        public ScriptTuple Translate(TranslationContext context)
        {
            BlockDef definition = new BlockDef
            {
                Spec = GetInternalName(),
                Atomic = Atomic
            };

            // Add parameters
            foreach (ParamDeclaration paramDeclaration in Params)
            {
                definition.InputNames.Add(paramDeclaration.Name);
                definition.DefaultValues.Add(paramDeclaration.Type.GetDefault());
            }

            // Add hidden stack parameters
            definition.InputNames.Add(Settings.StackRefParam);
            definition.DefaultValues.Add(0);
            definition.InputNames.Add(Settings.StackOffsetParam);
            definition.DefaultValues.Add(0);

            // Create script tuple
            ScriptTuple script = new ScriptTuple();
            script.Blocks.Add(definition);

            // Create translation context
            Scope scope = new Scope(this, Unsafe);
            TranslationContext newContext = new TranslationContext(scope, context);

            // Translate blocks
            foreach (IStatement statement in Statements)
                foreach (Block block in statement.Translate(newContext))
                    script.Blocks.Add(block);

            // Clean up scope (if necessary)
            IStatement lastStatement = Statements.LastOrDefault();
            if (!(lastStatement is ReturnStmt || lastStatement is ForeverLoop))
                foreach (Block block in scope.CreateCleanUp())
                    script.Blocks.Add(block);

            return script;
        }

        /// <summary>
        /// Returns the parameter with the specified name.
        /// </summary>
        /// <param name="name">The name of the parameter to search for.</param>
        /// <returns>The parameter with the specified name, null if not found.</returns>
        public ParamDeclaration FindParam(string name)
        {
            return Params.FirstOrDefault(x => x.Name == name);
        }

        /// <summary>
        /// Returns the internal name of the method.
        /// </summary>
        /// <returns>The internal name of the method.</returns>
        public string GetInternalName() => string.Concat(
            Name, " ", string.Join(" ", Params.Select(x => x.Type.ToInputNotation())), " ", BlockSpecs.InputNum, " ", BlockSpecs.InputNum);

        /// <summary>
        /// Returns the name of the variable used to temporarily store return output.
        /// </summary>
        /// <returns>The name of the variable used to temporarily store return output.</returns>
        public string GetReturnVariableName() => $"@return: {Name}({Params.Count})";

        #endregion
    }
}