using System;
using UnityEngine;

namespace WhacAMole.Model
{
    [Serializable]
    public struct GameDeltas
    {
        [Range(-10, 10)]
        [SerializeField] private int _lifes;
        [Range(-10, 10)]
        [SerializeField] private int _scores;

        public GameDeltas(int lifes, int scores)
        {
            _lifes = lifes;
            _scores = scores;
        }

        public int Lifes => _lifes;
        public int Scores => _scores;

        public void ChangeTo(GameDeltas deltas)
        {
            _lifes += deltas.Lifes;
            _scores += deltas.Scores;
        }
    }
}
