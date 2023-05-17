using System;
using System.Threading;
using SmartHead.HandControl.Interfaces;
using SmartHead.Quest.Displays;
using Zenject;

namespace SmartHead.Quest
{
    public class QuestManager: IInitializable, IDisposable
    {
        private readonly QuestNode _startNode;
        private readonly QuestNodeDisplay _questNodeDisplay;
        private readonly IScreenControl _screenControl;
        private readonly QuestFinishDisplay _questFinishDisplay;
        private readonly QuestStartDisplay _questStartDisplay;

        public QuestManager(QuestNode startNode, QuestNodeDisplay questNodeDisplay, IScreenControl screenControl, QuestFinishDisplay questFinishDisplay, QuestStartDisplay questStartDisplay)
        {
            _startNode = startNode;
            _questNodeDisplay = questNodeDisplay;
            _screenControl = screenControl;
            _questFinishDisplay = questFinishDisplay;
            _questStartDisplay = questStartDisplay;
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
            _questNodeDisplay.Fill(_startNode);
        }

        private void ScreenControlOnRestart()
        {
            _questStartDisplay.SetActive(true);
            _questFinishDisplay.UnFill();
            _questNodeDisplay.UnFill();
        }

        private void ScreenControlOnStart()
        {
        }

        private void QuestNodeDisplayOnOptionSelected(QuestNode.Option selectedOption)
        {
            _questNodeDisplay.UnFill();
            if (selectedOption.Node.NodeType == QuestNode.NodeTypes.Options)
            {
                _questNodeDisplay.Fill(selectedOption.Node);
            }
            else
            {
                _questFinishDisplay.Fill(selectedOption.Node);
            }
        }

        public void Dispose()
        {
            _questNodeDisplay.onOptionSelected -= QuestNodeDisplayOnOptionSelected;
        }
    }
}