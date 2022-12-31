using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

public class SerializeProvider
{
    public T Load<T>(string path)
    {
        var isExist = File.Exists(path);
        if (isExist == false)
            throw new FileNotFoundException(path);

        var dataJson = File.ReadAllText(path);
        if (string.IsNullOrEmpty(dataJson) == true)
            throw new FormatException();

        if (Regex.IsMatch(dataJson.Trim(), @"{\s*}") == true)
            throw new FormatException();

        return JsonUtility.FromJson<T>(dataJson);
    }

    public void Save(object data, string path)
    {
        var jsonData = JsonUtility.ToJson(data);
        File.WriteAllText(path, jsonData);
    }
}
