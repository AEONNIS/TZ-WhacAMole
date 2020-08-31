using System;
using UnityEngine;

namespace WhacAMole.Model
{
    [Serializable]
    public struct Deltas
    {
        [Range(-10, 10)]
        [SerializeField] private int _score;
        [Range(-10, 10)]
        [SerializeField] private int _health;

        public int Score => _score;
        public int Health => _health;
    }
}
