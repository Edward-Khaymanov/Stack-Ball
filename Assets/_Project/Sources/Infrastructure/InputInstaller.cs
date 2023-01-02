using UnityEngine;
using Zenject;

public class InputInstaller : MonoInstaller
{
    [SerializeField] private Input _input;

    public override void InstallBindings()
    {
        Container
            .Bind<Input>()
            .FromInstance(_input)
            .AsSingle();
    }
}