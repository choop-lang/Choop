using System;
using System.Drawing;
using Antlr4.Runtime;
using Choop.Compiler.BlockModel;
using Choop.Compiler.TranslationUtils;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents a stage declaration.
    /// </summary>
    public class StageDeclaration : SpriteBaseDeclaration, ICompilable<Stage>
    {
        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="StageDeclaration"/> class.
        /// </summary>
        /// <param name="metaFile">The file path to the metadata file for the stage.</param>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="errorToken">The token to report any compiler errors to.</param>
        public StageDeclaration(string metaFile, string fileName, IToken errorToken) : base(Settings.StageName, metaFile, fileName, errorToken)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the translated code for the grammar structure.
        /// </summary>
        /// <returns>The translated code for the grammar structure.</returns>
        public Stage Translate(TranslationContext context)
        {
            // Create blank stage instance
            Stage stage = new Stage();

            // TODO: Import modules

            // Variables
            foreach (GlobalVarDeclaration globalVarDeclaration in Variables)
                stage.Variables.Add(globalVarDeclaration.Translate(context));

            // Lists
            foreach (GlobalListDeclaration globalListDeclaration in Lists)
                stage.Lists.Add(globalListDeclaration.Translate(context));

            // Events
            foreach (EventHandler eventHandler in EventHandlers)
            {
                Tuple<BlockModel.EventHandler, BlockDef> translated = eventHandler.Translate(context);
                stage.Scripts.Add(translated.Item1);
                stage.Scripts.Add(translated.Item2);
            }

            // Methods
            foreach (MethodDeclaration methodDeclaration in Methods)
                stage.Scripts.Add(methodDeclaration.Translate(context));

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

            return stage;
        }

        #endregion
    }
}