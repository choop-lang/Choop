using System.Collections.Generic;
using Choop.Compiler.ChoopModel;

namespace Choop.Compiler.Helpers
{
    /// <summary>
    /// Contains the block spec for every Scratch block.
    /// </summary>
    public static class BlockSpecs
    {
        #region Block Specs

        public const string Minus = "-";
        public const string Times = "*";
        public const string Divide = "/";
        public const string And = "&";
        public const string Mod = "%";
        public const string Add = "+";
        public const string LessThan = "<";
        public const string Equal = "=";
        public const string GreaterThan = ">";
        public const string Or = "|";
        public const string Answer = "answer";
        public const string AddToList = "append:toList:";
        public const string BackdropNumber = "backgroundIndex";
        public const string IfOnEdgeBounce = "bounceOffEdge";
        public const string Broadcast = "broadcast:";
        public const string CustomMethodCall = "call";
        public const string ChangeEffectBy = "changeGraphicEffect:by:";
        public const string ChangePenColorBy = "changePenHueBy:";
        public const string ChangePenShadeBy = "changePenShadeBy:";
        public const string ChangePenSizeBy = "changePenSizeBy:";
        public const string ChangeSizeBy = "changeSizeBy:";
        public const string ChangeTempoBy = "changeTempoBy:";
        public const string ChangeVarBy = "changeVar:by:";
        public const string ChangeVolumeBy = "changeVolumeBy:";
        public const string ChangeXBy = "changeXposBy:";
        public const string ChangeYBy = "changeYposBy:";
        public const string Clear = "clearPenTrails";
        public const string ColorIsTouchingColor = "color:sees:";
        public const string GoToFront = "comeToFront";
        public const string ComputeFunction = "computeFunction:of:";
        public const string Join = "concatenate:with:";
        public const string ListReporter = "contentsOfList:";
        public const string CostumeNumber = "costumeIndex";
        public const string CostumeName = "costumeName";
        public const string CreateCloneOf = "createCloneOf";
        public const string DeleteThisClone = "deleteClone";
        public const string DeleteItemOfList = "deleteLine:ofList:";
        public const string DistanceTo = "distanceTo:";
        public const string AskAndWait = "doAsk";
        public const string BroadcastAndWait = "doBroadcastAndWait";
        public const string Forever = "doForever";
        public const string ForEachIn = "doForLoop";
        public const string IfThen = "doIf";
        public const string IfThenElse = "doIfElse";
        public const string PlaySoundUntilDone = "doPlaySoundAndWait";
        public const string Repeat = "doRepeat";
        public const string RepeatUntil = "doUntil";
        public const string WaitUntil = "doWaitUntil";
        public const string While = "doWhile";
        public const string PlayDrumForBeats = "drum:duration:elapsed:from:";
        public const string ClearGraphicEffects = "filterReset";
        public const string MoveSteps = "forward:";
        public const string GetAttribute = "getAttribute:of:";
        public const string GetItemOfList = "getLine:ofList:";
        public const string GetParameter = "getParam";
        public const string GetUsername = "getUserName";
        public const string GlideSecsToXy = "glideSecs:toX:y:elapsed:from:";
        public const string GoBackLayers = "goBackByLayers:";
        public const string GoToSprite = "gotoSpriteOrMouse:";
        public const string GoToXy = "gotoX:y:";
        public const string Direction = "heading";
        public const string PointInDirection = "heading:";
        public const string Hide = "hide";
        public const string HideList = "hideList:";
        public const string HideVariable = "hideVariable:";
        public const string InsertItemInList = "insert:at:ofList:";
        public const string SetInstrumentTo = "instrument:";
        public const string KeyPressed = "keyPressed:";
        public const string GetLetterOf = "letter:of:";
        public const string LengthOfList = "lineCountOfList:";
        public const string ListContains = "list:contains:";
        public const string SwitchCostumeTo = "lookLike:";
        public const string SetInstrumentToOld = "midiInstrument:";
        public const string MouseDown = "mousePressed";
        public const string MouseX = "mouseX";
        public const string MouseY = "mouseY";
        public const string NextCostume = "nextCostume";
        public const string NextBackdrop = "nextScene";
        public const string Not = "not";
        public const string PlayNoteForBeats = "noteOn:duration:elapsed:from:";
        public const string SetPenRgbTo = "penColor:";
        public const string SetPenSizeTo = "penSize:";
        public const string PlaySound = "playSound:";
        public const string PointTowards = "pointTowards:";
        public const string MethodDeclaration = "procDef";
        public const string PenDown = "putPenDown";
        public const string PenUp = "putPenUp";
        public const string GetVariable = "readVariable";
        public const string PickRandomTo = "randomFrom:to:";
        public const string RestForBeats = "rest:elapsed:from:";
        public const string Round = "rounded";
        public const string Say = "say:";
        public const string SayForSecs = "say:duration:elapsed:from:";
        public const string Size = "scale";
        public const string BackdropName = "sceneName";
        public const string VideoMotionOrDirectionOn = "senseVideoMotion";
        public const string SetGraphicEffectTo = "setGraphicEffect:to:";
        public const string ReplaceItemOfList = "setLine:ofList:to:";
        public const string SetPenHueTo = "setPenHueTo:";
        public const string SetPenShadeTo = "setPenShadeTo:";
        public const string SetRotationStyle = "setRotationStyle";
        public const string SetSizeTo = "setSizeTo:";
        public const string SetTempoTo = "setTempoTo:";
        public const string SetVariableTo = "setVar:to:";
        public const string TurnVideoOnOff = "setVideoState";
        public const string SetVideoTranparencyTo = "setVideoTransparency";
        public const string SetVolumeTo = "setVolumeTo:";
        public const string Show = "show";
        public const string ShowList = "showList:";
        public const string ShowVariable = "showVariable:";
        public const string Loudness = "soundLevel";
        public const string Stamp = "stampCostume";
        public const string SwitchBackdropTo = "startScene";
        public const string SwitchBackdropToAndWait = "startSceneAndWait";
        public const string StopAllSounds = "stopAllSounds";
        public const string Stop = "stopScripts";
        public const string LengthOfString = "stringLength:";
        public const string Tempo = "tempo";
        public const string Think = "think:";
        public const string ThinkForSecs = "think:duration:elapsed:from:";
        public const string CurrentTime = "timeAndDate";
        public const string Timer = "timer";
        public const string ResetTimer = "timerReset";
        public const string DaysSince2000 = "timestamp";
        public const string TouchingSprite = "touching:";
        public const string TouchingColor = "touchingColor:";
        public const string TurnCounterClockwiseDegrees = "turnLeft:";
        public const string TurnClockwiseDegrees = "turnRight:";
        public const string Volume = "volume";
        public const string WaitSecs = "wait:elapsed:from:";
        public const string WhenSpriteClicked = "whenClicked";
        public const string WhenSpriteCloned = "whenCloned";
        public const string WhenGreenFlagClicked = "whenGreenFlag";
        public const string WhenIReceive = "whenIReceive";
        public const string WhenKeyPressed = "whenKeyPressed";
        public const string WhenBackdropSwitchesTo = "whenSceneStarts";
        public const string WhenSensorGreaterThan = "whenSensorGreaterThan";
        public const string XPos = "xpos";
        public const string SetXTo = "xpos:";
        public const string YPos = "ypos";
        public const string SetYTo = "ypos:";

