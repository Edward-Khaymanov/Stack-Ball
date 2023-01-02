using System;
using System.IO;
using UnityEngine;

[Serializable]
public class Level
{
    [SerializeField] private int _number = 1;
    [SerializeField] private string _colorPaletteReferenceKey;
    [SerializeField] private string _platformReferenceKey;

    private static Level _instance;

    private LevelColorPalette _colorPalette;
    private Platform _platform;

    public Level(int number, string colorPaletteReferenceKey, string platformReferenceKey)
    {
        Validate(number, colorPaletteReferenceKey, platformReferenceKey);

        _number = number;
        _colorPaletteReferenceKey = colorPaletteReferenceKey;
        _platformReferenceKey = platformReferenceKey;
    }

    public static Level Current => _instance;
    private static string SavePath => Path.Combine(Application.persistentDataPath, "level.json");

    public int Number => _number;
    public LevelColorPalette ColorPalette => _colorPalette;
    public Platform Platform => _platform;

    public static void InitSingleton(Level instance)
    {
        _instance = instance;
    }

    public static Level Load()
    {
        var provider = new SerializeProvider();
        return provider.Load<Level>(SavePath);
    }

    public void Save()
    {
        var provider = new SerializeProvider();
        provider.Save(this, SavePath);

        _instance._number = _number;
        _instance._platformReferenceKey = _platformReferenceKey;
        _instance._colorPaletteReferenceKey = _colorPaletteReferenceKey;
        _instance._platform = _platform;
        _instance._colorPalette = _colorPalette;
    }

    public void LoadProperties()
    {
        _platform = LevelProvider.Instance.LoadPlatform(_platformReferenceKey);
        _colorPalette = LevelProvider.Instance.LoadPalette(_colorPaletteReferenceKey);
    }

    private void Validate(int number, string colorPaletteReferenceKey, string platformReferenceKey)
    {
        if (number < 1)
            throw new ArgumentOutOfRangeException(nameof(number));

        if (string.IsNullOrEmpty(colorPaletteReferenceKey))
            throw new ArgumentException(nameof(colorPaletteReferenceKey));

        if (string.IsNullOrEmpty(platformReferenceKey))
            throw new ArgumentException(nameof(platformReferenceKey));
    }
}
