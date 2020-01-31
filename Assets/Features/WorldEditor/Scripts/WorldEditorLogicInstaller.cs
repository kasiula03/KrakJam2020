using UnityEngine;
using Zenject;

public class WorldEditorLogicInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<WorldEditorLogic>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerAbilitiesLogic>().AsSingle();
    }
}