using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Antlr4.Runtime;
using Choop.Compiler.BlockModel;
using Choop.Compiler.ChoopModel.Expressions;
using Choop.Compiler.Helpers;

namespace Choop.Compiler.ChoopModel.Methods
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
        /// Balances the binary trees within the expression.
        /// </summary>
        /// <returns>The balanced expression.</returns>
        public IExpression Balance() => new MethodCall(MethodName, FileName, ErrorToken,
            Parameters.Select(x => x.Balance()).ToArray());

        /// <summary>
        /// Returns the output type of the translated expression.
        /// </summary>
        /// <param name="context">The current translation state.</param>
        public DataType GetReturnType(TranslationContext context)
        {
            throw new NotImplementedException();

            //MethodDeclaration customMethod = context.CurrentSprite.GetMethod(MethodName, Parameters.Count);

            //if (customMethod != null) return customMethod.Type;

            //if (BlockSpecs.Inbuilt.TryGetValue(MethodName, out MethodSignature inbuiltMethod))
            //    return inbuiltMethod.Type;
        }

        /// <summary>
        /// Gets the translated code for the grammar structure.
        /// </summary>
        /// <returns>The translated code for the grammar structure.</returns>
        public Block[] Translate(TranslationContext context)
        {
            // Find method
            MethodDeclaration customMethod = context.CurrentSprite.GetMethod(MethodName, Parameters.Count);

            if (customMethod != null)
            {
                // Custom method found

                // Don't check if returns value - it does not matter here

                // TODO: inline

                List<object> translatedParams = new List<object> {customMethod.GetInternalName()};
                translatedParams.AddRange(Parameters.Select(x => x.Balance().Translate(context)));
                for (int i = Parameters.Count; i < customMethod.Params.Count; i++)
                    translatedParams.Add(customMethod.Params[i].Default);
                translatedParams.Add(new Block(BlockSpecs.GetParameter, Settings.StackRefParam));
                translatedParams.Add(new Block(BlockSpecs.LengthOfList, Settings.StackIdentifier));

                return new[]
                    {new Block(BlockSpecs.CustomMethodCall, translatedParams.ToArray())};
            }

            // Custom method doesn't exist, so search inbuilt methods
            if (InbuiltMethods.StandardMethods.TryGetValue(MethodName, out MethodSignature inbuiltMethod))
            {
                // Inbuilt method found

                // Check method is not a reporter
                if (inbuiltMethod.IsReporter)
                {
                    context.ErrorList.Add(new CompilerError(
                        $"The inbuilt method '{MethodName}' can only be used as an input", ErrorType.ImproperUsage,
                        ErrorToken, FileName));
                    return new Block[0];
                }

                // Check parameter count is valid
                if (Parameters.Count == inbuiltMethod.Inputs.Length)
                    return new[]
                    {
                        new Block(inbuiltMethod.Name, Parameters.Select(x => x.Balance().Translate(context)).ToArray())
                    };

                // Error: Invalid parameters
                context.ErrorList.Add(new CompilerError(
                    $"Expected inputs '{string.Join("', '", inbuiltMethod.Inputs)}'", ErrorType.InvalidArgument,
                    ErrorToken, FileName));
                return new Block[0];
            }

            // Try non standard blocks
            if (TranslateNonStandardMethod(context, false) is Block nonStandardMethod)
                return new[] {nonStandardMethod};

            // Error - nethod not found
            context.ErrorList.Add(new CompilerError($"Method '{MethodName}' is not defined", ErrorType.NotDefined,
                ErrorToken, FileName));
            return new Block[0];
        }

        /// <summary>
        /// Gets the translated code for the grammar structure.
        /// </summary>
        /// <returns>The translated code for the grammar structure.</returns>
        object ICompilable<object>.Translate(TranslationContext context)
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
                        ErrorType.ImproperUsage, ErrorToken, FileName));
                    return null;
                }

                // TODO: inline

                // TODO: Optimise to not need additional stack value (except for repeat until)

                List<object> translatedParams = new List<object> {customMethod.GetInternalName()};
                translatedParams.AddRange(Parameters.Select(x => x.Translate(context)));
                for (int i = Parameters.Count; i < customMethod.Params.Count; i++)
                    translatedParams.Add(customMethod.Params[i].Default);
                translatedParams.Add(new Block(BlockSpecs.GetParameter, Settings.StackRefParam));
                translatedParams.Add(new Block(BlockSpecs.LengthOfList, Settings.StackIdentifier));

                context.Before.Add(new Block(BlockSpecs.CustomMethodCall, translatedParams.ToArray()));
                StackValue returnValue = context.CurrentScope.CreateStackValue();
                context.Before.Add(returnValue.CreateDeclaration(new Block(BlockSpecs.GetVariable,
                    customMethod.GetReturnVariableName()))[0]);

                return returnValue.CreateVariableLookup();
            }

            // Custom method doesn't exist, so search inbuilt methods
            if (InbuiltMethods.StandardMethods.TryGetValue(MethodName, out MethodSignature inbuiltMethod))
            {
                // Inbuilt method found

                // Check method is a reporter
                if (!inbuiltMethod.IsReporter)
                {
                    context.ErrorList.Add(new CompilerError(
                        $"The inbuilt method '{MethodName}' does not return a value", ErrorType.ImproperUsage,
                        ErrorToken, FileName));
                    return new Block(null);
                }

                // Check parameter count is valid
                if (Parameters.Count == inbuiltMethod.Inputs.Length)
                    return new Block(inbuiltMethod.Name, Parameters.Select(x => x.Translate(context)).ToArray());

                // Parameter count not valid
                context.ErrorList.Add(new CompilerError(
                    $"Expected inputs '{string.Join("', '", inbuiltMethod.Inputs)}'", ErrorType.InvalidArgument,
                    ErrorToken, FileName));
                return new Block(null);
            }

            // Try non standard blocks
            object nonStandardMethod = TranslateNonStandardMethod(context, true);
            if (nonStandardMethod != null) return nonStandardMethod;

            // Error - nethod not found
            context.ErrorList.Add(new CompilerError($"Method '{MethodName}' is not defined", ErrorType.NotDefined,
                ErrorToken, FileName));
            return new Block(null);
        }

        /// <summary>
        /// Interpret the method call as a non standard inbuilt block.
        /// </summary>
        /// <param name="context">The context for the translation.</param>
        /// <param name="isFunctionCall">Whether the method is being used as a function or a statement.</param>
        /// <returns>The translated non-standard block.</returns>
        private object TranslateNonStandardMethod(TranslationContext context, bool isFunctionCall)
        {
            switch (MethodName)
            {
                case InbuiltMethods.StopAll:
                    if (isFunctionCall)
                    {
                        context.ErrorList.Add(new CompilerError($"'{MethodName}' does not return a value",
                            ErrorType.ImproperUsage, ErrorToken, FileName));
                        return new Block(null);
                    }

                    if (Parameters.Count == 0) return new Block(BlockSpecs.Stop, "all");

                    context.ErrorList.Add(new CompilerError($"Expected no parameters on method '{MethodName}'",
                        ErrorType.InvalidArgument, ErrorToken, FileName));
                    return new Block(null);

                case InbuiltMethods.StopOtherScriptsInSprite:
                    if (isFunctionCall)
                    {
                        context.ErrorList.Add(new CompilerError($"'{MethodName}' does not return a value",
                            ErrorType.ImproperUsage, ErrorToken, FileName));
                        return new Block(null);
                    }

                    if (Parameters.Count == 0) return new Block(BlockSpecs.Stop, "other scripts in sprite");

                    context.ErrorList.Add(new CompilerError($"Expected no parameters on method '{MethodName}'",
                        ErrorType.InvalidArgument, ErrorToken, FileName));
                    return new Block(null);

                case InbuiltMethods.Abs:
                    return TranslateMathsFunction(context, "abs", isFunctionCall, Math.Abs);

                case InbuiltMethods.Floor:
                    return TranslateMathsFunction(context, "floor", isFunctionCall, Math.Floor);

                case InbuiltMethods.Ceiling:
                    return TranslateMathsFunction(context, "ceiling", isFunctionCall, Math.Ceiling);

                case InbuiltMethods.Sqrt:
                    return TranslateMathsFunction(context, "sqrt", isFunctionCall, Math.Sqrt);

                case InbuiltMethods.Sin:
                    return TranslateMathsFunction(context, "sin", isFunctionCall, x => Math.Atan(x * Math.PI / 180));

                case InbuiltMethods.Cos:
                    return TranslateMathsFunction(context, "cos", isFunctionCall, x => Math.Atan(x * Math.PI / 180));

                case InbuiltMethods.Tan:
                    return TranslateMathsFunction(context, "tan", isFunctionCall, x => Math.Atan(x * Math.PI / 180));

                case InbuiltMethods.Asin:
                    return TranslateMathsFunction(context, "asin", isFunctionCall, x => Math.Asin(x) * 180 / Math.PI);

                case InbuiltMethods.Acos:
                    return TranslateMathsFunction(context, "acos", isFunctionCall, x => Math.Acos(x) * 180 / Math.PI);

                case InbuiltMethods.Atan:
                    return TranslateMathsFunction(context, "atan", isFunctionCall, x => Math.Atan(x) * 180 / Math.PI);

                case InbuiltMethods.Ln:
                    return TranslateMathsFunction(context, "ln", isFunctionCall, Math.Log);

                case InbuiltMethods.Log:
                    return TranslateMathsFunction(context, "log", isFunctionCall, Math.Log10);

                case InbuiltMethods.PowE:
                    return TranslateMathsFunction(context, "e ^", isFunctionCall, x => Math.Pow(Math.E, x));

                case InbuiltMethods.Pow10:
                    return TranslateMathsFunction(context, "10 ^", isFunctionCall, x => Math.Pow(10, x));

                default:
                    return null;
            }
        }

        /// <summary>
        /// Translates the method call as a non standard maths function.
        /// </summary>
        /// <param name="context">The context of the translation.</param>
        /// <param name="opName">The Scratch block spec of the operator.</param>
        /// <param name="isFunctionCall">Whether it is a function call.</param>
        /// <param name="optimiser">The function used to produce an actual value, when the inputs are known.</param>
        /// <returns>The tranlated call.</returns>
        private object TranslateMathsFunction(TranslationContext context, string opName, bool isFunctionCall,
            Func<double, double> optimiser)
        {
            if (!isFunctionCall)
            {
                context.ErrorList.Add(new CompilerError($"'{MethodName}' cannot be used as a statement",
                    ErrorType.ImproperUsage, ErrorToken, FileName));
                return new Block(null);
            }

            if (Parameters.Count == 1)
            {
                object translatedParam = Parameters[0].Translate(context);
                return double.TryParse(translatedParam.ToString(), out double value)
                    ? (object) optimiser(value)
                    : new Block(BlockSpecs.ComputeFunction, opName, Parameters[0].Translate(context));
            }

            context.ErrorList.Add(new CompilerError($"Expected 1 parameter on method '{MethodName}'",
                ErrorType.InvalidArgument, ErrorToken, FileName));
            return new Block(null);
        }

        #endregion
    }
}