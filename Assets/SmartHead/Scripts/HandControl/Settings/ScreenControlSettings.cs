using System;
using UnityEngine;

namespace SmartHead.HandControl.Settings
{
    [Serializable]
    public class ScreenControlSettings
    {
        [field: SerializeField] public Vector3 Normal { get; private set; }
        [field: SerializeField] public Vector2 ArmMoveRange { get; private set; }
    }
}