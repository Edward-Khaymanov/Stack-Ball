using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public static class AnimationExtention
{
    public static IEnumerator Move(this Transform original, Vector3 targetPosition, float duration)
    {
        var expiredSeconds = 0f;
        var progress = 0f;
        var startPosition = original.transform.position;
        var difference = targetPosition - startPosition;

        while (progress < 1)
        {
            var newPosition = difference * progress;
            original.position = startPosition + newPosition;

            expiredSeconds += Time.deltaTime;
            progress = expiredSeconds / duration;

            yield return null;
        }
    }

    public static IEnumerator RotateExt(this Transform original, Vector3 targetRotation, float duration)
    {
        var expiredSeconds = 0f;
        var progress = 0f;
        var startRotation = original.rotation.eulerAngles;
        var difference = targetRotation - startRotation;

        while (progress < 1)
        {
            var newRotation = difference * progress;
            original.eulerAngles = startRotation + newRotation;

            expiredSeconds += Time.deltaTime;
            progress = expiredSeconds / duration;

            yield return null;
        }
    }

    public static IEnumerator StartCount(int from, int to, float duration, AnimationCurve pattern, Action<int> tickCallback)
    {
        var expiredSeconds = 0f;
        var progress = 0f;
        var difference = to - from;

        while (progress < 1)
        {
            var curvePosition = pattern.Evaluate(progress);
            var newValue = from + Mathf.RoundToInt(curvePosition * difference);
            tickCallback?.Invoke(newValue);

            expiredSeconds += Time.deltaTime;
            progress = expiredSeconds / duration;

            yield return null;
        }
    }
}

