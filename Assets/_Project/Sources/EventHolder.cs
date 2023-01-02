using System;

public static class EventsHolder
{
    public static event Action LevelBuilding;
    public static event Action LevelPassed;
    public static event Action LevelStarted;
    public static event Action Losed;
    public static event Action PlatformDestroyed;
    public static event Action SkinUpdated;


    public static void BuildLevel() => LevelBuilding?.Invoke();
    public static void DestroyPlatform() => PlatformDestroyed?.Invoke();
    public static void Lose() => Losed?.Invoke();
    public static void PassLevel() => LevelPassed?.Invoke();
    public static void StartLevel() => LevelStarted?.Invoke();
    public static void UpdateBallSkin() => SkinUpdated?.Invoke();
}
