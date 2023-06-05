using System;

namespace SmartHead.Quest.Interfaces
{
    public interface IQuestProviderAction
    {
        event Action<IQuestionModel> onQuestionSet;
    }
}