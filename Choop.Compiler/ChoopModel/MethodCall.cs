using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Antlr4.Runtime;
using Choop.Compiler.BlockModel;
using Choop.Compiler.TranslationUtils;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents a method call.
    /// </summary>
    /// <remarks>Can be used inside an expression or as a standalone statement.</remarks>
    public class MethodCall : IExpression, IStatement
    {
        #region Properties

        /// <summary>
        /// Gets the name of the method being called.
        /// </summary>
        public string MethodName { get; }

        /// <summary>
        /// Gets the collection of parameters to the method.
        /// </summary>
        public Collection<IExpression> Parameters { get; }

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
        /// Creates a new instance of the <see cref="MethodCall"/> class.
        /// </summary>
        /// <param name="methodName">The name of the method being called.</param>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="errorToken">The token to report any compiler errors to.</param>
        public MethodCall(string methodName, string fileName, IToken errorToken)
        {
            MethodName = methodName;
            FileName = fileName;
            ErrorToken = errorToken;
            Parameters = new Collection<IExpression>();
        }

        /// <summary>
        /// Creates a new instance of the <see cref="MethodCall"/> class.
        /// </summary>
        /// <param name="methodName">The name of the method being called.</param>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="errorToken">The token to report any compiler errors to.</param>
        /// <param name="parameters">The parameters for the method.</param>
        public MethodCall(string methodName, string fileName, IToken errorToken, params IExpression[] parameters)
        {
            MethodName = methodName;
            FileName = fileName;
            ErrorToken = errorToken;
            Parameters = new Collection<IExpression>(parameters);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the translated code for the grammar structure.
        /// </summary>
        /// <returns>The translated code for the grammar structure.</returns>
        public object Translate(TranslationContext context)
        {
            // Find method
            MethodDeclaration customMethod = context.CurrentSprite.GetMethod(MethodName, Parameters.Count);

            if (customMethod != null)
            {
                // Custom method found

                // Ensure it returns a value
                if (!customMethod.HasReturn)
                {
                    context.ErrorList.Add(new CompilerError($"Method '{MethodName}' does not return a value",
                        ErrorType.InvalidArgument, ErrorToken, FileName));
                    return null;
                }

                // TODO: inline

                // TODO: Optimise to not need additional stack value (except for repeat until)

                List<object> translatedParams = new List<object> {customMethod.GetInternalName()};
                translatedParams.AddRange(Parameters.Select(x => x.Translate(context)));
                translatedParams.Add(new Block(BlockSpecs.GetParameter, Settings.StackRefParam));

                context.Before.Add(new Block(BlockSpecs.CustomMethodCall, translatedParams.ToArray()));
                StackValue returnValue = context.CurrentScope.CreateStackValue();
                context.Before.Add(returnValue.CreateVariableAssignment(
                    new LookupExpression(customMethod.GetReturnVariableName(), FileName, ErrorToken)));

                return returnValue.CreateVariableLookup();
            }

            // Custom method doesn't exist, so search inbuilt methods
            if (BlockSpecs.Inbuilt.TryGetValue(MethodName, out MethodSignature inbuiltMethod))
            {
                // Inbuilt method found

                // Check method is a reporter
                if (!inbuiltMethod.IsReporter)
                {
                    context.ErrorList.Add(new CompilerError($"The inbuilt method '{MethodName}' does not return a value", ErrorType.Unspecified,
                        ErrorToken, FileName));
                    return new Block[0];
                }

                // Check parameter count is valid
                if (Parameters.Count == inbuiltMethod.Inputs.Length)
                    return new[]
                        {new Block(inbuiltMethod.Name, Parameters.Select(x => x.Translate(context)).ToArray())};

                // Parameter count not valid
                context.ErrorList.Add(new CompilerError(
                    $"Expected inputs '{string.Join("', '", inbuiltMethod.Inputs)}'", ErrorType.InvalidArgument,
                    ErrorToken, FileName));
                return new Block[0];
            }

            // Error - nethod not found
            context.ErrorList.Add(new CompilerError($"Method '{MethodName}' is not defined", ErrorType.NotDefined,
                ErrorToken, FileName));
            return new Block(null);
        }

        /// <summary>
        /// Gets the translated code for the grammar structure.
        /// </summary>
        /// <returns>The translated code for the grammar structure.</returns>
        Block[] ICompilable<Block[]>.Translate(TranslationContext context)
        {
            // Find method
            MethodDeclaration customMethod = context.CurrentSprite.GetMethod(MethodName, Parameters.Count);

            if (customMethod != null)
            {
                // Custom method found

                // Don't check if returns value - it does not matter here

                // TODO: inline

                List<object> translatedParams = new List<object> { customMethod.GetInternalName() };
                translatedParams.AddRange(Parameters.Select(x => x.Translate(context)));
                translatedParams.Add(new Block(BlockSpecs.GetParameter, Settings.StackRefParam));

                return new[]
                    {new Block(BlockSpecs.CustomMethodCall, translatedParams.ToArray())};
            }

            // Custom method doesn't exist, so search inbuilt methods
            if (BlockSpecs.Inbuilt.TryGetValue(MethodName, out MethodSignature inbuiltMethod))
            {
                // Inbuilt method found

                // Check method is not a reporter
                if (inbuiltMethod.IsReporter)
                {
                    context.ErrorList.Add(new CompilerError($"The inbuilt method '{MethodName}' can only be used as an input", ErrorType.Unspecified,
                        ErrorToken, FileName));
                    return new Block[0];
                }

                // Check parameter count is valid
                if (Parameters.Count == inbuiltMethod.Inputs.Length)
                    return new[]
                        {new Block(inbuiltMethod.Name, Parameters.Select(x => x.Translate(context)).ToArray())};

                // Error: Invalid parameters
                context.ErrorList.Add(new CompilerError(
                    $"Expected inputs '{string.Join("', '", inbuiltMethod.Inputs)}'", ErrorType.InvalidArgument,
                    ErrorToken, FileName));
                return new Block[0];
            }

            // Error - nethod not found
            context.ErrorList.Add(new CompilerError($"Method '{MethodName}' is not defined", ErrorType.NotDefined,
                ErrorToken, FileName));
            return new Block[0];
        }

        #endregion
    }
}