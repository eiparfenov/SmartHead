using System;
using TMPro;
using UnityEngine.UI;
using Zenject;

namespace SmartHead.Quest.Displays
{
    public class OptionDisplay: IDisposable
    {
        private readonly QuestNode.Option _model;
        private readonly TextMeshProUGUI _text;
        private readonly Button _button;

        public event Action<QuestNode.Option> onOptionSelected; 

        [Inject]
        public OptionDisplay(QuestNode.Option model, TextMeshProUGUI text, Button button)
        {
            _model = model;
            _text = text;
            _button = button;
            Initialize();
        }

        public void Initialize()
        {
            _button.onClick.AddListener(ButtonOnClicked);
            _text.text = _model.OptionText;
        }

        private void ButtonOnClicked()
        {
            onOptionSelected?.Invoke(_model);
        }


        public void Dispose()
        {
            _button.onClick.RemoveListener(ButtonOnClicked);
            UnityEngine.Object.Destroy(_button.gameObject);
        }
    }
}