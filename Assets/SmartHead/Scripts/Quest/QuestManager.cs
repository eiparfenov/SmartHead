using System;
using System.Threading;
using SmartHead.HandControl.Interfaces;
using SmartHead.Quest.Displays;
using SmartHead.Quest.Interfaces;
using Zenject;

namespace SmartHead.Quest
{
    public class QuestManager: IInitializable, IDisposable
    {
        private readonly QuestNodeDisplay _questNodeDisplay;
        private readonly IScreenControl _screenControl;
        private readonly QuestFinishDisplay _questFinishDisplay;
        private readonly QuestStartDisplay _questStartDisplay;
        private readonly IQuestProvider _questProvider;

        public QuestManager(QuestNodeDisplay questNodeDisplay, IScreenControl screenControl, QuestFinishDisplay questFinishDisplay, QuestStartDisplay questStartDisplay, IQuestProvider questProvider)
        {
            _questNodeDisplay = questNodeDisplay;
            _screenControl = screenControl;
            _questFinishDisplay = questFinishDisplay;
            _questStartDisplay = questStartDisplay;
            _questProvider = questProvider;
        }

        public void Initialize()
        {
            _questNodeDisplay.onOptionSelected += QuestNodeDisplayOnOptionSelected;
            _screenControl.onStart += ScreenControlOnStart;
            _screenControl.onRestart += ScreenControlOnRestart;
            _questStartDisplay.onQuestStarted += QuestStartDisplayOnQuestStarted;
        }

        private void QuestStartDisplayOnQuestStarted()
        {
            _questStartDisplay.SetActive(false);
            _questNodeDisplay.Fill(_questProvider.GetStartNode());
        }

        private void ScreenControlOnRestart()
        {
            _questStartDisplay.SetActive(false);
            _questFinishDisplay.UnFill();
            _questNodeDisplay.UnFill();
        }

        private void ScreenControlOnStart()
        {
            _questStartDisplay.SetActive(true);
        }

        private void QuestNodeDisplayOnOptionSelected(IOptionModel selectedOption)
        {
            _questNodeDisplay.UnFill();
            var nextQuestion = _questProvider.FromPlayerResponse(selectedOption);
            if (nextQuestion.NodeType == NodeTypes.Options)
            {
                _questNodeDisplay.Fill(nextQuestion);
            }
            else
            {
                _questFinishDisplay.Fill(nextQuestion);
            }
        }

        public void Dispose()
        {
            _questNodeDisplay.onOptionSelected -= QuestNodeDisplayOnOptionSelected;
        }
    }
}