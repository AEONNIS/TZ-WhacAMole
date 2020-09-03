using System;
using System.Collections.Generic;
using UnityEngine;

namespace WhacAMole.Model
{
    [Serializable]
    public partial class TopScores
    {
        [SerializeField] private List<ScoreEntry> _entries = new List<ScoreEntry>();

        public IReadOnlyList<ScoreEntry> Entires => _entries;

        public bool TryAdd(int score, string name)
        {
            if (score <= 0 || string.IsNullOrWhiteSpace(name))
            {
                return false;
            }
            else
            {
                _entries.Add(new ScoreEntry(score, name));
                return true;
            }
        }
    }
}