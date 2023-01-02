using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

public abstract class AssetLoader
{
    protected AsyncOperationHandle<IList<IResourceLocation>> GetLocationsByLabelInternal<T>(AssetLabelReference reference)
    {
        var handle = Addressables.LoadResourceLocationsAsync(reference, typeof(T));
        handle.WaitForCompletion();
        return handle;
    }

    protected AsyncOperationHandle<T> LoadInternal<T>(string assetKey)
    {
        var handle = Addressables.LoadAssetAsync<T>(assetKey);
        handle.WaitForCompletion();
        return handle;
    }

    protected void ReleaseInternal(AsyncOperationHandle handle)
    {
        if (handle.IsValid())
            Addressables.Release(handle);
    }
}