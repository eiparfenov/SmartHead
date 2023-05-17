using SmartHead.Screen;
using SmartHead.Screen.Components;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace SmartHead.Infrastructure.Screen
{
    public class ScreenMonoInstaller: MonoInstaller
    {
        [SerializeField] private Pointer pointer;
        [SerializeField] private ScreenInfoProvider screenInfo;
        [SerializeField] private EventSystem eventSystem;
        
        public override void InstallBindings()
        {
            Container.Bind<Pointer>().FromInstance(pointer).AsCached();
            Container.Bind<ScreenInfoProvider>().FromInstance(screenInfo).AsCached();
            Container.Bind<EventSystem>().FromInstance(eventSystem).AsCached();

            Container.BindInterfacesTo<ScreenPointer>().AsCached().NonLazy();
            Container.BindInterfacesTo<ScreenRayCaster>().AsCached().NonLazy();
        }
    }
}