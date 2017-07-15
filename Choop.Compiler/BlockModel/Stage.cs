using System;
using System.Collections.ObjectModel;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Choop.Compiler.BlockModel
{
    /// <summary>
    /// Represents the stage.
    /// </summary>
    public class Stage : ISprite
    {
        #region Properties

        /// <summary>
        /// Gets or sets the name of the stage.
        /// </summary>
        public string Name
        {
            get { return "Stage"; }
            set { throw new NotSupportedException(); }
        }

        /// <summary>
        /// Gets the collection of variables in the stage.
        /// </summary>
        public Collection<Variable> Variables { get; } = new Collection<Variable>();

        /// <summary>
        /// Gets the collection of lists in the stage.
        /// </summary>
        public Collection<List> Lists { get; } = new Collection<List>();

        /// <summary>
        /// Gets the collection of scripts in the stage. 
        /// </summary>
        public Collection<IScript> Scripts { get; } = new Collection<IScript>();

        /// <summary>
        /// Gets the collection of script comments in the stage.
        /// </summary>
        public Collection<Comment> Comments { get; } = new Collection<Comment>();

        /// <summary>
        /// Gets the collection of sounds in the stage.
        /// </summary>
        public Collection<Sound> Sounds { get; } = new Collection<Sound>();

        /// <summary>
        /// Gets the collection of backdrops in the stage.
        /// </summary>
        public Collection<Costume> Costumes { get; } = new Collection<Costume>();

        /// <summary>
        /// Gets or sets the zero-based index of the current backdrop of the stage.
        /// </summary>
        public int CurrentCostume { get; set; } = 0;

        /// <summary>
        /// Gets or sets the number of the image file in the project ZIP file containing the pen trails.
        /// </summary>
        public int PenLayer { get; set; } = 0;

        /// <summary>
        /// Gets or sets the MD5 hash name for the image of the pen trails.
        /// </summary>
        public string PenLayerMd5 { get; set; } = "";

        /// <summary>
        /// Gets or sets the tempo of the project in bpm.
        /// </summary>
        public double Tempo { get; set; } = 60;

        /// <summary>
        /// Gets or sets the video transparency of the project. (0 - 1)
        /// </summary>
        public double VideoAlpha { get; set; } = 0.5;

        /// <summary>
        /// Gets the collection of monitors in the stage.
        /// </summary>
        public Collection<IMonitor> Children { get; } = new Collection<IMonitor>();

        /// <summary>
        /// Gets or sets the info for the Scratch project.
        /// </summary>
        public ProjectInfo Info { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Serializes the current instance into a JSON object.
        /// </summary>
        /// <returns>The JSON representation of the current instance.</returns>
        public JToken ToJson()
        {
            return new JObject
            {
                {"objName", ChoopModel.Settings.StageName},
                {"variables", new JArray(Variables.Select(x => x.ToJson()))},
                {"lists", new JArray(Lists.Select(x => x.ToJson()))},
                {"scripts", new JArray(Scripts.Select(x => x.ToJson()))},
                {"scriptComments", new JArray(Comments.Select(x => x.ToJson()))},
                {"sounds", new JArray(Sounds.Select(x => x.ToJson()))},
                {"costumes", new JArray(Costumes.Select(x => x.ToJson()))},
                {"currentCostumeIndex", CurrentCostume},
                {"penLayerID", PenLayer},
                {"penLayerMD5", PenLayerMd5},
                {"tempoBPM", Tempo},
                {"videoAlpha", VideoAlpha},
                {"children", new JArray(Children.Select(x => x.ToJson()))},
                {"info", Info.ToJson()}
            };
        }

        #endregion
    }
}