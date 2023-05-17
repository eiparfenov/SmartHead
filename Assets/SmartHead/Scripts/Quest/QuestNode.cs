using System;
using NaughtyAttributes;
using SmartHead.Quest.Interfaces;
using UnityEngine;

namespace SmartHead.Quest
{
    [CreateAssetMenu(menuName = "SmartHead/Node")]
    public class QuestNode: ScriptableObject, IQuestionModel
    {
        [Serializable]
        public class Option: IOptionModel
        {
            [field: ResizableTextArea]
            [field: SerializeField] public string OptionText { get; private set; }
            [field: SerializeField] public QuestNode Node { get; private set; }
        }

        [field: ResizableTextArea]
        [field: SerializeField] public string NodeText { get; private set; }
        [field: SerializeField] public Sprite NodeImage { get; private set; }
        [field: SerializeField] public NodeTypes NodeType { get; private set; }
        [field: SerializeField] public Option[] Options { get; private set; }
        [field: SerializeField] public IOptionModel[] OptionModels => Options;
    }
}