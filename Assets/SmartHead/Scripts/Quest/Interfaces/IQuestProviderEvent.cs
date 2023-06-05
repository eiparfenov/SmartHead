using System;

namespace SmartHead.Quest.Interfaces
{
    public interface IQuestProviderEvent
    {
        void StartOnButtonPressed();
        void OptionOnSelected(IOptionModel model);
    }
}