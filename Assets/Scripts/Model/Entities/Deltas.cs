using System;
using UnityEngine;

namespace WhacAMole.Model
{
    [Serializable]
    public struct Deltas
    {
        [Range(-10, 10)]
        [SerializeField] private int _scores;
        [Range(-10, 10)]
        [SerializeField] private int _lifes;

        public int Scores => _scores;
        public int Lifes => _lifes;
    }
}
