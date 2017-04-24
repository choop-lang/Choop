namespace Choop.Compiler.BlockModel
{
    /// <summary>
    /// Specifies the different types of <see cref="StageMonitor"/> objects. 
    /// </summary>
    public enum MonitorCmd
    {
        /// <summary>
        /// Indicates the answer monitor.
        /// </summary>
        Answer,
        /// <summary>
        /// Indicates the backdrop number monitor.
        /// </summary>
        BackgroundIndex,
        /// <summary>
        /// Indicates the costume number monitor of the target sprite.
        /// </summary>
        CostumeIndex,
        /// <summary>
        /// Indicates a variable monitor.
        /// </summary>
        GetVar,
        /// <summary>
        /// Indicates the direction monitor of the target sprite.
        /// </summary>
        Heading,
        /// <summary>
        /// Indicates the size monitor of the target sprite.
        /// </summary>
        Scale,
        /// <summary>
        /// Indicates the backdrop name monitor.
        /// </summary>
        SceneName,
        /// <summary>
        /// Indicates the video motion monitor of the target object.
        /// </summary>
        SenseVideoMotion,
        /// <summary>
        /// Indicates the loudness monitor.
        /// </summary>
        SoundLevel,
        /// <summary>
        /// Indicates the tempo monitor.
        /// </summary>
        Tempo,
        /// <summary>
        /// Indicates the time monitor for the specified time unit.
        /// </summary>
        TimeAndDate,
        /// <summary>
        /// Indicates the timer monitor.
        /// </summary>
        Timer,
        /// <summary>
        /// Indicates the volume monitor of the target sprite.
        /// </summary>
        Volume,
        /// <summary>
        /// Indicates the x position monitor of the target sprite.
        /// </summary>
        XPos,
        /// <summary>
        /// Indicates the y position monitor of the target sprite.
        /// </summary>
        YPos
    }
}
