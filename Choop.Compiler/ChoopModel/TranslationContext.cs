using System.Collections.ObjectModel;
using Choop.Compiler.TranslationUtils;

namespace Choop.Compiler.ChoopModel
{
    public class TranslationContext
    {
        #region Properties

        /// <summary>
        /// Gets the current active scope.
        /// </summary>
        public Scope CurrentScope { get; }

        /// <summary>
        /// Gets the collection of compiler errors.
        /// </summary>
        public Collection<CompilerError> ErrorList { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="TranslationContext"/> class.
        /// </summary>
        /// <param name="currentScope">The current active scope.</param>
        /// <param name="errorList">The collection of compiler errors.</param>
        public TranslationContext(Scope currentScope, Collection<CompilerError> errorList)
        {
            CurrentScope = currentScope;
            ErrorList = errorList;
        }

        #endregion
    }
}