using System;
using UnityEngine;

namespace WhacAMole.Infrastructure
{
    [Serializable]
    public class Counter
    {
        [SerializeField] private int _value = 0;
        [SerializeField] private int _minLimiter = int.MinValue;
        [SerializeField] private int _maxLmiter = int.MaxValue;

        public event Action<int> Changed;

        public int Value => _value;

        public int Decrease()
        {
            if (_value > _minLimiter)
                Changed?.Invoke(--_value);

            return _value;
        }

        public int Increase()
        {
            if (_value < _maxLmiter)
                Changed?.Invoke(++_value);

            return _value;
        }
    }
}
