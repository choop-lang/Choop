using Choop.Compiler.ChoopModel;
using System;

namespace Choop.Compiler.ObjectModel
{
    /// <summary>
    /// Provides extension methods for the <see cref="ISpriteSignature"/> interface. 
    /// </summary>
    public static class SpriteSignatureExtension
    {
        #region Methods
        /// <summary>
        /// Finds the method which has the specified name and is compatible with the specified parameter types.
        /// </summary>
        /// <param name="name">The name of the method.</param>
        /// <param name="paramTypes">The types of each of the supplied parameters, in order.</param>
        /// <returns>The signature of the method if found; otherwise null.</returns>
        public static MethodSignature GetMethod(this ISpriteSignature sprite, string name, params DataType[] paramTypes)
        {
            foreach (MethodSignature method in sprite.Methods)
            {
                // Check name matches
                if (method.Name.Equals(name, StageSignature.IdentifierComparisonMode))
                {
                    // Check valid amount of parameters
                    if (paramTypes.Length <= method.Params.Count)
                    {
                        // Default to valid
                        bool Valid = true;

                        // Check each parameter
                        for (int i = 0; i < method.Params.Count; i++)
                        {
                            if (i < paramTypes.Length)
                            {
                                // Check parameter types are compatible
                                if (!method.Params[i].Type.IsCompatible(paramTypes[i]))
                                {
                                    Valid = false;
                                    break;
                                }
                            }
                            else
                            {
                                // These parameters weren't specified, so they must be optional
                                if (!method.Params[i].Optional)
                                {
                                    Valid = false;
                                    break;
                                }
                            }
                        }

                        // Return method if valid
                        if (Valid)
                            return method;
                    }
                }
            }

            // Not found
            return null;
        }

        /// <summary>
        /// Imports the specified module.
        /// </summary>
        /// <param name="module">The module to import.</param>
        public static void Import(this ISpriteSignature sprite, ModuleSignature module)
        {
            // Constants
            foreach (ConstSignature constant in module.Constants)
                sprite.Constants.Add(constant);

            // Variables
            foreach (VarSignature variable in module.Variables)
                sprite.Variables.Add(variable);

            // Arrays
            foreach (VarSignature array in module.Arrays)
                sprite.Arrays.Add(array);

            // Lists
            foreach (VarSignature list in module.Lists)
                sprite.Lists.Add(list);

            // Event handlers
            foreach (Scope scope in module.EventHandlers)
                sprite.EventHandlers.Add(scope);

            // Methods
            foreach (MethodSignature method in module.Methods)
                sprite.Methods.Add(method);
        }
        #endregion
    }
}
