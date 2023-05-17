using UnityEngine;

namespace SmartHead.Quest.Interfaces
{
    public interface IQuestionModel
    {
        public string NodeText { get; }
        public Sprite NodeImage { get; }
        public NodeTypes NodeType { get; }
        public IOptionModel[] OptionModels { get; }
    }
}