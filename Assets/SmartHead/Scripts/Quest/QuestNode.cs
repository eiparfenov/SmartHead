using System;
using NaughtyAttributes;
using UnityEngine;

namespace SmartHead.Quest
{
    [CreateAssetMenu(menuName = "SmartHead/Node")]
    public class QuestNode: ScriptableObject
    {
        [Serializable]
        public class Option
        {
            [field: ResizableTextArea]
            [field: SerializeField] public string OptionText { get; private set; }
            [field: SerializeField] public QuestNode Node { get; private set; }
        }
        public enum NodeTypes { Options, Result}
        [field: ResizableTextArea]
        [field: SerializeField] public string NodeText { get; private set; }
        [field: SerializeField] public Sprite NodeImage { get; private set; }
        [field: SerializeField] public NodeTypes NodeType { get; private set; }
        [field: SerializeField] public Option[] Options { get; private set; }
    }
}