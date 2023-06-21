using System;
using System.Collections.Generic;
using SmartHead.Infrastructure.Quest.Factories;
using SmartHead.Quest.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SmartHead.Quest.Displays
{
    public class QuestNodeDisplay
    {
        private readonly TextMeshProUGUI _text;
        private readonly OptionDisplayFactory _optionDisplayFactory;
        private readonly GridLayoutGroup _grid;
        private readonly List<OptionDisplay> _createOptionDisplays;

        public event Action<IOptionModel> onOptionSelected;

        public QuestNodeDisplay(TextMeshProUGUI text, OptionDisplayFactory optionDisplayFactory, GridLayoutGroup grid)
        {
            _grid = grid;
            _text = text;
            _optionDisplayFactory = optionDisplayFactory;
            _createOptionDisplays = new List<OptionDisplay>();
        }

        public void Fill(IQuestionModel model)
        {
            var size = _grid.GetComponent<RectTransform>().rect.size;
            size.y /= model.OptionModels.Length;
            _grid.cellSize = size;
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