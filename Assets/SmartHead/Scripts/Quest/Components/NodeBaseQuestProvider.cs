using System.Linq;
using SmartHead.Quest.Interfaces;
using UnityEngine;

namespace SmartHead.Quest.Components
{
    public class NodeBaseQuestProvider: MonoBehaviour, IQuestProvider
    {
        [SerializeField] private QuestNode startNode;
        private QuestNode _currentNode;
        
        public IQuestionModel FromPlayerResponse(IOptionModel playerResponseModel)
        {
            return (playerResponseModel as QuestNode.Option)!.Node;
        }

        public IQuestionModel GetStartNode()
        {
            _currentNode = startNode;
            return _currentNode;
        }
    }
}