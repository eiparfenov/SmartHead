using System;
using SmartHead.Utils.LiveData;
using UnityEngine;

namespace SmartHead.HandControl.Interfaces
{
    public interface IScreenControl
    {
        /// <summary>
        /// Invokes when the screen should be restarted
        /// </summary>
        event Action onRestart;

        /// <summary>
        /// Invokes before first input
        /// </summary>
        event Action onStart;
        
        event Action<Vector2> onClick;
        
        ILiveData<Vector2> HandPosition { get; }
    }
}