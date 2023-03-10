using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AppStartup : MonoBehaviour
{
    [SerializeField] private AssetReferenceT<BallSkin> _defaultSkin;
    [SerializeField] private AssetReference _gameScene;
    [SerializeField] private AssetLabelReference _levelColorPaletteLabel;
    [SerializeField] private AssetLabelReference _platformTemplatesLabel;

    private void Start()
    {
        var levelProvider = new LevelProvider(_platformTemplatesLabel, _levelColorPaletteLabel);
        Application.targetFrameRate = 60;
        LevelProvider.InitSingleton(levelProvider);
        Vibration.Init();

        try
        {
            var player = Player.Load();
            player.LoadProperties();
            Player.InitSingleton(player);
        }
        catch (Exception)
        {
            var player = new Player(_defaultSkin.AssetGUID);
            player.LoadProperties();
            Player.InitSingleton(player);
            player.Save();
        }

        try
        {
            var level = Level.Load();
            level.LoadProperties();
            Level.InitSingleton(level);
        }
        catch (Exception)
        {
            var level = new Level(1, levelProvider.GetRandomColorPaletteKey(), levelProvider.GetRandomPlatformKey());
            level.LoadProperties();
            Level.InitSingleton(level);
            level.Save();
        }

        try
        {
            var settings = Settings.Load();
            Settings.InitSingleton(settings);
        }
        catch (Exception)
        {
            var settings = new Settings();
            Settings.InitSingleton(settings);
            settings.Save();
        }

        _gameScene.LoadSceneAsync();
    }
}
