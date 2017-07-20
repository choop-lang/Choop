using System.Collections.Generic;

namespace Choop.Compiler.TranslationUtils
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
        public static readonly Dictionary<string, MethodSignature> Inbuilt = new Dictionary<string, MethodSignature>()
        {
            // Events
            {"Broadcast", new MethodSignature(Broadcast, false, "message")},
            {"BroadcastAndWait", new MethodSignature(BroadcastAndWait, false, "message")},

            // Control
            {"Wait", new MethodSignature(WaitSecs, false, "seconds") },
            {"WaitUntil", new MethodSignature(WaitUntil, false, "condition") },
            {"CreateCloneOf", new MethodSignature(CreateCloneOf, false, "sprite") },
            {"DeleteThisClone", new MethodSignature(DeleteThisClone, false)},

            // Sensing
            {"Touching", new MethodSignature(TouchingSprite, true, "sprite name or mouse or edge") },
            {"TouchingColor", new MethodSignature(TouchingColor, true, "color") },
            {"ColorTouchingColor", new MethodSignature(ColorIsTouchingColor, true, "color 1", "color 2")},
            {"DistanceTo", new MethodSignature(DistanceTo, true, "sprite or mouse") },
            {"KeyPressed", new MethodSignature(KeyPressed, true, "key") },
            {"MouseDown", new MethodSignature(MouseDown, true) },
            {"MouseX", new MethodSignature(MouseX, true) },
            {"MouseY", new MethodSignature(MouseY, true) },
            {"Loudness", new MethodSignature(Loudness, true)},
            {"VideoSensorOn", new MethodSignature(VideoMotionOrDirectionOn, true, "motion or direction", "sprite") },
            {"SetVideo", new MethodSignature(TurnVideoOnOff, false, "on or off") },
            {"SetVideoTransparency", new MethodSignature(SetVideoTranparencyTo, false, "percentage") },
            {"Timer", new MethodSignature(Timer, true) },
            {"ResetTimer", new MethodSignature(ResetTimer, false) },
            {"GetAttributeOf", new MethodSignature(GetAttribute, true, "attribute", "sprite")},
            {"CurrentDateTime", new MethodSignature(CurrentTime, true, CurrentTime)},
            {"DaysSince2000", new MethodSignature(DaysSince2000, true)},
            {"Username", new MethodSignature(GetUsername, true) },

            // Operators
            {"Random", new MethodSignature(PickRandomTo, true, "min", "max") },
            {"GetLetter", new MethodSignature(GetLetterOf, true, "index", "text")},
            {"StringLength", new MethodSignature(LengthOfString, true, "text") },
            {"Round", new MethodSignature(Round, true, "value") },
            {"ComputeFunction", new MethodSignature(ComputeFunction, true, "function", "value") },

            // Motion
            {"MoveSteps", new MethodSignature(MoveSteps, false, "steps") },
            {"TurnRight", new MethodSignature(TurnClockwiseDegrees, false, "angle")},
            {"TurnLeft", new MethodSignature(TurnCounterClockwiseDegrees, false, "angle") },
            {"PointInDirection", new MethodSignature(PointInDirection, false, "angle")},
            {"PointTowards", new MethodSignature(PointTowards, false, "sprite or mouse") },
            {"GoToXY", new MethodSignature(GoToXy, false, "x", "y")},
            {"GoToSprite", new MethodSignature(GoToSprite, false, "sprite or mouse or random position") },
            {"GlideSecsToXY", new MethodSignature(GlideSecsToXy, false, "seconds", "x", "y")},
            {"ChangeXBy", new MethodSignature(ChangeXBy, false, "value")},
            {"SetX", new MethodSignature(SetXTo, false, "value") },
            {"ChangeYBy", new MethodSignature(ChangeYBy, false, "value") },
            {"SetY", new MethodSignature(SetYTo, false, "value") },
            {"IfOnEdgeBounce", new MethodSignature(IfOnEdgeBounce, false)},
            {"SetRotationStyle", new MethodSignature(SetRotationStyle, false, "left-right or don't rotate or all around") },
            {"X", new MethodSignature(XPos, true) },
            {"Y", new MethodSignature(YPos, true) },
            {"Direction", new MethodSignature(Direction, true) },

            // Looks
            {"SayForSecs", new MethodSignature(SayForSecs, false, "message", "seconds")},
            {"Say", new MethodSignature(Say, false, "message") },
            {"ThinkForSecs", new MethodSignature(ThinkForSecs, false, "message", "seconds") },
            {"Think", new MethodSignature(Think, false, "message") },
            {"Show", new MethodSignature(Show, false) },
            {"Hide", new MethodSignature(Hide, false) },
            {"SwitchCostumeTo", new MethodSignature(SwitchCostumeTo, false, "costume") },
            {"NextCostume", new MethodSignature(NextCostume, false) },
            {"SwitchBackdropTo", new MethodSignature(SwitchBackdropTo, false, "backdrop or next backdrop or previous backdrop") },
            {"SwitchBackdropToAndWaut", new MethodSignature(SwitchBackdropToAndWait, false, "backdrop or next backdrop or previous backdrop") },
            {"NextBackdrop", new MethodSignature(NextBackdrop, false) },
            {"ChangeEffectBy", new MethodSignature(ChangeEffectBy, false, "graphic effect", "value")},
            {"SetEffectTo", new MethodSignature(SetGraphicEffectTo, false, "graphic effect", "value")},
            {"ClearGraphicEffects", new MethodSignature(ClearGraphicEffects, false) },
            {"ChangeSizeBy", new MethodSignature(ChangeSizeBy, false, "value")},
            {"SetSize", new MethodSignature(SetSizeTo, false, "value") },
            {"GoToFront", new MethodSignature(GoToFront, false) },
            {"GoBackLayers", new MethodSignature(GoBackLayers, false, "layers") },
            {"CostumeName", new MethodSignature(CostumeName, true) },
            {"CostumeNumber", new MethodSignature(CostumeNumber, true) },
            {"BackdropName", new MethodSignature(BackdropName, true) },
            {"BackdropNumber", new MethodSignature(BackdropNumber, true) },
            {"Size", new MethodSignature(Size, true) },

            // Sound
            {"PlaySound", new MethodSignature(PlaySound, false, "sound") },
            {"PlaySoundUntilDone", new MethodSignature(PlaySoundUntilDone, false, "sound") },
            {"StopAllSounds", new MethodSignature(StopAllSounds, false) },
            {"PlayDrumForBeats", new MethodSignature(PlayDrumForBeats, false, "drum", "beats") },
            {"RestForBeats", new MethodSignature(RestForBeats, false, "beats") },
            {"PlayNoteForBeats", new MethodSignature(PlayNoteForBeats, false, "note", "beats") },
            {"SetInstrument", new MethodSignature(SetInstrumentTo, false, "instrument") },
            {"SetMidiInstrument", new MethodSignature(SetInstrumentToOld, false, "MIDI instrument") },
            {"ChangeVolumeBy", new MethodSignature(ChangeVolumeBy, false, "value") },
            {"SetVolume", new MethodSignature(SetVolumeTo, false, "value") },
            {"Volume", new MethodSignature(Volume, true) },
            {"ChangeTempoBy", new MethodSignature(ChangeTempoBy, false, "value") },
            {"SetTempo", new MethodSignature(SetTempoTo, false, "value") },
            {"Tempo", new MethodSignature(Tempo, true) },

            // Pen
            {"Clear", new MethodSignature(Clear, false) },
            {"Stamp", new MethodSignature(Stamp,false) },
            {"PenDown", new MethodSignature(PenDown, false) },
            {"PenUp", new MethodSignature(PenUp, false) },
            {"SetPenRGB", new MethodSignature(SetPenRgbTo, false, "ARGB") },
            {"ChangePenHueBy", new MethodSignature(ChangePenColorBy, false, "value")},
            {"SetPenHue", new MethodSignature(SetPenHueTo, false, "value") },
            {"ChangePenShadeBy", new MethodSignature(ChangePenShadeBy, false, "value") },
            {"SetPenShade", new MethodSignature(SetPenShadeTo, false, "value") },
            {"ChangePenSizeBy", new MethodSignature(ChangePenSizeBy, false, "value") },
            {"SetPenSize", new MethodSignature(SetPenSizeTo, false, "value")},

            // Variables
            {"SetVarTo", new MethodSignature(SetVariableTo, false, "variable", "value") },
            {"ChangeVarBy", new MethodSignature(ChangeVarBy, false, ChangeVarBy, "variable", "value") },
            {"ShowVariable", new MethodSignature(ShowVariable, false, "variable") },
            {"HideVariable", new MethodSignature(HideVariable, false, "variable") },

            // Lists
            {"AddToList", new MethodSignature(AddToList, false, "value", "list")},
            {"DeleteItemOfList", new MethodSignature(DeleteItemOfList, false, "item", "list") },
            {"InsertItemInList", new MethodSignature(InsertItemInList, false, "item", "index", "list") },
            {"ReplaceItemOfList", new MethodSignature(ReplaceItemOfList, false, "index", "list", "value")},
            {"GetItemOfList", new MethodSignature(GetItemOfList, true, "index", "list") },
            {"ListLength", new MethodSignature(LengthOfList, true, "list") },
            {"ListContains", new MethodSignature(ListContains, true, "list", "item") },
            {"ShowList", new MethodSignature(ShowList, false, "list") },
            {"HideList", new MethodSignature(HideList, false, "list") }
        };

        /*
         * Blocks not included in Inbuilt:
         * 
         * (Control blocks, hat blocks, etc.)
         * Stop ()
         * Functions (sqrt, abs, etc.)
         * Ask () and wait, Answer
         * Custom blocks
         * Get variable
         * Get list string contents
         */

        #endregion
    }
}