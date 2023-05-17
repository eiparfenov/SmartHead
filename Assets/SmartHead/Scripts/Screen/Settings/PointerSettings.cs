using System;
using UnityEngine;

namespace SmartHead.Screen.Settings
{
    [Serializable]
    public class PointerSettings
    {
        [field: SerializeField] public int PointerShowDelay { get; private set; }
    }
}