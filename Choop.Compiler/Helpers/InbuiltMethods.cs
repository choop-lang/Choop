using System.Collections.Generic;
using Choop.Compiler.BlockModel;
using Choop.Compiler.ChoopModel;

namespace Choop.Compiler.Helpers
{
    /// <summary>
    /// Specifies all the inbuilt Choop methods.
    /// </summary>
    public static class InbuiltMethods
    {
        #region Method Names

        // Events
        public const string Broadcast = "Broadcast";
        public const string BroadcastAndWait = "BroadcastAndWait";

        // Control
        public const string Wait = "Wait";
        public const string WaitUntil = "WaitUntil";
        public const string CreateCloneOf = "CreateCloneOf";
        public const string DeleteThisClone = "DeleteThisClone";
        public const string StopAll = "StopAll";
        public const string StopOtherScriptsInSprite = "StopOtherScriptsInSprite";

        // Sensing
        public const string Touching = "Touching";
        public const string TouchingColor = "TouchingColor";
        public const string ColorTouchingColor = "ColorTouchingColor";
        public const string AskAndWait = "AskAndWait";
        public const string Answer = "Answer";
        public const string DistanceTo = "DistanceTo";
        public const string KeyPressed = "KeyPressed";
        public const string MouseDown = "MouseDown";
        public const string MouseX = "MouseX";
        public const string MouseY = "MouseY";
        public const string Loudness = "Loudness";
        public const string VideoSensorOn = "VideoSensorOn";
        public const string SetVideo = "SetVideo";
        public const string SetVideoTransparency = "SetVideoTransparency";
        public const string Timer = "Timer";
        public const string ResetTimer = "ResetTimer";
        public const string GetAttributeOf = "GetAttributeOf";
        public const string CurrentDateTime = "CurrentDateTime";
        public const string DaysSince2000 = "DaysSince2000";
        public const string Username = "Username";

        // Operators
        public const string Random = "Random";
        public const string GetLetter = "GetLetter";
        public const string StringLength = "StringLength";
        public const string Round = "Round";
        public const string ComputeFunction = "ComputeFunction";
        public const string Abs = "Abs";
        public const string Floor = "Floor";
        public const string Ceiling = "Ceiling";
        public const string Sqrt = "Sqrt";
        public const string Sin = "Sin";
        public const string Cos = "Cos";
        public const string Tan = "Tan";
        public const string Asin = "Asin";
        public const string Acos = "Acos";
        public const string Atan = "Atan";
        public const string Ln = "Ln";
        public const string Log = "Log";
        public const string PowE = "PowE";
        public const string Pow10 = "Pow10";

        // Motion
        public const string MoveSteps = "MoveSteps";
        public const string TurnRight = "TurnRight";
        public const string TurnLeft = "TurnLeft";
        public const string PointInDirection = "PointInDirection";
        public const string PointTowards = "PointTowards";
        public const string GoToXY = "GoToXY";
        public const string GoToSprite = "GoToSprite";
        public const string GlideSecsToXY = "GlideSecsToXY";
        public const string ChangeXBy = "ChangeXBy";
        public const string SetX = "SetX";
        public const string ChangeYBy = "ChangeYBy";
        public const string SetY = "SetY";
        public const string IfOnEdgeBounce = "IfOnEdgeBounce";
        public const string SetRotationStyle = "SetRotationStyle";
        public const string X = "X";
        public const string Y = "Y";
        public const string Direction = "Direction";

        // Looks
        public const string SayForSecs = "SayForSecs";
        public const string Say = "Say";
        public const string ThinkForSecs = "ThinkForSecs";
        public const string Think = "Think";
        public const string Show = "Show";
        public const string Hide = "Hide";
        public const string SwitchCostumeTo = "SwitchCostumeTo";
        public const string NextCostume = "NextCostume";
        public const string SwitchBackdropTo = "SwitchBackdropTo";
        public const string SwitchBackdropToAndWait = "SwitchBackdropToAndWait";
        public const string NextBackdrop = "NextBackdrop";
        public const string ChangeEffectBy = "ChangeEffectBy";
        public const string SetEffectTo = "SetEffectTo";
        public const string ClearGraphicEffects = "ClearGraphicEffects";
        public const string ChangeSizeBy = "ChangeSizeBy";
        public const string SetSize = "SetSize";
        public const string GoToFront = "GoToFront";
        public const string GoBackLayers = "GoBackLayers";
        public const string CostumeName = "CostumeName";
        public const string CostumeNumber = "CostumeNumber";
        public const string BackdropName = "BackdropName";
        public const string BackdropNumber = "BackdropNumber";
        public const string Size = "Size";

