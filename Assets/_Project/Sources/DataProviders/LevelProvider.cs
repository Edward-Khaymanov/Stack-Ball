using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LevelProvider : AssetLoader
{
    private static LevelProvider _instance;

    private readonly IList<string> _colorPalettesLocations;
    private readonly IList<string> _platformsLocations;
    private AsyncOperationHandle _cachedColorPaletteHandle;
    private AsyncOperationHandle _cachedPlatfromHandle;


    public LevelProvider(AssetLabelReference platformsLabel, AssetLabelReference colorPalettesLabel)
    {
        var platformsLocationsHandle = GetLocationsByLabelInternal<GameObject>(platformsLabel);
        var colorPalettesLocationsHandle = GetLocationsByLabelInternal<LevelColorPalette>(colorPalettesLabel);

        _platformsLocations = platformsLocationsHandle.Result?.Select(x => x.PrimaryKey).ToList();
        _colorPalettesLocations = colorPalettesLocationsHandle.Result?.Select(x => x.PrimaryKey).ToList();

        ReleaseInternal(platformsLocationsHandle);
        ReleaseInternal(colorPalettesLocationsHandle);

        _instance = this;
    }

    public static LevelProvider Instance => _instance;

    public static void InitSingleton(LevelProvider instance)
    {
        _instance = instance;
    }

    public string GetRandomPlatformKey()
    {
        var platformTemplateIndex = UnityEngine.Random.Range(0, _platformsLocations.Count);
        var platfromKey = _platformsLocations[platformTemplateIndex];
        return platfromKey;
    }

    public string GetRandomColorPaletteKey()
    {
        var colorPaletteIndex = UnityEngine.Random.Range(0, _colorPalettesLocations.Count);
        var colorKey = _colorPalettesLocations[colorPaletteIndex];
        return colorKey;
    }

    public Platform LoadPlatform(string assetKey)
    {
        var platformAssetHandle = LoadInternal<GameObject>(assetKey);
        var platformObject = platformAssetHandle.Result;

        if (platformObject.TryGetComponent(out Platform platform) == false)
            throw new MissingComponentException(nameof(Platform));

        ReleaseInternal(_cachedPlatfromHandle);
        _cachedPlatfromHandle = platformAssetHandle;

        return platform;
    }

    public LevelColorPalette LoadPalette(string assetKey)
    {
        var paletteAssetHandle = LoadInternal<ScriptableObject>(assetKey);
        var palette = paletteAssetHandle.Result as LevelColorPalette;

        if (palette == null)
            throw new InvalidCastException();

        ReleaseInternal(_cachedColorPaletteHandle);
        _cachedColorPaletteHandle = paletteAssetHandle;

        return palette;
    }
}
