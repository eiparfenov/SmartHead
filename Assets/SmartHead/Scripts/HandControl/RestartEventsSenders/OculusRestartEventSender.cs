using System;
using SmartHead.HandControl.Interfaces;
using Zenject;

namespace SmartHead.HandControl.RestartEventsSenders
{
    public class OculusRestartEventSender: IRestartEventSender, IInitializable, IDisposable
    {
        public event Action onRestart;

        public void Initialize()
        {
            OVRManager.HMDMounted += OVRManagerOnHMDMounted;
        }

        private void OVRManagerOnHMDMounted()
        {
            onRestart?.Invoke();
        }

        public void Dispose()
        {
            OVRManager.HMDMounted += OVRManagerOnHMDMounted;
        }
    }
}