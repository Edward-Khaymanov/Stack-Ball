using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AddressableAssets;

[Serializable]
public class Player
{
    [SerializeField] private string _choicedSkinKey;
    [SerializeField] private Score _score;

    private static Player _instance;
    private BallSkin _choicedSkin;

    public Player(string skinKey)
    {
        _choicedSkinKey = skinKey;
        _score = new Score();
    }

    public static Player Current => _instance;
    private static string SavePath => Path.Combine(Application.persistentDataPath, "player.json");

    public BallSkin ChoicedSkin => _choicedSkin;
    public Score Score => _score;
    public string ChoicedSkinKey => _choicedSkinKey;

    public static void InitSingleton(Player instance)
    {
        _instance = instance;
    }

    public static Player Load()
    {
        var serializeProvider = new SerializeProvider();
        return serializeProvider.Load<Player>(SavePath);
    }

    public void Save()
    {
        var provider = new SerializeProvider();
        provider.Save(this, SavePath);

        _instance._choicedSkin = _choicedSkin;
        _instance._choicedSkinKey = _choicedSkinKey;
        _instance._score = _score;
    }

    public void ChoiceSkin(BallSkin skin, string skinKey)
    {
        if (skin == null)
            throw new ArgumentNullException(nameof(skin));

        if (string.IsNullOrEmpty(skinKey))
            throw new ArgumentException(nameof(skinKey));

        _choicedSkinKey = skinKey;
        _choicedSkin = skin;
        
        Save();
    }

    public void LoadProperties()
    {
        var skinProvider = new BallSkinProvider();
        _choicedSkin = skinProvider.Get(_choicedSkinKey);
        skinProvider.Release();
    }
}
