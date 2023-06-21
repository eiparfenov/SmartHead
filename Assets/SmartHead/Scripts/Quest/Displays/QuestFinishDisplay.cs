using SmartHead.Quest.Interfaces;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

namespace SmartHead.Quest.Displays
{
    public class QuestFinishDisplay
    {
        private readonly TextMeshProUGUI _text;
        private readonly Image _image;
        private readonly GameObject _screen;

        public QuestFinishDisplay(TextMeshProUGUI text, Image image, GameObject screen)
        {
            _screen = screen;
            _text = text;
            _image = image;
        }

        public void Fill(IQuestionModel result)
        {
            _screen.SetActive(true);
            _text.text = result.NodeText;
            if (result.NodeImage != null)
            {
                _image.sprite = result.NodeImage;
                _image.enabled = true;
            }
        }

        public void UnFill()
        {
            _screen.SetActive(false);
            _text.text = "";
            _image.enabled = false;
        }
    }
}