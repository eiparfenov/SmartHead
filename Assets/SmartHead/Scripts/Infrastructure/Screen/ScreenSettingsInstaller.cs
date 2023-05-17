using SmartHead.HandControl.Settings;
using SmartHead.Screen.Settings;
using UnityEngine;
using Zenject;

namespace SmartHead.Infrastructure.Screen
{
    [CreateAssetMenu(menuName = "SmartHead/Settings/Screen")]
    public class ScreenSettingsInstaller: ScriptableObjectInstaller
    {
        [SerializeField] private PointerSettings pointerSettings;

        public override void InstallBindings()
        {
            Container.Bind<PointerSettings>().FromInstance(pointerSettings).AsCached();
        }
    }
}