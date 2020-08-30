using System;
using UnityEngine;

namespace WhacAMole.Model
{
    public partial class Generator
    {
        [Serializable]
        private class AccelerationRange
        {
            [SerializeField] private int _minImpulses;
            [SerializeField] private int _maxImpulses;

            public int MinImpulses => _minImpulses;
            public int MaxImpulses => _maxImpulses;
        }
    }
}
