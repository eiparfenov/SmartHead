using Oculus.Interaction;
using SmartHead.HandControl;
using SmartHead.HandControl.RestartEventsSenders;
using SmartHead.HandControl.Temp;
using SmartHead.HandControl.VrHands;
using UnityEngine;
using Zenject;

namespace SmartHead.Infrastructure.HandControl
{
    public class OculusHandControlInstaller: MonoInstaller
    {
        [SerializeField] private SelectorUnityEventWrapper poseDetector;
        [SerializeField] private Transform vrHand;
        public override void InstallBindings()
        {
            Container.Bind<SelectorUnityEventWrapper>().FromInstance(poseDetector).AsCached();
            Container.Bind<Transform>().WithId("Hand").FromInstance(vrHand).AsCached();
            Container.BindInterfacesTo<OculusRestartEventSender>().AsCached();
            Container.BindInterfacesTo<OculusVrHand>().AsCached();
            Container.BindInterfacesTo<ScreenControl>().AsCached();
        }
    }
}