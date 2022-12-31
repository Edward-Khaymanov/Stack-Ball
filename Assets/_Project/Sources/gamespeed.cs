using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamespeed : MonoBehaviour
{
    [Range(0,1)]public float speed;
    private void Start()
    {
        Time.timeScale = speed;
    }

    private void OnValidate()
    {
        Time.timeScale = speed;

    }
}