        #endregion

        #region Input Codes

        public const string InputBool = "%b";
        public const string InputColor = "%c";
        public const string InputNum = "%n";
        public const string InputString = "%s";

        #endregion

        #region Inbuilt Dictionary

        /// <summary>
        /// Represents the mapping from Choop methods to inbuilt common Scratch blocks.
        /// </summary>
        public static readonly Dictionary<string, MethodSignature> Inbuilt = new Dictionary<string, MethodSignature>
        {
            // Events
            {"Broadcast", new MethodSignature(Broadcast, false, DataType.Object, "message")},
            {"BroadcastAndWait", new MethodSignature(BroadcastAndWait, false, DataType.Object, "message")},

            // Control
            {"Wait", new MethodSignature(WaitSecs, false, DataType.Object, "seconds") },
            {"WaitUntil", new MethodSignature(WaitUntil, false, DataType.Object, "condition") },
            {"CreateCloneOf", new MethodSignature(CreateCloneOf, false, DataType.Object, "sprite") },
            {"DeleteThisClone", new MethodSignature(DeleteThisClone, false, DataType.Object)},

            // Sensing
            {"Touching", new MethodSignature(TouchingSprite, true, DataType.Boolean, "sprite name or mouse or edge") },
            {"TouchingColor", new MethodSignature(TouchingColor, true, DataType.Boolean, "color") },
            {"ColorTouchingColor", new MethodSignature(ColorIsTouchingColor, true, DataType.Boolean, "color 1", "color 2")},
            {"AskAndWait", new MethodSignature(AskAndWait, false, DataType.Object, "message") },
            {"Answer", new MethodSignature(Answer, true, DataType.String) },
            {"DistanceTo", new MethodSignature(DistanceTo, true, DataType.Number, "sprite or mouse") },
            {"KeyPressed", new MethodSignature(KeyPressed, true, DataType.Boolean, "key") },
            {"MouseDown", new MethodSignature(MouseDown, true, DataType.Boolean) },
            {"MouseX", new MethodSignature(MouseX, true, DataType.Number) },
            {"MouseY", new MethodSignature(MouseY, true, DataType.Number) },
            {"Loudness", new MethodSignature(Loudness, true, DataType.Number)},
            {"VideoSensorOn", new MethodSignature(VideoMotionOrDirectionOn, true, DataType.Number, "motion or direction", "sprite") },
            {"SetVideo", new MethodSignature(TurnVideoOnOff, false, DataType.Object, "on or off") },
            {"SetVideoTransparency", new MethodSignature(SetVideoTranparencyTo, false, DataType.Object, "percentage") },
            {"Timer", new MethodSignature(Timer, true, DataType.Number) },
            {"ResetTimer", new MethodSignature(ResetTimer, false, DataType.Object) },
            {"GetAttributeOf", new MethodSignature(GetAttribute, true, DataType.Object, "attribute", "sprite")},
            {"CurrentDateTime", new MethodSignature(CurrentTime, true, DataType.Number, "year date day of week etc.")},
            {"DaysSince2000", new MethodSignature(DaysSince2000, true, DataType.Number)},
            {"Username", new MethodSignature(GetUsername, true, DataType.String) },

            // Operators
            {"Random", new MethodSignature(PickRandomTo, true, DataType.Number, "min", "max") },
            {"GetLetter", new MethodSignature(GetLetterOf, true, DataType.String, "index", "text")},
            {"StringLength", new MethodSignature(LengthOfString, true, DataType.Number, "text") },
            {"Round", new MethodSignature(Round, true, DataType.Number, "value") },
            {"ComputeFunction", new MethodSignature(ComputeFunction, true, DataType.Number, "function", "value") },

            // Motion
            {"MoveSteps", new MethodSignature(MoveSteps, false, DataType.Object, "steps") },
            {"TurnRight", new MethodSignature(TurnClockwiseDegrees, false, DataType.Object, "angle")},
            {"TurnLeft", new MethodSignature(TurnCounterClockwiseDegrees, false, DataType.Object, "angle") },
            {"PointInDirection", new MethodSignature(PointInDirection, false, DataType.Object, "angle")},
            {"PointTowards", new MethodSignature(PointTowards, false, DataType.Object, "sprite or mouse") },
            {"GoToXY", new MethodSignature(GoToXy, false, DataType.Object, "x", "y")},
            {"GoToSprite", new MethodSignature(GoToSprite, false, DataType.Object, "sprite or mouse or random position") },
            {"GlideSecsToXY", new MethodSignature(GlideSecsToXy, false, DataType.Object, "seconds", "x", "y")},
            {"ChangeXBy", new MethodSignature(ChangeXBy, false, DataType.Object, "value")},
            {"SetX", new MethodSignature(SetXTo, false, DataType.Object, "value") },
            {"ChangeYBy", new MethodSignature(ChangeYBy, false, DataType.Object, "value") },
            {"SetY", new MethodSignature(SetYTo, false, DataType.Object, "value") },
            {"IfOnEdgeBounce", new MethodSignature(IfOnEdgeBounce, false, DataType.Object)},
            {"SetRotationStyle", new MethodSignature(SetRotationStyle, false, DataType.Object, "left-right or don't rotate or all around") },
            {"X", new MethodSignature(XPos, true, DataType.Number) },
            {"Y", new MethodSignature(YPos, true, DataType.Number) },
            {"Direction", new MethodSignature(Direction, true, DataType.Number) },

            // Looks
            {"SayForSecs", new MethodSignature(SayForSecs, false, DataType.Object, "message", "seconds")},
            {"Say", new MethodSignature(Say, false, DataType.Object, "message") },
            {"ThinkForSecs", new MethodSignature(ThinkForSecs, false, DataType.Object, "message", "seconds") },
            {"Think", new MethodSignature(Think, false, DataType.Object, "message") },
            {"Show", new MethodSignature(Show, false, DataType.Object) },
            {"Hide", new MethodSignature(Hide, false, DataType.Object) },
            {"SwitchCostumeTo", new MethodSignature(SwitchCostumeTo, false, DataType.Object, "costume") },
            {"NextCostume", new MethodSignature(NextCostume, false, DataType.Object) },
            {"SwitchBackdropTo", new MethodSignature(SwitchBackdropTo, false, DataType.Object, "backdrop or next backdrop or previous backdrop") },
            {"SwitchBackdropToAndWaut", new MethodSignature(SwitchBackdropToAndWait, false, DataType.Object, "backdrop or next backdrop or previous backdrop") },
            {"NextBackdrop", new MethodSignature(NextBackdrop, false, DataType.Object) },
            {"ChangeEffectBy", new MethodSignature(ChangeEffectBy, false, DataType.Object, "graphic effect", "value")},
            {"SetEffectTo", new MethodSignature(SetGraphicEffectTo, false, DataType.Object, "graphic effect", "value")},
            {"ClearGraphicEffects", new MethodSignature(ClearGraphicEffects, false, DataType.Object) },
            {"ChangeSizeBy", new MethodSignature(ChangeSizeBy, false, DataType.Object, "value")},
            {"SetSize", new MethodSignature(SetSizeTo, false, DataType.Object, "value") },
            {"GoToFront", new MethodSignature(GoToFront, false, DataType.Object) },
            {"GoBackLayers", new MethodSignature(GoBackLayers, false, DataType.Object, "layers") },
            {"CostumeName", new MethodSignature(CostumeName, true, DataType.String) },
            {"CostumeNumber", new MethodSignature(CostumeNumber, true, DataType.Number) },
            {"BackdropName", new MethodSignature(BackdropName, true, DataType.String) },
            {"BackdropNumber", new MethodSignature(BackdropNumber, true, DataType.Number) },
            {"Size", new MethodSignature(Size, true, DataType.Number) },

            // Sound
            {"PlaySound", new MethodSignature(PlaySound, false, DataType.Object, "sound") },
            {"PlaySoundUntilDone", new MethodSignature(PlaySoundUntilDone, false, DataType.Object, "sound") },
            {"StopAllSounds", new MethodSignature(StopAllSounds, false, DataType.Object) },
            {"PlayDrumForBeats", new MethodSignature(PlayDrumForBeats, false, DataType.Object, "drum", "beats") },
            {"RestForBeats", new MethodSignature(RestForBeats, false, DataType.Object, "beats") },
            {"PlayNoteForBeats", new MethodSignature(PlayNoteForBeats, false, DataType.Object, "note", "beats") },
            {"SetInstrument", new MethodSignature(SetInstrumentTo, false, DataType.Object, "instrument") },
            {"SetMidiInstrument", new MethodSignature(SetInstrumentToOld, false, DataType.Object, "MIDI instrument") },
            {"ChangeVolumeBy", new MethodSignature(ChangeVolumeBy, false, DataType.Object, "value") },
            {"SetVolume", new MethodSignature(SetVolumeTo, false, DataType.Object, "value") },
            {"Volume", new MethodSignature(Volume, true, DataType.Number) },
            {"ChangeTempoBy", new MethodSignature(ChangeTempoBy, false, DataType.Object, "value") },
            {"SetTempo", new MethodSignature(SetTempoTo, false, DataType.Object, "value") },
            {"Tempo", new MethodSignature(Tempo, true, DataType.Number) },

            // Pen
            {"Clear", new MethodSignature(Clear, false, DataType.Object) },
            {"Stamp", new MethodSignature(Stamp,false, DataType.Object) },
            {"PenDown", new MethodSignature(PenDown, false, DataType.Object) },
            {"PenUp", new MethodSignature(PenUp, false, DataType.Object) },
            {"SetPenRGB", new MethodSignature(SetPenRgbTo, false, DataType.Object, "ARGB") },
            {"ChangePenHueBy", new MethodSignature(ChangePenColorBy, false, DataType.Object, "value")},
            {"SetPenHue", new MethodSignature(SetPenHueTo, false, DataType.Object, "value") },
            {"ChangePenShadeBy", new MethodSignature(ChangePenShadeBy, false, DataType.Object, "value") },
            {"SetPenShade", new MethodSignature(SetPenShadeTo, false, DataType.Object, "value") },
            {"ChangePenSizeBy", new MethodSignature(ChangePenSizeBy, false, DataType.Object, "value") },
            {"SetPenSize", new MethodSignature(SetPenSizeTo, false, DataType.Object, "value")},

            // Variables
            {"SetVarTo", new MethodSignature(SetVariableTo, false, DataType.Object, "variable", "value") },
            {"ChangeVarBy", new MethodSignature(ChangeVarBy, false, DataType.Object, ChangeVarBy, "variable", "value") },
            {"ShowVariable", new MethodSignature(ShowVariable, false, DataType.Object, "variable") },
            {"HideVariable", new MethodSignature(HideVariable, false, DataType.Object, "variable") },

            // Lists
            {"AddToList", new MethodSignature(AddToList, false, DataType.Object, "value", "list")},
            {"DeleteItemOfList", new MethodSignature(DeleteItemOfList, false, DataType.Object, "item", "list") },
            {"InsertItemInList", new MethodSignature(InsertItemInList, false, DataType.Object, "item", "index", "list") },
            {"ReplaceItemOfList", new MethodSignature(ReplaceItemOfList, false, DataType.Object, "index", "list", "value")},
            {"GetItemOfList", new MethodSignature(GetItemOfList, true, DataType.Object, "index", "list") },
            {"ListLength", new MethodSignature(LengthOfList, true, DataType.Number, "list") },
            {"ListContains", new MethodSignature(ListContains, true, DataType.Boolean, "list", "item") },
            {"ShowList", new MethodSignature(ShowList, false, DataType.Object, "list") },
            {"HideList", new MethodSignature(HideList, false, DataType.Object, "list") }
        };

        /*
         * Blocks not included in Inbuilt:
         * 
         * (Control blocks, hat blocks, etc.)
         * Stop ()
         * Mathematical Functions (Sqrt, Abs, etc.)
         * Custom blocks
         * Get variable / parameter
         * Get list string contents
         */

        #endregion
    }
}