using System;
using UnityEngine;

namespace WhacAMole.Model
{
    [Serializable]
    public struct Deltas
    {
        [SerializeField] private int _score;
        [SerializeField] private int _health;

        public int Score => _score;
        public int Health => _health;
    }
}
