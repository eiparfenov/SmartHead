using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace SmartHead.Utils.LiveData
{
    public interface ILiveData<T>
    {
        T Value { get; }
        event Action<T> onValueChanged;
        event OnValueChangedWithBuffer<T> onValueChangeWithBuffer;
        UniTask<T> WaitUntilChanged(Predicate<T> expectation, PlayerLoopTiming timing = PlayerLoopTiming.Update, CancellationToken cancellationToken = default);
    }

    public delegate void OnValueChangedWithBuffer<T>(T previousValue, T currentValue);
}