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

        public event Action Decreased;
        public event Action Increased;

        public int Value => _value;

        public int Decrease()
        {
            if (_value > _minLimiter)
            {
                --_value;
                Decreased?.Invoke();
            }

            return _value;
        }

        public int Increase()
        {
            if (_value < _maxLmiter)
            {
                ++_value;
                Increased?.Invoke();
            }

            return _value;
        }
    }
}
