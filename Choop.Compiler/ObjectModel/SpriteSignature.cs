using Choop.Compiler.ChoopModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Choop.Compiler.ObjectModel
{
    /// <summary>
    /// Represents the signature for a sprite.
    /// </summary>
    public class SpriteSignature : ISpriteSignature
    {
        #region Properties

        /// <summary>
        /// Gets the name of the sprite.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the collection of constants that are local to the sprite.
        /// </summary>
        public Collection<ConstSignature> Constants { get; } = new Collection<ConstSignature>();

        /// <summary>
        /// Gets the collection of variables that are local to the sprite.
        /// </summary>
        public Collection<VarSignature> Variables { get; } = new Collection<VarSignature>();

        /// <summary>
        /// Gets the collection of arrays that are local to the sprite.
        /// </summary>
        public Collection<VarSignature> Arrays { get; } = new Collection<VarSignature>();

        /// <summary>
        /// Gets the collection of lists that are local to the sprite.
        /// </summary>
        public Collection<VarSignature> Lists { get; } = new Collection<VarSignature>();

        /// <summary>
        /// Gets the collection of event handlers in the sprite.
        /// </summary>
        public Collection<EventHandlerSignature> EventHandlers { get; } = new Collection<EventHandlerSignature>();

        /// <summary>
        /// Gets the collection of user-defined methods in the sprite.
        /// </summary>
        public Collection<MethodSignature> Methods { get; } = new Collection<MethodSignature>();

        /// <summary>
        /// Gets the project which contains the sprite.
        /// </summary>
        public Project Project { get; private set; }

        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="SpriteSignature"/> class. 
        /// </summary>
        /// <param name="name">The name of the sprite.</param>
        public SpriteSignature(string name)
        {
            Name = name;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Finds the method which has the specified name and is compatible with the specified parameter types.
        /// </summary>
        /// <param name="name">The name of the method.</param>
        /// <param name="paramTypes">The types of each of the supplied parameters, in order.</param>
        /// <returns>The signature of the method if found; otherwise null.</returns>
        public MethodSignature GetMethod(string name, params DataType[] paramTypes)
        {
            foreach (MethodSignature method in Methods)
            {
                // Check name matches
                if (!method.Name.Equals(name, Project.IdentifierComparisonMode)) continue;
                // Check valid amount of parameters
                if (paramTypes.Length > method.Params.Count) continue;
                
                // Default to valid
                bool valid = true;

                // Check each parameter
                for (int i = 0; i < method.Params.Count; i++)
                {
                    if (i < paramTypes.Length)
                    {
                        // Check parameter types are compatible
                        if (method.Params[i].Type.IsCompatible(paramTypes[i])) continue;

                        // Not compatible
                        valid = false;
                        break;
                    }

                    // These parameters weren't specified, so they must be optional
                    if (method.Params[i].Optional) continue;

                    // Not optional
                    valid = false;
                    break;
                }

                // Return method if valid
                if (valid)
                    return method;
            }

            // Not found
            return null;
        }

        /// <summary>
        /// Imports the specified module.
        /// </summary>
        /// <param name="module">The module to import.</param>
        public void Import(ModuleSignature module)
        {
            // Constants
            foreach (ConstSignature constant in module.Constants)
                Constants.Add(constant);

            // Variables
            foreach (VarSignature variable in module.Variables)
                Variables.Add(variable);

            // Arrays
            foreach (VarSignature array in module.Arrays)
                Arrays.Add(array);

            // Lists
            foreach (VarSignature list in module.Lists)
                Lists.Add(list);

            // Event handlers
            foreach (EventHandlerSignature scope in module.EventHandlers)
                EventHandlers.Add(scope);

            // Methods
            foreach (MethodSignature method in module.Methods)
                Methods.Add(method);
        }

        /// <summary>
        /// Finds the constant with the specified name within the sprite and project.
        /// </summary>
        /// <param name="name">The name of the constant to search for.</param>
        /// <returns>The signature of the constant with the specified name; null if not found.</returns>
        public ConstSignature GetConstant(string name) => GetItem(name, Constants, Project.Constants);

        /// <summary>
        /// Finds the variable with the specified name within the sprite and project.
        /// </summary>
        /// <param name="name">The name of the variable to search for.</param>
        /// <returns>The signature of the variable with the specified name; null if not found.</returns>
        public VarSignature GetVariable(string name) => GetItem(name, Variables, Project.Variables);

        /// <summary>
        /// Finds the array with the specified name within the sprite and project.
        /// </summary>
        /// <param name="name">The name of the array to search for.</param>
        /// <returns>The signature of the array with the specified name; null if not found.</returns>
        public VarSignature GetArray(string name) => GetItem(name, Arrays, Project.Arrays);

        /// <summary>
        /// Finds the list with the specified name within the sprite and project.
        /// </summary>
        /// <param name="name">The name of the list to search for.</param>
        /// <returns>The signature of the list with the specified name; null if not found.</returns>
        public VarSignature GetList(string name) => GetItem(name, Lists, Project.Lists);

        /// <summary>
        /// Finds the item with the specified name and signature type within the sprite or project.
        /// </summary>
        /// <param name="name">The name of the item to search for.</param>
        /// <param name="locals">The collection of local items to search inside.</param>
        /// <param name="globals">The collection of global items to search inside.</param>
        /// <returns>The signature of the item with the specified name; null if not found.</returns>
        private static T GetItem<T>(string name, IEnumerable<T> locals, IEnumerable<T> globals) where T : class, ITypedSignature
        {
            // Local
            foreach (T item in locals)
                if (item.Name.Equals(name, Project.IdentifierComparisonMode))
                    return item;
            
            // Global
            foreach (T item in globals)
                if (item.Name.Equals(name, Project.IdentifierComparisonMode))
                    return item;

            // Not found
            return null;
        }

        /// <summary>
        /// Registers the sprite with the specified project object. For internal use only.
        /// </summary>
        /// <param name="project">The project to register the sprite with.</param>
        /// <exception cref="InvalidOperationException"></exception>
        internal void Register(Project project)
        {
            // Check if previously registered
            if (Project != null)
                throw new InvalidOperationException("Sprite already registered to another project.");
            
            // Not previously registered
            Project = project;
        }
        #endregion
    }
}
