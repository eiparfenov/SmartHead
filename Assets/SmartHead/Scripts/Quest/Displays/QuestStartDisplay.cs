using System;
using UnityEngine.UI;
using Zenject;

namespace SmartHead.Quest.Displays
{
    public class QuestStartDisplay: IInitializable, IDisposable
    {
        private readonly Button _button;
        public event Action onQuestStarted;

        public QuestStartDisplay(Button button)
        {
            _button = button;
        }

        public void SetActive(bool active)
        {
            _button.gameObject.SetActive(active);
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