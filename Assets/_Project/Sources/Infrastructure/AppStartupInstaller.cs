using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Zenject;
using System;
using UnityEngine.AddressableAssets;
using System.Threading.Tasks;
using UnityEditor;

public class AppStartupInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        var ballSkinProvider = new BallSkinProvider();

        Container
            .Bind<BallSkinProvider>()
            .FromInstance(ballSkinProvider)
            .AsSingle();
    }
}