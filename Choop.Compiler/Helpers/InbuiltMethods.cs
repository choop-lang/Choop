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
        public const string PlayNoteForBeates = "PlayNoteForBeats";
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
    }
}