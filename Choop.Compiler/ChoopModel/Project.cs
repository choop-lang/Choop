using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using Antlr4.Runtime;
using Choop.Compiler.BlockModel;
using Choop.Compiler.TranslationUtils;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents an entire Choop project.
    /// </summary>
    public class Project : ISpriteDeclaration, ICompilable<Stage>
    {
        #region Properties

        /// <summary>
        /// Gets the name of the project.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the collection of superglobal constant declarations. (Not compiled)
        /// </summary>
        public Collection<ConstDeclaration> Constants { get; } = new Collection<ConstDeclaration>();

        /// <summary>
        /// Gets the collection of superglobal variable declarations.
        /// </summary>
        public Collection<GlobalVarDeclaration> Variables { get; } = new Collection<GlobalVarDeclaration>();

        /// <summary>
        /// Gets the collection of superglobal list declarations.
        /// </summary>
        public Collection<GlobalListDeclaration> Lists { get; } = new Collection<GlobalListDeclaration>();

        /// <summary>
        /// Gets the collection of sprites in the project.
        /// </summary>
        public Collection<SpriteDeclaration> Sprites { get; } = new Collection<SpriteDeclaration>();

        /// <summary>
        /// Gets the collection of modules in the project.
        /// </summary>
        public Collection<ModuleDeclaration> Modules { get; } = new Collection<ModuleDeclaration>();

        /// <summary>
        /// Unused. Gets the collection of event handlers.
        /// </summary>
        /// <exception cref="NotSupportedException"></exception>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete]
        public Collection<EventHandler> EventHandlers => null;

        /// <summary>
        /// Unused. Gets the collection of method declarations.
        /// </summary>
        /// <exception cref="NotSupportedException"></exception>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete]
        public Collection<MethodDeclaration> Methods => null;

        /// <summary>
        /// Unused. Gets the token to report any compiler errors to.
        /// </summary>
        /// <exception cref="NotSupportedException"></exception>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete]
        public IToken ErrorToken => throw new NotSupportedException();

        /// <summary>
        /// Gets the file name where the grammar structure was found.
        /// </summary>
        /// <exception cref="NotSupportedException"></exception>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete]
        public string FileName => throw new NotSupportedException();

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="Project"/> class.
        /// </summary>
        /// <param name="name">The name of the project.</param>
        public Project(string name)
        {
            Name = name;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Finds the constant with the specified name within the project superglobals.
        /// </summary>
        /// <param name="name">The name of the constant to search for.</param>
        /// <returns>The declaration of the constant with the specified name; null if not found.</returns>
        public ConstDeclaration GetConstant(string name) => GetItem(name, Constants);

        /// <summary>
        /// Finds the variable with the specified name within the project superglobals.
        /// </summary>
        /// <param name="name">The name of the variable to search for.</param>
        /// <returns>The declaration of the variable with the specified name; null if not found.</returns>
        public GlobalVarDeclaration GetVariable(string name) => GetItem(name, Variables);

        /// <summary>
        /// Finds the list or array with the specified name within the project superglobals.
        /// </summary>
        /// <param name="name">The name of the list to search for.</param>
        /// <returns>The declaration of the list with the specified name; null if not found.</returns>
        public GlobalListDeclaration GetList(string name) => GetItem(name, Lists);

        /// <summary>
        /// Finds the sprite with the specified name.
        /// </summary>
        /// <param name="name">The name of the sprite to search for.</param>
        /// <returns>The declaration of the sprite with the specified name; null if not found.</returns>
        public SpriteDeclaration GetSprite(string name) => GetItem(name, Sprites);

        /// <summary>
        /// Finds the module with the specified name.
        /// </summary>
        /// <param name="name">The name of the module to search for.</param>
        /// <returns>The declaration of the module with the specified name; null if not found.</returns>
        public ModuleDeclaration GetModule(string name) => GetItem(name, Modules);

        /// <summary>
        /// Finds the declaration with the specified name at the project level.
        /// </summary>
        /// <param name="name">The name of the declaration to search for.</param>
        /// <returns>The declaration with the specified name; null if not found.</returns>
        public IDeclaration GetDeclaration(string name) => GetItem(name, Constants) ??
                                                           GetItem(name, Variables) ??
                                                           GetItem(name, Lists) ??
                                                           GetItem(name, Sprites) ??
                                                           (IDeclaration) GetItem(name, Modules);

        /// <summary>
        /// Finds the item with the specified name and type within the project superglobals.
        /// </summary>
        /// <param name="name">The name of the item to search for.</param>
        /// <param name="locals">The collection of local items to search inside.</param>
        /// <returns>The declaration of the item with the specified name; null if not found.</returns>
        private static T GetItem<T>(string name, IEnumerable<T> locals)
            where T : class, IDeclaration => locals.FirstOrDefault(
            item => item.Name.Equals(name, Settings.IdentifierComparisonMode));

        /// <summary>
        /// Gets the translated code for the grammar structure.
        /// </summary>
        /// <returns>The translated code for the grammar structure.</returns>
        public Stage Translate(TranslationContext context)
        {
            // Create blank stage instance
            Stage stage = new Stage();

            // Translate superglobal variables
            foreach (GlobalVarDeclaration globalVarDeclaration in Variables)
                stage.Variables.Add(globalVarDeclaration.Translate(context));

            // Translate superglobal lists
            foreach (GlobalListDeclaration globalListDeclaration in Lists)
                stage.Lists.Add(globalListDeclaration.Translate(context));

            // Translate sprites and get statistics
            int spriteCount = 0;
            int scriptCount = 0;
            foreach (SpriteDeclaration spriteDeclaration in Sprites)
            {
                // Translate sprite
                Sprite translated = spriteDeclaration.Translate(context);
                stage.Children.Add(translated);

                // Update statistics
                scriptCount += translated.Scripts.Count;
                spriteCount++;
            }

            // Insert default costume
            // TODO use meta file
            stage.Costumes.Add(new Costume
            {
                Name = "backdrop1",
                Id = 3,
                Md5 = "739b5e2a2435f6e1ec2993791b423146.png",
                BitmapResolution = 1,
                RotationCenter = new Point(240, 180)
            });

            // Store project info / stats
            stage.Info = new ProjectInfo
            {
                SpriteCount = spriteCount,
                ScriptCount = scriptCount
            };

            // Insert Choop notice
            stage.Comments.Add(new Comment
            {
                Text = Properties.Resources.ChoopNotice,
                Location = new Point(20, 20),
                Size = new Size(500, 200),
                Open = true,
                BlockId = -1
            });

            return stage;
        }

        #endregion
    }
}