        // Sound
        public const string PlaySound = "PlaySound";
        public const string PlaySoundUntilDone = "PlaySoundUntilDone";
        public const string StopAllSounds = "StopAllSounds";
        public const string PlayDrumForBeats = "PlayDrumForBeats";
        public const string RestForBeats = "RestForBeats";
        public const string PlayNoteForBeats = "PlayNoteForBeats";
        public const string SetInstrument = "SetInstrument";
        public const string SetMidiInstrument = "SetMidiInstrument";
        public const string ChangeVolumeBy = "ChangeVolumeBy";
        public const string SetVolume = "SetVolume";
        public const string Volume = "Volume";
        public const string ChangeTempoBy = "ChangeTempoBy";
        public const string SetTempo = "SetTempo";
        public const string Tempo = "Tempo";

        // Pen
        public const string Clear = "Clear";
        public const string Stamp = "Stamp";
        public const string PenDown = "PenDown";
        public const string PenUp = "PenUp";
        public const string SetPenRGB = "SetPenRGB";
        public const string ChangePenHueBy = "ChangePenHueBy";
        public const string SetPenHue = "SetPenHue";
        public const string ChangePenShadeBy = "ChangePenShadeBy";
        public const string SetPenShade = "SetPenShade";
        public const string ChangePenSizeBy = "ChangePenSizeBy";
        public const string SetPenSize = "SetPenSize";

        // Variables
        public const string SetVarTo = "SetVarTo";
        public const string ChangeVarBy = "ChangeVarBy";
        public const string ShowVariable = "ShowVariable";
        public const string HideVariable = "HideVariable";

        // Lists
        public const string AddToList = "AddToList";
        public const string DeleteItemOfList = "DeleteItemOfList";
        public const string InsertItemInList = "InsertItemInList";
        public const string ReplaceItemOfList = "ReplaceItemOfList";
        public const string GetItemOfList = "GetItemOfList";
        public const string ListLength = "ListLength";
        public const string ListContains = "ListContains";
        public const string ShowList = "ShowList";
        public const string HideList = "HideList";

        #endregion

        #region Dictionaries

