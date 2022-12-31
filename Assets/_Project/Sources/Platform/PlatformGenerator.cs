using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlatformGenerator : MonoBehaviour
{
    [SerializeField] private float _offsetHeight = 0.4f;
    [SerializeField] private float _offestAngle = 2;
    [SerializeField] private int _count = 140;
    [SerializeField] private int _minSamePlatformsCount = 5;
    [SerializeField] private int _maxSamePlatformsCount = 40;

    private const int PLATFORM_SAFE_PARTS = 2;
    private Transform _platformsContainer;
    private Vector3 _startPosition;

    public int PlatformsCount => _count;

    private void Awake()
    {
        _startPosition = transform.position;
    }

    private void OnValidate()
    {
        if (_maxSamePlatformsCount <= _minSamePlatformsCount)
            _maxSamePlatformsCount = _minSamePlatformsCount + 1;
    }

    public void Build(Platform template, Gradient platformsColors)
    {
        SetPlatformsContainer();
        var platforms = GeneratePlatforms(template, _count, _offestAngle);
        ConfigurePlatforms(platforms, platformsColors, template.PartsCount);
    }

    private Queue<Platform> GeneratePlatforms(Platform template, int count, float rotationAngle)
    {
        var platforms = new Queue<Platform>();
        var rotation = Quaternion.identity;
        var offsetRotation = Quaternion.Euler(0, rotationAngle, 0);
        var position = _startPosition;

        for (int i = 0; i < count; i++)
        {
            position.y += _offsetHeight;
            rotation *= offsetRotation;

            var platform = Instantiate(template, position, rotation, _platformsContainer);
            platforms.Enqueue(platform);
        }

        return platforms;
    }

    private void ConfigurePlatforms(Queue<Platform> platforms, Gradient platformsColors, int templatePartsCount)
    {
        var gradientTime = 0f;
        var platformsCount = platforms.Count;

        for (float i = 1; i <= platformsCount;)
        {
            var samePlatformsCount = Random.Range(_minSamePlatformsCount, _maxSamePlatformsCount + 1);
            var samePlatformsDangerPartsCount = Random.Range(0, templatePartsCount - PLATFORM_SAFE_PARTS);

            for (int j = 0; j < samePlatformsCount && i <= platformsCount; j++, i++)
            {
                var platform = platforms.Dequeue();
                platform.Init(platformsColors.Evaluate(gradientTime), samePlatformsDangerPartsCount);
                gradientTime =  i / platformsCount;
            }
        }
    }

    private void SetPlatformsContainer()
    {
        if (_platformsContainer != null)
            Destroy(_platformsContainer.gameObject);

        var container = new GameObject("container");
        container.transform.parent = transform;
        _platformsContainer = container.transform;
    }
}
