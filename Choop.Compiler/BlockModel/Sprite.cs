﻿using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using Choop.Compiler.Helpers;
using Newtonsoft.Json.Linq;

namespace Choop.Compiler.BlockModel
{
    /// <summary>
    /// Represents a sprite.
    /// </summary>
    public class Sprite : ISprite, IMonitor, IComponent
    {
        #region Properties

        /// <summary>
        /// Gets or sets the name of the sprite.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the collection of variables in the sprite.
        /// </summary>
        public Collection<Variable> Variables { get; } = new Collection<Variable>();

        /// <summary>
        /// Gets the collection of lists in the sprite.
        /// </summary>
        public Collection<List> Lists { get; } = new Collection<List>();

        /// <summary>
        /// Gets the collection of scripts in the sprite. 
        /// </summary>
        public Collection<ScriptTuple> Scripts { get; } = new Collection<ScriptTuple>();

        /// <summary>
        /// Gets the collection of script comments in the sprite.
        /// </summary>
        public Collection<Comment> Comments { get; } = new Collection<Comment>();

        /// <summary>
        /// Gets the collection of sounds in the sprite.
        /// </summary>
        public Collection<Sound> Sounds { get; } = new Collection<Sound>();

        /// <summary>
        /// Gets the collection of costumes in the sprite.
        /// </summary>
        public Collection<Costume> Costumes { get; } = new Collection<Costume>();

        /// <summary>
        /// Gets or sets the zero-based index of the current costume of the sprite.
        /// </summary>
        public int CurrentCostume { get; set; } = 0;

        /// <summary>
        /// Gets or sets the location of the sprite.
        /// </summary>
        public Point Location { get; set; } = Point.Empty;

        /// <summary>
        /// Gets or sets the scale of the sprite. (Default is 1)
        /// </summary>
        public double Scale { get; set; } = 1;

        /// <summary>
        /// Gets or sets the angle of the sprite in degrees from the upwards direction. (Default is 90)
        /// </summary>
        public double Direction { get; set; } = 90;

        /// <summary>
        /// Gets or sets the rotation style of the sprite. (Default is normal)
        /// </summary>
        public RotationType RotationStyle { get; set; } = RotationType.Normal;

        /// <summary>
        /// Gets or sets whether the sprite is draggable in the player. (Default is false)
        /// </summary>
        public bool Draggable { get; set; } = false;

        /// <summary>
        /// Gets or sets the one-based index of the sprite in the list of sprites.
        /// </summary>
        public int LibraryIndex { get; set; } = 1;

        /// <summary>
        /// Gets or sets whether the sprite is visible. (Default is true)
        /// </summary>
        public bool Visible { get; set; } = true;

        /// <summary>
        /// Gets or sets the info for the sprite. (Unused)
        /// </summary>
        public object SpriteInfo { get; set; } = null;

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
                {"objName", Name},
                {"variables", new JArray(Variables.Select(x => x.ToJson()))},
                {"lists", new JArray(Lists.Select(x => x.ToJson()))},
                {"scripts", new JArray(Scripts.Select(x => x.ToJson()))},
                {"scriptComments", new JArray(Comments.Select(x => x.ToJson()))},
                {"sounds", new JArray(Sounds.Select(x => x.ToJson())) },
                {"costumes", new JArray(Costumes.Select(x => x.ToJson())) },
                {"currentCostumeIndex", CurrentCostume },
                {"scratchX", Location.X },
                {"scratchY", Location.Y },
                {"scale", Scale },
                {"direction", Direction },
                {"rotationStyle", RotationStyle.ToSerializedString()},
                {"isDraggable", Draggable },
                {"indexInLibrary", LibraryIndex },
                {"visible", Visible },
                {"spriteInfo", new JValue(SpriteInfo) }
            };
        }

        #endregion
    }
}