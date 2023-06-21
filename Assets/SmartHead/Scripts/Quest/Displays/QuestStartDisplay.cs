using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SmartHead.Quest.Displays
{
    public class QuestStartDisplay: IInitializable, IDisposable
    {
        private readonly Button _button;
        private readonly GameObject _screen;
        public event Action onQuestStarted;

        public QuestStartDisplay(Button button, GameObject screen)
        {
            _button = button;
            _screen = screen;
        }

        public void SetActive(bool active)
        {
            _screen.SetActive(active);
        }

        public void Initialize()
        {
            _button.onClick.AddListener(ButtonOnQuestStarted);
        }

        public void Dispose()
        {
            _button.onClick.RemoveListener(ButtonOnQuestStarted);
        }

        private void ButtonOnQuestStarted()
        {
            onQuestStarted?.Invoke();
        }
    }
}