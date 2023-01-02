using UnityEngine;
using Zenject;

public class LevelInstaller : MonoInstaller
{

    [SerializeField] private Ball _ball;
    [SerializeField] private CameraMover _camera;
    [SerializeField] private PlatformGenerator _platformGenerator;
    [SerializeField] private Transform _ballStartPoint;
    [SerializeField] private Transform _cameraMinHeight;

    public override void InstallBindings()
    {
        Container
            .Bind<Ball>()
            .FromInstance(_ball)
            .AsSingle();

        Container
            .Bind<CameraMover>()
            .FromInstance(_camera)
            .AsSingle();

        Container
            .Bind<PlatformGenerator>()
            .FromInstance(_platformGenerator)
            .AsSingle();

        Container
            .Bind<Transform>()
            .WithId(DIMarkers.BALL_START_POINT)
            .FromInstance(_ballStartPoint)
            .AsSingle();

        Container
            .Bind<float>()
            .WithId(DIMarkers.CAMERA_MIN_HEIGHT)
            .FromInstance(_cameraMinHeight.position.y)
            .AsSingle();
    }
}