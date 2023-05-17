using System;
using UnityEngine;

namespace SmartHead.Screen.Components
{
    public class ScreenInfoProvider: MonoBehaviour
    {
        public Vector3 Position => transform.position;
        public Vector3 Scale => transform.localScale;
        public Vector3 Forward => transform.forward;
        public Vector3 Right => transform.right;
        public Vector2 Size { get; private set; }

        private void Awake()
        {
            Size = GetComponent<RectTransform>().sizeDelta;
        }
    }
}