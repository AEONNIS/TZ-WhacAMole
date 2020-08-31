using System;
using UnityEngine;

namespace WhacAMole.Infrastructure
{
    public partial class Generator
    {
        [Serializable]
        private struct ChangingRange
        {
            [SerializeField] private float _min;
            [SerializeField] private float _maxFactor;
            [SerializeField] private float _accelerationStep;
            [SerializeField] private Limiters _limiters;

            private float _initialMin;

            public (float, float) CurrentRange => (_min, _min * _maxFactor);

            public void Init() => _initialMin = _min;

            public void Accelerate()
            {
                if (_min > _limiters.Min && _min * _maxFactor < _limiters.Max)
                {
                    _min += _accelerationStep;
                    _min = Mathf.Clamp(_min, _limiters.Min, _limiters.Max / _maxFactor);
                }
            }

            public void Reset() => _min = _initialMin;

            [Serializable]
            private struct Limiters
            {
                [SerializeField] private float _min;
                [SerializeField] private float _max;

                public float Min => _min;
                public float Max => _max;
            }
        }
    }
}
