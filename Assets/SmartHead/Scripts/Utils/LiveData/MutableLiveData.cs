using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace SmartHead.Utils.LiveData
{
    public class MutableLiveData<T>: ILiveData<T>
    {
        private T _value;
        public T Value
        {
            get => _value;
            set
            {
                if(Equals(value, _value)) return;

                var previousValue = _value;
                _value = value;
                onValueChanged?.Invoke(_value);
                onValueChangeWithBuffer?.Invoke(previousValue, _value);
            }
        }
        public event Action<T> onValueChanged;
        public event OnValueChangedWithBuffer<T> onValueChangeWithBuffer;
        public async UniTask<T> WaitUntilChanged(Predicate<T> expectation, PlayerLoopTiming timing, CancellationToken cancellationToken)
        {
            var returnable = false;
            var callback = new Action<T>(value =>
            {
                if (expectation == null || expectation(value))
                {
                    returnable = true;
                }
            });
            onValueChanged += callback;
            await UniTask.WaitUntil(() => returnable, timing, cancellationToken);
            onValueChanged -= callback;
            return _value;
        }
    }
}