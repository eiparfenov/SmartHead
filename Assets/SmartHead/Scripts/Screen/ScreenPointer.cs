using System;
using Cysharp.Threading.Tasks;
using SmartHead.HandControl.Interfaces;
using SmartHead.Screen.Components;
using SmartHead.Screen.Settings;
using UnityEngine;
using Zenject;

namespace SmartHead.Screen
{
    public class ScreenPointer: IInitializable, IDisposable
    {
        private readonly ScreenInfoProvider _screenInfo;
        private readonly IScreenControl _screenControl;
        private readonly Pointer _pointer;
        private readonly PointerSettings _settings;

        private int _pointerClicked;

        public ScreenPointer(ScreenInfoProvider screenInfo, IScreenControl screenControl, Pointer pointer, PointerSettings settings)
        {
            _screenInfo = screenInfo;
            _screenControl = screenControl;
            _pointer = pointer;
            _settings = settings;
        }


        public void Initialize()
        {
            _screenControl.HandPosition.onValueChanged += HandPositionOnValueChanged;
            _screenControl.onClick += ScreenControlOnClick;
            _screenControl.onStart += ScreenControlOnStart;
            _screenControl.onRestart += ScreenControlOnRestart;
        }

        private void ScreenControlOnRestart()
        {
            _pointer.SetActive(false);
        }

        private void ScreenControlOnStart()
        {
            _pointer.SetActive(true);
        }

        private async void ScreenControlOnClick(Vector2 pos)
        {
            _pointer.ShowClick(true);
            _pointerClicked += 1;
            await UniTask.Delay(_settings.PointerShowDelay);
            _pointerClicked -= 1;
            if (_pointer)
            {
                _pointer.ShowClick(_pointerClicked > 0);
            }
        }

        private void HandPositionOnValueChanged(Vector2 pos)
        {
            _pointer.Position = _screenInfo.Size * pos;
        }
        

        public void Dispose()
        {
            _screenControl.HandPosition.onValueChanged += HandPositionOnValueChanged;
            _screenControl.onClick += ScreenControlOnClick;
        }
    }
}