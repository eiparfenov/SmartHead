using System;
using Oculus.Interaction;
using SmartHead.HandControl.Interfaces;
using SmartHead.Utils.LiveData;
using UnityEngine;
using Zenject;

namespace SmartHead.HandControl.VrHands
{
    public class OculusVrHand: IVrHand, ITickable, IInitializable, IDisposable
    {
        private readonly SelectorUnityEventWrapper _poseDetector;
        private readonly Transform _handTransform;
        private readonly MutableLiveData<Vector3> _position = new();

        public ILiveData<Vector3> Position => _position;
        public event Action onClick;

        [Inject]
        public OculusVrHand(SelectorUnityEventWrapper poseDetector, [Inject(Id = "Hand")] Transform handTransform)
        {
            _poseDetector = poseDetector;
            _handTransform = handTransform;
        }

        public void Initialize()
        {
            _poseDetector.WhenSelected.AddListener(PoseOnDetected);
        }

        public void Tick()
        {
            _position.Value = _handTransform.position;
        }

        public void Dispose()
        {
            _poseDetector.WhenSelected.RemoveListener(PoseOnDetected);
        }

        private void PoseOnDetected()
        {
            onClick?.Invoke();
        }
    }
}