        /// <summary>
        /// Represents the mapping from Choop methods to standard Scratch blocks.
        /// </summary>
        public static readonly Dictionary<string, MethodSignature> StandardMethods =
            new Dictionary<string, MethodSignature>
        {
            // Events
            {Broadcast, new MethodSignature(BlockSpecs.Broadcast, false, DataType.Object, "message")},
            {BroadcastAndWait, new MethodSignature(BlockSpecs.BroadcastAndWait, false, DataType.Object, "message")},

            // Control
            {Wait, new MethodSignature(BlockSpecs.WaitSecs, false, DataType.Object, "seconds") },
            {WaitUntil, new MethodSignature(BlockSpecs.WaitUntil, false, DataType.Object, "condition") },
            {CreateCloneOf, new MethodSignature(BlockSpecs.CreateCloneOf, false, DataType.Object, "sprite") },
            {DeleteThisClone, new MethodSignature(BlockSpecs.DeleteThisClone, false, DataType.Object)},

            // Sensing
            {Touching, new MethodSignature(BlockSpecs.TouchingSprite, true, DataType.Boolean, "sprite name or mouse or edge") },
            {TouchingColor, new MethodSignature(BlockSpecs.TouchingColor, true, DataType.Boolean, "color") },
            {ColorTouchingColor, new MethodSignature(BlockSpecs.ColorIsTouchingColor, true, DataType.Boolean, "color 1", "color 2")},
            {AskAndWait, new MethodSignature(BlockSpecs.AskAndWait, false, DataType.Object, "message") },
            {Answer, new MethodSignature(BlockSpecs.Answer, true, DataType.String) },
            {DistanceTo, new MethodSignature(BlockSpecs.DistanceTo, true, DataType.Number, "sprite or mouse") },
            {KeyPressed, new MethodSignature(BlockSpecs.KeyPressed, true, DataType.Boolean, "key") },
            {MouseDown, new MethodSignature(BlockSpecs.MouseDown, true, DataType.Boolean) },
            {MouseX, new MethodSignature(BlockSpecs.MouseX, true, DataType.Number) },
            {MouseY, new MethodSignature(BlockSpecs.MouseY, true, DataType.Number) },
            {Loudness, new MethodSignature(BlockSpecs.Loudness, true, DataType.Number)},
            {VideoSensorOn, new MethodSignature(BlockSpecs.VideoMotionOrDirectionOn, true, DataType.Number, "motion or direction", "sprite") },
            {SetVideo, new MethodSignature(BlockSpecs.TurnVideoOnOff, false, DataType.Object, "on or off") },
            {SetVideoTransparency, new MethodSignature(BlockSpecs.SetVideoTranparencyTo, false, DataType.Object, "percentage") },
            {Timer, new MethodSignature(BlockSpecs.Timer, true, DataType.Number) },
            {ResetTimer, new MethodSignature(BlockSpecs.ResetTimer, false, DataType.Object) },
            {GetAttributeOf, new MethodSignature(BlockSpecs.GetAttribute, true, DataType.Object, "attribute", "sprite")},
            {CurrentDateTime, new MethodSignature(BlockSpecs.CurrentTime, true, DataType.Number, "year date day of week etc.")},
            {DaysSince2000, new MethodSignature(BlockSpecs.DaysSince2000, true, DataType.Number)},
            {Username, new MethodSignature(BlockSpecs.GetUsername, true, DataType.String) },

            // Operators
            {Random, new MethodSignature(BlockSpecs.PickRandomTo, true, DataType.Number, "min", "max") },
            {GetLetter, new MethodSignature(BlockSpecs.GetLetterOf, true, DataType.String, "index", "text")},
            {StringLength, new MethodSignature(BlockSpecs.LengthOfString, true, DataType.Number, "text") },
            {Round, new MethodSignature(BlockSpecs.Round, true, DataType.Number, "value") },
            {ComputeFunction, new MethodSignature(BlockSpecs.ComputeFunction, true, DataType.Number, "function", "value") },

            // Motion
            {MoveSteps, new MethodSignature(BlockSpecs.MoveSteps, false, DataType.Object, "steps") },
            {TurnRight, new MethodSignature(BlockSpecs.TurnClockwiseDegrees, false, DataType.Object, "angle")},
            {TurnLeft, new MethodSignature(BlockSpecs.TurnCounterClockwiseDegrees, false, DataType.Object, "angle") },
            {PointInDirection, new MethodSignature(BlockSpecs.PointInDirection, false, DataType.Object, "angle")},
            {PointTowards, new MethodSignature(BlockSpecs.PointTowards, false, DataType.Object, "sprite or mouse") },
            {GoToXY, new MethodSignature(BlockSpecs.GoToXy, false, DataType.Object, "x", "y")},
            {GoToSprite, new MethodSignature(BlockSpecs.GoToSprite, false, DataType.Object, "sprite or mouse or random position") },
            {GlideSecsToXY, new MethodSignature(BlockSpecs.GlideSecsToXy, false, DataType.Object, "seconds", "x", "y")},
            {ChangeXBy, new MethodSignature(BlockSpecs.ChangeXBy, false, DataType.Object, "value")},
            {SetX, new MethodSignature(BlockSpecs.SetXTo, false, DataType.Object, "value") },
            {ChangeYBy, new MethodSignature(BlockSpecs.ChangeYBy, false, DataType.Object, "value") },
            {SetY, new MethodSignature(BlockSpecs.SetYTo, false, DataType.Object, "value") },
            {IfOnEdgeBounce, new MethodSignature(BlockSpecs.IfOnEdgeBounce, false, DataType.Object)},
            {SetRotationStyle, new MethodSignature(BlockSpecs.SetRotationStyle, false, DataType.Object, "left-right or don't rotate or all around") },
            {X, new MethodSignature(BlockSpecs.XPos, true, DataType.Number) },
            {Y, new MethodSignature(BlockSpecs.YPos, true, DataType.Number) },
            {Direction, new MethodSignature(BlockSpecs.Direction, true, DataType.Number) },

            // Looks
            {SayForSecs, new MethodSignature(BlockSpecs.SayForSecs, false, DataType.Object, "message", "seconds")},
            {Say, new MethodSignature(BlockSpecs.Say, false, DataType.Object, "message") },
            {ThinkForSecs, new MethodSignature(BlockSpecs.ThinkForSecs, false, DataType.Object, "message", "seconds") },
            {Think, new MethodSignature(BlockSpecs.Think, false, DataType.Object, "message") },
            {Show, new MethodSignature(BlockSpecs.Show, false, DataType.Object) },
            {Hide, new MethodSignature(BlockSpecs.Hide, false, DataType.Object) },
            {SwitchCostumeTo, new MethodSignature(BlockSpecs.SwitchCostumeTo, false, DataType.Object, "costume") },
            {NextCostume, new MethodSignature(BlockSpecs.NextCostume, false, DataType.Object) },
            {SwitchBackdropTo, new MethodSignature(BlockSpecs.SwitchBackdropTo, false, DataType.Object, "backdrop or next backdrop or previous backdrop") },
            {SwitchBackdropToAndWait, new MethodSignature(BlockSpecs.SwitchBackdropToAndWait, false, DataType.Object, "backdrop or next backdrop or previous backdrop") },
            {NextBackdrop, new MethodSignature(BlockSpecs.NextBackdrop, false, DataType.Object) },
            {ChangeEffectBy, new MethodSignature(BlockSpecs.ChangeEffectBy, false, DataType.Object, "graphic effect", "value")},
            {SetEffectTo, new MethodSignature(BlockSpecs.SetGraphicEffectTo, false, DataType.Object, "graphic effect", "value")},
            {ClearGraphicEffects, new MethodSignature(BlockSpecs.ClearGraphicEffects, false, DataType.Object) },
            {ChangeSizeBy, new MethodSignature(BlockSpecs.ChangeSizeBy, false, DataType.Object, "value")},
            {SetSize, new MethodSignature(BlockSpecs.SetSizeTo, false, DataType.Object, "value") },
            {GoToFront, new MethodSignature(BlockSpecs.GoToFront, false, DataType.Object) },
            {GoBackLayers, new MethodSignature(BlockSpecs.GoBackLayers, false, DataType.Object, "layers") },
            {CostumeName, new MethodSignature(BlockSpecs.CostumeName, true, DataType.String) },
            {CostumeNumber, new MethodSignature(BlockSpecs.CostumeNumber, true, DataType.Number) },
            {BackdropName, new MethodSignature(BlockSpecs.BackdropName, true, DataType.String) },
            {BackdropNumber, new MethodSignature(BlockSpecs.BackdropNumber, true, DataType.Number) },
            {Size, new MethodSignature(BlockSpecs.Size, true, DataType.Number) },

            // Sound
            {PlaySound, new MethodSignature(BlockSpecs.PlaySound, false, DataType.Object, "sound") },
            {PlaySoundUntilDone, new MethodSignature(BlockSpecs.PlaySoundUntilDone, false, DataType.Object, "sound") },
            {StopAllSounds, new MethodSignature(BlockSpecs.StopAllSounds, false, DataType.Object) },
            {PlayDrumForBeats, new MethodSignature(BlockSpecs.PlayDrumForBeats, false, DataType.Object, "drum", "beats") },
            {RestForBeats, new MethodSignature(BlockSpecs.RestForBeats, false, DataType.Object, "beats") },
            {PlayNoteForBeats, new MethodSignature(BlockSpecs.PlayNoteForBeats, false, DataType.Object, "note", "beats") },
            {SetInstrument, new MethodSignature(BlockSpecs.SetInstrumentTo, false, DataType.Object, "instrument") },
            {SetMidiInstrument, new MethodSignature(BlockSpecs.SetInstrumentToOld, false, DataType.Object, "MIDI instrument") },
            {ChangeVolumeBy, new MethodSignature(BlockSpecs.ChangeVolumeBy, false, DataType.Object, "value") },
            {SetVolume, new MethodSignature(BlockSpecs.SetVolumeTo, false, DataType.Object, "value") },
            {Volume, new MethodSignature(BlockSpecs.Volume, true, DataType.Number) },
            {ChangeTempoBy, new MethodSignature(BlockSpecs.ChangeTempoBy, false, DataType.Object, "value") },
            {SetTempo, new MethodSignature(BlockSpecs.SetTempoTo, false, DataType.Object, "value") },
            {Tempo, new MethodSignature(BlockSpecs.Tempo, true, DataType.Number) },

            // Pen
            {Clear, new MethodSignature(BlockSpecs.Clear, false, DataType.Object) },
            {Stamp, new MethodSignature(BlockSpecs.Stamp,false, DataType.Object) },
            {PenDown, new MethodSignature(BlockSpecs.PenDown, false, DataType.Object) },
            {PenUp, new MethodSignature(BlockSpecs.PenUp, false, DataType.Object) },
            {SetPenRGB, new MethodSignature(BlockSpecs.SetPenRgbTo, false, DataType.Object, "ARGB") },
            {ChangePenHueBy, new MethodSignature(BlockSpecs.ChangePenColorBy, false, DataType.Object, "value")},
            {SetPenHue, new MethodSignature(BlockSpecs.SetPenHueTo, false, DataType.Object, "value") },
            {ChangePenShadeBy, new MethodSignature(BlockSpecs.ChangePenShadeBy, false, DataType.Object, "value") },
            {SetPenShade, new MethodSignature(BlockSpecs.SetPenShadeTo, false, DataType.Object, "value") },
            {ChangePenSizeBy, new MethodSignature(BlockSpecs.ChangePenSizeBy, false, DataType.Object, "value") },
            {SetPenSize, new MethodSignature(BlockSpecs.SetPenSizeTo, false, DataType.Object, "value")},

            // Variables
            {SetVarTo, new MethodSignature(BlockSpecs.SetVariableTo, false, DataType.Object, "variable", "value") },
            {ChangeVarBy, new MethodSignature(BlockSpecs.ChangeVarBy, false, DataType.Object, ChangeVarBy, "variable", "value") },
            {ShowVariable, new MethodSignature(BlockSpecs.ShowVariable, false, DataType.Object, "variable") },
            {HideVariable, new MethodSignature(BlockSpecs.HideVariable, false, DataType.Object, "variable") },

            // Lists
            {AddToList, new MethodSignature(BlockSpecs.AddToList, false, DataType.Object, "value", "list")},
            {DeleteItemOfList, new MethodSignature(BlockSpecs.DeleteItemOfList, false, DataType.Object, "item", "list") },
            {InsertItemInList, new MethodSignature(BlockSpecs.InsertItemInList, false, DataType.Object, "item", "index", "list") },
            {ReplaceItemOfList, new MethodSignature(BlockSpecs.ReplaceItemOfList, false, DataType.Object, "index", "list", "value")},
            {GetItemOfList, new MethodSignature(BlockSpecs.GetItemOfList, true, DataType.Object, "index", "list") },
            {ListLength, new MethodSignature(BlockSpecs.LengthOfList, true, DataType.Number, "list") },
            {ListContains, new MethodSignature(BlockSpecs.ListContains, true, DataType.Boolean, "list", "item") },
            {ShowList, new MethodSignature(BlockSpecs.ShowList, false, DataType.Object, "list") },
            {HideList, new MethodSignature(BlockSpecs.HideList, false, DataType.Object, "list") }
        };

