using SmartHead.HandControl.Interfaces;
using UnityEngine;

namespace SmartHead.HandControl.Temp
{
    public class ScreenControlDebugger
    {
        private readonly IScreenControl _screenControl;

        public ScreenControlDebugger(IScreenControl screenControl)
        {
            _screenControl = screenControl;

            _screenControl.onClick += pos => Debug.Log($"Clicked {pos}");
            _screenControl.HandPosition.onValueChanged += pos => Debug.Log($"Moved to {pos}");
            _screenControl.onRestart += () => Debug.Log($"Restarted");
            _screenControl.onStart += () => Debug.Log($"Started");
        }
    }
}