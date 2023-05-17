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


        public QuestManager(QuestNode startNode, QuestNodeDisplay questNodeDisplay, IScreenControl screenControl)
        {
            _startNode = startNode;
            _questNodeDisplay = questNodeDisplay;
            _screenControl = screenControl;
        }

        public void Initialize()
        {
            _questNodeDisplay.onOptionSelected += QuestNodeDisplayOnOptionSelected;
            _screenControl.onStart += ScreenControlOnStart;
            _screenControl.onRestart += ScreenControlOnRestart;
        }

        private void ScreenControlOnRestart()
        {
            _questNodeDisplay.UnFill();
        }

        private void ScreenControlOnStart()
        {
            _questNodeDisplay.Fill(_startNode);
        }

        private void QuestNodeDisplayOnOptionSelected(QuestNode.Option selectedOption)
        {
            _questNodeDisplay.UnFill();
            _questNodeDisplay.Fill(selectedOption.Node);
        }

        public void Dispose()
        {
            _questNodeDisplay.onOptionSelected -= QuestNodeDisplayOnOptionSelected;
        }
    }
}