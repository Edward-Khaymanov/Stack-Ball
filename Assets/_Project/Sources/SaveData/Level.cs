using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;

[Serializable]
public class Level
{
    [SerializeField] private int _number = 1;
    [SerializeField] private string _platformReferenceKey;
    [SerializeField] private string _colorPaletteReferenceKey;

    private static Level _instance;

    private Platform _platform;
    private LevelColorPalette _colorPalette;

    public Level(int number, string platformReferenceKey, string colorPaletteReferenceKey)
    {
        Validate(number, platformReferenceKey, colorPaletteReferenceKey);

        _number = number;
        _platformReferenceKey = platformReferenceKey;
        _colorPaletteReferenceKey = colorPaletteReferenceKey;
    }

    public static Level Current => _instance;
    private static string SavePath => Path.Combine(Application.persistentDataPath, "level.json");

    public int Number => _number;
    public Platform Platform => _platform;
    public LevelColorPalette ColorPalette => _colorPalette;

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

    private void Validate(int number, string platformReferenceKey, string colorPaletteReferenceKey)
    {
        if (number < 1)
            throw new ArgumentOutOfRangeException(nameof(number));

        if (string.IsNullOrEmpty(platformReferenceKey))
            throw new ArgumentException(nameof(platformReferenceKey));

        if (string.IsNullOrEmpty(colorPaletteReferenceKey))
            throw new ArgumentException(nameof(colorPaletteReferenceKey));
    }
}
