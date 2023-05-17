using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using SmartHead.HandControl.Interfaces;
using SmartHead.HandControl.Settings;
using SmartHead.Utils;
using SmartHead.Utils.LiveData;
using UnityEngine;
using Zenject;

namespace SmartHead.HandControl
{
    public class ScreenControl: IScreenControl, IInitializable, IDisposable
    {
        private readonly IRestartEventSender _restartEventSender;
        private readonly IVrHand _vrHand;
        private readonly ScreenControlSettings _settings;
        private readonly MutableLiveData<Vector2> _handPosition = new();
        private readonly CancellationTokenSource _cancellationTokenSource;

        private Vector3 _startPosition;


        public ScreenControl(IRestartEventSender restartEventSender, IVrHand vrHand, ScreenControlSettings settings)
        {
            _restartEventSender = restartEventSender;
            _vrHand = vrHand;
            _settings = settings;
            _cancellationTokenSource = new CancellationTokenSource();
        }
        
        public event Action onRestart;
        public event Action onStart;
        public event Action<Vector2> onClick;
        public ILiveData<Vector2> HandPosition => _handPosition;
        
        public void Initialize()
        {
            MainLoop(_cancellationTokenSource.Token);
        }
        
        public async void MainLoop(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await WaitForClick(cancellationToken);
                onStart?.Invoke();
                _startPosition = _vrHand.Position.Value;
                _vrHand.Position.onValueChanged += VrHandPositionOnValueChanged;
                _vrHand.onClick += VrHandOnClick;
                await WaitForRestart(cancellationToken);
                onRestart?.Invoke();
                _vrHand.Position.onValueChanged -= VrHandPositionOnValueChanged;
                _vrHand.onClick -= VrHandOnClick;
            }
        }

        private void VrHandOnClick()
        {
            onClick?.Invoke(HandPosition.Value);
        }

        private void VrHandPositionOnValueChanged(Vector3 position)
        {
            var delta = position - _startPosition;
            delta = Vector3.ProjectOnPlane(delta, _settings.Normal);
            var positionOnScreen = new Vector2
            {
                x = Mathf.InverseLerp(-_settings.ArmMoveRange.x, _settings.ArmMoveRange.x,
                    Vector3.Dot(delta, Vector3.Cross(Vector3.up, _settings.Normal))),
                y = Mathf.InverseLerp(-_settings.ArmMoveRange.y, _settings.ArmMoveRange.y, delta.y),
            };
            positionOnScreen = positionOnScreen * 2 - Vector2.one;
            _handPosition.Value = positionOnScreen;
        }

        public async UniTask WaitForClick(CancellationToken cancellationToken)
        {
            var clicked = false;
            var callback = new Action(() => clicked = true);
            _vrHand.onClick += callback;
            await UniTask.WaitUntil(() => clicked, PlayerLoopTiming.Update, cancellationToken);
            _vrHand.onClick -= callback;
        }
        public async UniTask WaitForRestart(CancellationToken cancellationToken)
        {
            var clicked = false;
            var callback = new Action(() => clicked = true);
            _restartEventSender.onRestart += callback;
            await UniTask.WaitUntil(() => clicked, PlayerLoopTiming.Update, cancellationToken);
            _restartEventSender.onRestart -= callback;
        }

        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
        }
        
        
    }
}