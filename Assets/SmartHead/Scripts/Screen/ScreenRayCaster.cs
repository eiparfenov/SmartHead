using System;
using Cysharp.Threading.Tasks;
using SmartHead.HandControl.Interfaces;
using SmartHead.Screen.Components;
using SmartHead.Screen.Settings;
using SmartHead.Utils.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace SmartHead.Screen
{
    public class ScreenRayCaster: IInitializable, IDisposable
    {
        private const bool ShowDebugSphere = false;
        private readonly ScreenInfoProvider _screenInfo;
        private readonly IScreenControl _screenControl;
        private readonly EventSystem _eventSystem;
        private readonly PointerSettings _settings;

        private Collider _activeCollider;
        private UiElement _activeUiElement;
        private int _clicks;
        private GameObject _debugSphere;

        [Inject]
        public ScreenRayCaster(ScreenInfoProvider screenInfo, IScreenControl screenControl, EventSystem eventSystem, PointerSettings settings)
        {
            _screenInfo = screenInfo;
            _screenControl = screenControl;
            _eventSystem = eventSystem;
            _settings = settings;
        }

        public void Initialize()
        {
            _screenControl.HandPosition.onValueChanged += HandPositionOnValueChanged;
            _screenControl.onClick += ScreenControlOnClick;
            if(ShowDebugSphere) _debugSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            if(ShowDebugSphere) _debugSphere.transform.localScale = Vector3.one * .1f;
        }

        private async void ScreenControlOnClick(Vector2 pos)
        {
            if(_activeUiElement == null) return;
            
            _activeUiElement.GetComponents<IPointerDownHandler>().Foreach(x => x.OnPointerDown(new PointerEventData(_eventSystem){button = PointerEventData.InputButton.Left}));
            _clicks += 1;
            await UniTask.Delay(_settings.PointerShowDelay);
            if(!_activeUiElement) return; 
            
            _activeUiElement.GetComponents<IPointerClickHandler>().Foreach(x => x.OnPointerClick(new PointerEventData(_eventSystem){button = PointerEventData.InputButton.Left}));
            _clicks -= 1;
            if(_clicks > 0 || !_activeUiElement) return;
            _activeUiElement.GetComponents<IPointerUpHandler>().Foreach(x => x.OnPointerUp(new PointerEventData(_eventSystem){button = PointerEventData.InputButton.Left}));
            
        }

        private void HandPositionOnValueChanged(Vector2 pos)
        {
            var collider = RayCast(pos);
            if(_activeCollider == collider) return;

            if (_activeUiElement != null)
            {
                if (_clicks > 0)
                {
                    _activeUiElement.GetComponents<IPointerUpHandler>().Foreach(x => x.OnPointerUp(new PointerEventData(_eventSystem){button = PointerEventData.InputButton.Left}));
                }

                _activeUiElement.GetComponents<IPointerExitHandler>().Foreach(x => x.OnPointerExit(default));
            }

            _activeCollider = collider;
            _activeUiElement = collider? collider.GetComponent<UiElement>(): null;

            if (_activeUiElement != null)
            {
                _activeUiElement.GetComponents<IPointerEnterHandler>().Foreach(x => x.OnPointerEnter(default));
            }
        }

        private Collider RayCast(Vector2 pos)
        {
            var raycastOrigin = _screenInfo.Position +
                                Vector3.up * _screenInfo.Size.y * _screenInfo.Scale.y * pos. y + 
                                _screenInfo.Right * _screenInfo.Size.x * _screenInfo.Scale.x * pos.x + 
                                _screenInfo.Forward * .01f;
            
            if(ShowDebugSphere) _debugSphere.transform.position = raycastOrigin;

            
            var collided = Physics.Raycast(raycastOrigin, -_screenInfo.Forward, out var raycastHit,.1f);
            return raycastHit.collider;
        }

        public void Dispose()
        {
            _screenControl.HandPosition.onValueChanged -= HandPositionOnValueChanged;
            _screenControl.onClick -= ScreenControlOnClick;
        }
    }
}