        /// <summary>
        /// Represents the mapping from Choop methods to inbuilt non-standard Scratch blocks.
        /// </summary>
        public static readonly Dictionary<string, MethodSignature> NonStandardMethods =
            new Dictionary<string, MethodSignature>
            {
                // Control
                {StopAll, new MethodSignature(BlockSpecs.Stop, false, DataType.Object)},
                {StopOtherScriptsInSprite, new MethodSignature(BlockSpecs.Stop, false, DataType.Object) },

                // Operators
                {Abs, new MethodSignature(BlockSpecs.ComputeFunction, true, DataType.Number, "value") },
                {Floor, new MethodSignature(BlockSpecs.ComputeFunction, true, DataType.Number, "value") },
                {Ceiling, new MethodSignature(BlockSpecs.ComputeFunction, true, DataType.Number, "value") },
                {Sqrt, new MethodSignature(BlockSpecs.ComputeFunction, true, DataType.Number, "value") },
                {Sin, new MethodSignature(BlockSpecs.ComputeFunction, true, DataType.Number, "value") },
                {Cos, new MethodSignature(BlockSpecs.ComputeFunction, true, DataType.Number, "value") },
                {Tan, new MethodSignature(BlockSpecs.ComputeFunction, true, DataType.Number, "value") },
                {Asin, new MethodSignature(BlockSpecs.ComputeFunction, true, DataType.Number, "value") },
                {Acos, new MethodSignature(BlockSpecs.ComputeFunction, true, DataType.Number, "value") },
                {Atan, new MethodSignature(BlockSpecs.ComputeFunction, true, DataType.Number, "value") },
                {Ln, new MethodSignature(BlockSpecs.ComputeFunction, true, DataType.Number, "value") },
                {Log, new MethodSignature(BlockSpecs.ComputeFunction, true, DataType.Number, "value") },
                {PowE, new MethodSignature(BlockSpecs.ComputeFunction, true, DataType.Number, "value") },
                {Pow10, new MethodSignature(BlockSpecs.ComputeFunction, true, DataType.Number, "value") },
            };

        #endregion
    }
}