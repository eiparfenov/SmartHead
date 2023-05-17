using SmartHead.HandControl.Settings;
using UnityEngine;
using Zenject;

namespace SmartHead.Infrastructure.HandControl
{
    [CreateAssetMenu(menuName = "SmartHead/Settings/ScreenControl")]
    public class ScreenControlSettingsInstaller: ScriptableObjectInstaller
    {
        [SerializeField] private ScreenControlSettings screenControlSettings;

        public override void InstallBindings()
        {
            Container.Bind<ScreenControlSettings>().FromInstance(screenControlSettings).AsCached();
        }
    }
}