using System;
using System.Linq;
using SmartHead.Quest.Interfaces;
using UnityEngine;

namespace SmartHead.Quest.Components
{
    public class NodeBaseQuestProvider: MonoBehaviour, IQuestProviderAction, IQuestProviderEvent
    {
        [SerializeField] private QuestNode startNode;
        private QuestNode _currentNode;
        
        public event Action<IQuestionModel> onQuestionSet;
        public void StartOnButtonPressed()
        {
            _currentNode = startNode;
            onQuestionSet?.Invoke(_currentNode);
        }

        public void OptionOnSelected(IOptionModel model)
        {
            onQuestionSet?.Invoke((model as QuestNode.Option)!.Node);
        }
    }
}