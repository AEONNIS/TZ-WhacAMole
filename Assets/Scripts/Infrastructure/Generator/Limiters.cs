using System;
using UnityEngine;

namespace WhacAMole.Infrastructure
{
    public partial class Generator
    {
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
