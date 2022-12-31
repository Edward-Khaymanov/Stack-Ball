using UnityEngine;
using Zenject;

public class InputInstaller : MonoInstaller
{
    [SerializeField] private BallInput _input;

    public override void InstallBindings()
    {
        Container
            .Bind<BallInput>()
            .FromInstance(_input)
            .AsSingle();
    }
}