using System;

namespace SmartHead.HandControl.Interfaces
{
    public interface IRestartEventSender
    {
        event Action onRestart;
    }
}