using System;
using UnityEngine;

namespace WhacAMole.Model
{
    public partial class Generator
    {
        [Serializable]
        private class ChangingRange
        {
            [SerializeField] private float _min;
            [SerializeField] private float _maxFactor;
            [SerializeField] private float _accelerationStep;

            private float _initialMin;

            public (float, float) CurrentRange => (_min, _min * _maxFactor);

            public void Init() => _initialMin = _min;

            public void Accelerate() => _min += _accelerationStep;

            public void Reset() => _min = _initialMin;
        }
    }
}
