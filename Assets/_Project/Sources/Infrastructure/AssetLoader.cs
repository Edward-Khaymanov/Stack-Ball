using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using System.Threading;
using System.Threading.Tasks;

public abstract class AssetLoader
{
    private void DebugHandle(AsyncOperationHandle handle, string message)
    {
        Debug.Log(handle.DebugName + message);
    }

    protected AsyncOperationHandle<IList<IResourceLocation>> GetLocationsByLabelInternalAsync<T>(AssetLabelReference reference)
    {
        var handle = Addressables.LoadResourceLocationsAsync(reference, typeof(T));
        //_cachedHandle = handle;
        handle.Completed += (handle) => DebugHandle(handle, " Completed");
        handle.Destroyed += (handle) => DebugHandle(handle, " Destroyed");
        return handle;
    }

    protected AsyncOperationHandle<IList<IResourceLocation>> GetLocationsByLabelInternal<T>(AssetLabelReference reference)
    {
        var handle = Addressables.LoadResourceLocationsAsync(reference, typeof(T));
        //_cachedHandle = handle;
        handle.Completed += (handle) => DebugHandle(handle, " Completed");
        handle.Destroyed += (handle) => DebugHandle(handle, " Destroyed");
        handle.WaitForCompletion();
        return handle;
    }

    protected AsyncOperationHandle<T> LoadInternalAsync<T>(string assetKey)
    {
        var handle = Addressables.LoadAssetAsync<T>(assetKey);
        //_cachedHandle = handle;
        handle.Completed += (handle) => DebugHandle(handle, " Completed");
        handle.Destroyed += (handle) => DebugHandle(handle, " Destroyed");
        return handle;
    }

    protected AsyncOperationHandle<T> LoadInternal<T>(string assetKey)
    {
        var handle = Addressables.LoadAssetAsync<T>(assetKey);
        //_cachedHandle = handle;
        handle.Completed += (handle) => DebugHandle(handle, " Completed");
        handle.Destroyed += (handle) => DebugHandle(handle, " Destroyed");
        handle.WaitForCompletion();
        return handle;
    }


    protected void ReleaseInternal(AsyncOperationHandle handle)
    {
        if (handle.IsValid())
            Addressables.Release(handle);
    }
}