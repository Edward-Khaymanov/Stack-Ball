using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class Settings
{
    [SerializeField] public bool AudioIsEnabled = true;
    [SerializeField] public bool VibrationIsEnabled = true;

    private static Settings _instance;

    public static Settings Current => _instance;
    private static string SavePath => Path.Combine(Application.persistentDataPath, "settings.json");

    public static void InitSingleton(Settings instance)
    {
        _instance = instance;
    }

    public static Settings Load()
    {
        var provider = new SerializeProvider();
        return provider.Load<Settings>(SavePath);
    }

    public void Save()
    {
        var serializer = new SerializeProvider();
        serializer.Save(this, SavePath);

        _instance.AudioIsEnabled = AudioIsEnabled;
        _instance.VibrationIsEnabled = VibrationIsEnabled;
    }
}
