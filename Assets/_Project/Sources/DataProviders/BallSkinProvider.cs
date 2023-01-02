using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

public class BallSkinProvider : AssetLoader
{
    private AsyncOperationHandle _cachedHandle;

    public IList<IResourceLocation> GetLocationsByLabel(AssetLabelReference ballsLabel)
    {
        return GetLocationsByLabelInternal<BallSkin>(ballsLabel).Result;
    }

    public BallSkin Get(string assetKey)
    {
        var skinAssetHandle = LoadInternal<ScriptableObject>(assetKey);
        if (skinAssetHandle.Result == null)
            throw new FileNotFoundException(nameof(assetKey));

        var skin = skinAssetHandle.Result as BallSkin;
        if (skin == null)
            throw new InvalidCastException();

        ReleaseInternal(_cachedHandle);
        _cachedHandle = skinAssetHandle;
        return skin;
    }

    public Dictionary<string, BallSkin> GetKeySkins(IList<IResourceLocation> skinsLocation)
    {
        var result = new Dictionary<string, BallSkin>();

        foreach (var location in skinsLocation)
        {
            result.Add(location.PrimaryKey, Get(location.PrimaryKey));
        }

        return result;
    }

    public void Release()
    {
        ReleaseInternal(_cachedHandle);
    }
}
