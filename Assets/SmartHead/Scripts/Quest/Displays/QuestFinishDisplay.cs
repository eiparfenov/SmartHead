using SmartHead.Quest.Interfaces;
using UnityEngine.UI;
using TMPro;

namespace SmartHead.Quest.Displays
{
    public class QuestFinishDisplay
    {
        private readonly TextMeshProUGUI _text;
        private readonly Image _image;

        public QuestFinishDisplay(TextMeshProUGUI text, Image image)
        {
            _text = text;
            _image = image;
        }

        public void Fill(IQuestionModel result)
        {
            _text.text = result.NodeText;
            if (result.NodeImage != null)
            {
                _image.sprite = result.NodeImage;
                _image.enabled = true;
            }
        }

        public void UnFill()
        {
            _text.text = "";
            _image.enabled = false;
        }
    }
}