using UnityEngine;
using UnityEngine.UI;

namespace SmartHead.Screen.Components
{
    public class Pointer: MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private Sprite idle;
        [SerializeField] private Sprite click;

        public Vector3 Position
        {
            get => transform.localPosition;
            set => transform.localPosition = value;
        }

        public void ShowClick(bool clicked)
        {
            image.sprite = clicked ? click : idle;
        }

        public void SetActive(bool active)
        {
            image.enabled = active;
        }

        public void Start()
        {
            image.sprite = idle;
        }
    }
}