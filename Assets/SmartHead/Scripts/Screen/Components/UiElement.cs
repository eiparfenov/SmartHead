using System;
using UnityEngine;
using UnityEngine.UI;

namespace SmartHead.Screen.Components
{
    [RequireComponent(typeof(BoxCollider))]
    public class UiElement: MonoBehaviour
    {
        private BoxCollider _collider;
        public Collider Collider => _collider;
        private void OnEnable()
        {
            _collider = GetComponent<BoxCollider>();
            var grid = transform.parent.GetComponent<GridLayoutGroup>();
            if (grid)
            {
                _collider.size = grid.cellSize;
                return;
            }

            var rect = GetComponent<RectTransform>();
            if (rect)
            {
                _collider.size = rect.sizeDelta;
            }
        }
    }
}