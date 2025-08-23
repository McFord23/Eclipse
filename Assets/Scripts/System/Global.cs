public static class Global
{
    public static Mode Mode { get; private set; } = Mode.Map;
    
    public static bool IsPause;
    public static bool IsPlayerBlocked;
    
    public static float MouseSens = 10;
    public static float ScrollSens = 2f;

    public static float TimeScale { get; private set; } = 1f;

    public delegate void ModeEvent(Mode mode);
    public static event ModeEvent OnChangeMode;
    
    public delegate void Event();
    public static event Event OnChangeTimeScale;
    public static event Event OnChangeMapScale;

    public static Scale MapScale { get; private set; } = Scale.Transport;
    
    public static void SetTimeScale(float value)
    {
        TimeScale = value;
        OnChangeTimeScale?.Invoke();
    }

    public static void SetMapScale(Scale value)
    {
        MapScale = value;
        OnChangeMapScale?.Invoke();
    }

    public static void SetMode(Mode value)
    {
        Mode = value;
        OnChangeMode?.Invoke(value);
    }
}

public enum Scene
{
    MainMenu,
    Game
}

public enum Scale
{
    Transport,
    Planet,
    Solar
}

public enum Mode
{
    Map,
    Photo
}
