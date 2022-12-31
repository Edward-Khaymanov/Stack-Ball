using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
    [SerializeField] private BallInvincibilityIndicator _ballInvincibilityIndicator;
    [SerializeField] private LevelProgressBar _levelProgressBar;
    [SerializeField] private LoseScreen _loseScreen;
    [SerializeField] private SkyboxView _skybox;
    [SerializeField] private TextView _scoreView;
    [SerializeField] private WinScreen _winScreen;

    public override void InstallBindings()
    {
        Container
            .Bind<BallInvincibilityIndicator>()
            .FromInstance(_ballInvincibilityIndicator)
            .AsSingle();

        Container
            .Bind<LevelProgressBar>()
            .FromInstance(_levelProgressBar)
            .AsSingle();

        Container
            .Bind<LoseScreen>()
            .FromInstance(_loseScreen)
            .AsSingle();

        Container
            .Bind<SkyboxView>()
            .FromInstance(_skybox)
            .AsSingle();

        Container
            .Bind<TextView>()
            .WithId(DIMarkers.SCORE_VIEW)
            .FromInstance(_scoreView)
            .AsSingle();

        Container
            .Bind<WinScreen>()
            .FromInstance(_winScreen)
            .AsSingle();
    }
}