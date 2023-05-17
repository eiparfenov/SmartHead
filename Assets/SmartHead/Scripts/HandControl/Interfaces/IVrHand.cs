using System;
using SmartHead.Utils.LiveData;
using UnityEngine;

namespace SmartHead.HandControl.Interfaces
{
    public interface IVrHand
    {
        ILiveData<Vector3> Position { get; }
        
        event Action onClick;
    }
}