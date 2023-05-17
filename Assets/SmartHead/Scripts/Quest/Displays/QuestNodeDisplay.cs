using System;
using System.Collections.Generic;
using SmartHead.Infrastructure.Quest.Factories;
using SmartHead.Quest.Interfaces;
using TMPro;

namespace SmartHead.Quest.Displays
{
    public class QuestNodeDisplay
    {
        private readonly TextMeshProUGUI _text;
        private readonly OptionDisplayFactory _optionDisplayFactory;
        
        private readonly List<OptionDisplay> _createOptionDisplays;

        public event Action<IOptionModel> onOptionSelected;

        public QuestNodeDisplay(TextMeshProUGUI text, OptionDisplayFactory optionDisplayFactory)
        {
            _text = text;
            _optionDisplayFactory = optionDisplayFactory;
            _createOptionDisplays = new List<OptionDisplay>();
        }

        public void Fill(IQuestionModel model)
        {
            _text.text = model.NodeText;
            foreach (var modelOption in model.OptionModels)
            {
                var optionDisplay = _optionDisplayFactory.Create(modelOption);
                _createOptionDisplays.Add(optionDisplay);
                optionDisplay.onOptionSelected += OptionDisplayOnOptionSelected;
            }
        }

        private void OptionDisplayOnOptionSelected(IOptionModel obj)
        {
            onOptionSelected?.Invoke(obj);
        }

        public void UnFill()
        {
            _text.text = string.Empty;

            foreach (var optionDisplay in _createOptionDisplays)
            {
                optionDisplay.Dispose();
                optionDisplay.onOptionSelected -= OptionDisplayOnOptionSelected;
            }
            _createOptionDisplays.Clear();
        }
    }
}