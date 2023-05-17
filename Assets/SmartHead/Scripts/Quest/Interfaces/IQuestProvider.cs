namespace SmartHead.Quest.Interfaces
{
    public interface IQuestProvider
    {
        IQuestionModel FromPlayerResponse(IOptionModel playerResponseModel);
        IQuestionModel GetStartNode();
    }
}