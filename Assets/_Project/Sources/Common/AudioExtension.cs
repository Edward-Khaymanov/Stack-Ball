using UnityEngine;
using UnityEngine.Audio;

public static class AudioExtension
{
    public static void SetVolume(this AudioMixer mixer, string volumeParameterName, float value)
    {
        mixer.SetFloat(volumeParameterName, Mathf.Lerp(-80.0f, 0.0f, Mathf.Clamp01(value)));
    }

    public static float GetVolume(this AudioMixer mixer, string volumeParameterName)
    {
        if (mixer.GetFloat(volumeParameterName, out float volume))
            return Mathf.InverseLerp(-80.0f, 0.0f, volume);

        return 0f;
    }
}
