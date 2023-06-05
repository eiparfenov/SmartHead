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
        private readonly IQuestProviderEvent _questProviderEvent;
        private readonly IQuestProviderAction _questProviderAction;

        public QuestManager(QuestNodeDisplay questNodeDisplay, IScreenControl screenControl, QuestFinishDisplay questFinishDisplay, QuestStartDisplay questStartDisplay, IQuestProviderEvent questProviderEvent, IQuestProviderAction questProviderAction)
        {
            _questNodeDisplay = questNodeDisplay;
            _screenControl = screenControl;
            _questFinishDisplay = questFinishDisplay;
            _questStartDisplay = questStartDisplay;
            _questProviderEvent = questProviderEvent;
            _questProviderAction = questProviderAction;
        }

        public void Initialize()
        {
            _questNodeDisplay.onOptionSelected += QuestNodeDisplayOnOptionSelected;
            _screenControl.onStart += ScreenControlOnStart;
            _screenControl.onRestart += ScreenControlOnRestart;
            _questStartDisplay.onQuestStarted += QuestStartDisplayOnQuestStarted;
            _questProviderAction.onQuestionSet += QuestProviderActionOnQuestionSet;
        }

        private void QuestProviderActionOnQuestionSet(IQuestionModel nextQuestion)
        {
            _questNodeDisplay.UnFill();
            
            if (nextQuestion.NodeType == NodeTypes.Options)
            {
                _questNodeDisplay.Fill(nextQuestion);
            }
            else
            {
                _questFinishDisplay.Fill(nextQuestion);
            }
        }

        private void QuestStartDisplayOnQuestStarted()
        {
            _questStartDisplay.SetActive(false);
            _questProviderEvent.StartOnButtonPressed();
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
            _questProviderEvent.OptionOnSelected(selectedOption);
        }

        public void Dispose()
        {
            _questNodeDisplay.onOptionSelected -= QuestNodeDisplayOnOptionSelected;
            _screenControl.onStart -= ScreenControlOnStart;
            _screenControl.onRestart -= ScreenControlOnRestart;
            _questStartDisplay.onQuestStarted -= QuestStartDisplayOnQuestStarted;
            _questProviderAction.onQuestionSet -= QuestProviderActionOnQuestionSet;
        }
    }
}