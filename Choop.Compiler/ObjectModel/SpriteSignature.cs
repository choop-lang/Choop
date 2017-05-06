using Choop.Compiler.ChoopModel;
using System;
using System.Collections.ObjectModel;

namespace Choop.Compiler.ObjectModel
{
    /// <summary>
    /// Represents the signature for a sprite.
    /// </summary>
    public class SpriteSignature : ISpriteSignature
    {
        #region Properties
        private Project project;

        /// <summary>
        /// Gets or sets the name of the sprite.
        /// </summary>
        public string Name { get; set; }

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
        /// Gets the collection of scopes for the event handlers in the sprite.
        /// </summary>
        public Collection<Scope> EventHandlers { get; } = new Collection<Scope>();

        /// <summary>
        /// Gets the collection of user-defined methods in the sprite.
        /// </summary>
        public Collection<MethodSignature> Methods { get; } = new Collection<MethodSignature>();

        /// <summary>
        /// Gets the project which contains the sprite.
        /// </summary>
        public Project Project
        {
            get { return project; }
        }
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
                if (method.Name.Equals(name, Project.IdentifierComparisonMode))
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
            foreach (Scope scope in module.EventHandlers)
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
        public ConstSignature GetConstant(string name)
        {
            return GetItem(name, Constants, project.Constants);
        }

        /// <summary>
        /// Finds the variable with the specified name within the sprite and project.
        /// </summary>
        /// <param name="name">The name of the variable to search for.</param>
        /// <returns>The signature of the variable with the specified name; null if not found.</returns>
        public VarSignature GetVariable(string name)
        {
            return GetItem(name, Variables, project.Variables);
        }

        /// <summary>
        /// Finds the array with the specified name within the sprite and project.
        /// </summary>
        /// <param name="name">The name of the array to search for.</param>
        /// <returns>The signature of the array with the specified name; null if not found.</returns>
        public VarSignature GetArray(string name)
        {
            return GetItem(name, Arrays, project.Arrays);
        }

        /// <summary>
        /// Finds the list with the specified name within the sprite and project.
        /// </summary>
        /// <param name="name">The name of the list to search for.</param>
        /// <returns>The signature of the list with the specified name; null if not found.</returns>
        public VarSignature GetList(string name)
        {
            return GetItem(name, Lists, project.Lists);
        }

        /// <summary>
        /// Finds the item with the specified name and signature type within the sprite or project.
        /// </summary>
        /// <param name="name">The name of the item to search for.</param>
        /// <returns>The signature of the item with the specified name; null if not found.</returns>
        private T GetItem<T>(string name, Collection<T> locals, Collection<T> globals) where T : class, ITypedSignature
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
        public void Register(Project project)
        {
            if (Project == null)
            {
                // Not previously registered
                this.project = project;
            }
            else
            {
                // Previously registered
                throw new InvalidOperationException("Sprite already registered to another project.");
            }
        }
        #endregion
    }
}
