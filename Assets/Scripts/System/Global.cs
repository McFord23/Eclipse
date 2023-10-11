public static class Global
{
    public static Mode Mode = Mode.Map;
    
    public static bool IsPause;
    
    public static float MouseSens = 10;
    public static float ScrollSens = 2f;

    public static float TimeScale { get; private set; } = 1f;
    public delegate void ChangeTimeScale();
    public static event ChangeTimeScale OnChangeTimeScale;
    
    public static void SetTimeScale(float value)
    {
        TimeScale = value;
        OnChangeTimeScale?.Invoke();
    }
}

public enum Scene
{
    MainMenu,
    Game
}

public enum Mode
{
    Map,
    Photo
}
