using System;
using UnityEngine;

namespace WhacAMole.Model
{
    public partial class TopScores
    {
        [Serializable]
        public struct ScoreEntry
        {
            [SerializeField] private int _score;
            [SerializeField] private string _name;

            public ScoreEntry(int score, string name)
            {
                _score = score;
                _name = name;
            }

            public int Score => _score;
            public string Name => _name;
        }
    }
}