using System;
using UnityEngine;

namespace WhacAMole.Infrastructure
{
    public partial class Generator
    {
        [Serializable]
        private struct AccelerationRange
        {
            [SerializeField] private int _minImpulses;
            [SerializeField] private int _maxImpulses;

            public int MinImpulses => _minImpulses;
            public int MaxImpulses => _maxImpulses;
        }
    }
}
