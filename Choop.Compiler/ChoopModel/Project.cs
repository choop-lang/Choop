using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Antlr4.Runtime;
using Choop.Compiler.BlockModel;
using Choop.Compiler.ChoopModel.Declarations;
using Choop.Compiler.ChoopModel.Methods;
using Choop.Compiler.ChoopModel.Sprites;
using Choop.Compiler.Helpers;
using Choop.Compiler.ProjectModel;
using Choop.Compiler.Properties;
using EventHandler = Choop.Compiler.ChoopModel.Methods.EventHandler;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents an entire Choop project.
    /// </summary>
    public class Project : ISpriteDeclaration, ICompilable<Stage>
    {
        #region Properties

        /// <summary>
        /// Gets or sets the name of the project.
        /// </summary>
        public string Name { get; set; }

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
        /// Gets or sets the settings of the project.
        /// </summary>
        public ProjectSettings Settings { get; set; }

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
            item => item.Name.Equals(name, Helpers.Settings.IdentifierComparisonMode));

        /// <summary>
        /// Gets the translated code for the grammar structure.
        /// </summary>
        /// <returns>The translated code for the grammar structure.</returns>
        public Stage Translate(TranslationContext context)
        {
            // Create blank stage instance
            Stage stage = new Stage();

            // Create new translation context
            TranslationContext newContext = new TranslationContext(this, context);

            // Translate superglobal variables
            foreach (GlobalVarDeclaration globalVarDeclaration in Variables)
                stage.Variables.Add(globalVarDeclaration.Translate(newContext));

            // Translate superglobal lists
            foreach (GlobalListDeclaration globalListDeclaration in Lists)
                stage.Lists.Add(globalListDeclaration.Translate(newContext));

            // Translate sprites and get statistics
            int spriteCount = 0;
            int scriptCount = 0;
            foreach (Sprite translated in Sprites.Select(x => x.Translate(newContext)))
            {
                stage.Children.Add(translated);

                // Update statistics
                scriptCount += translated.Scripts.Count;
                spriteCount++;
            }

            // Get pen layer md5
            using (MemoryStream ms = new MemoryStream())
            {
                Settings.PenLayerImage.Save(ms, ImageFormat.Png);
                stage.PenLayerMd5 = ms.ToArray().GetMd5Checksum() + Helpers.Settings.PngExtension;
            }

            // Backdrops
            foreach (Asset backdrop in Settings.Backdrops)
            {
                // Find backdrop file
                if (!context.ProjectAssets.CostumeFiles.TryGetValue(backdrop.Path, out LoadedAsset backdropData))
                {
                    context.ErrorList.Add(new CompilerError($"Backdrop '{backdrop.Path}' could not be found",
                        ErrorType.FileNotFound, null, Helpers.Settings.ProjectSettingsFile));
                    return null;
                }

                // Create backdrop
                // TODO bitmap resolution
                dynamic rotationCenter = backdrop.Attributes.rotationCenter;
                stage.Costumes.Add(new Costume
                {
                    Name = backdrop.Name,
                    Id = backdropData.Id,
                    BitmapResolution = 1,
                    Md5 = backdropData.Contents.GetMd5Checksum() + backdropData.Extension,
                    RotationCenter = new Point((int)rotationCenter.x.Value, (int)rotationCenter.y.Value)
                });
            }

            // Store project info / stats
            stage.Info = new ProjectInfo
            {
                SpriteCount = spriteCount,
                ScriptCount = scriptCount
            };

            // Insert Choop notice
            stage.Comments.Add(new Comment
            {
                Text = Resources.ChoopNotice,
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