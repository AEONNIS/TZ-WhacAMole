﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhacAMole.Infrastructure;

namespace WhacAMole.Model
{
    public class Ground : MonoBehaviour
    {
        [SerializeField] private GridCreator _gridCreator;
        [SerializeField] private Transform _content;
        [SerializeField] private Hole _holeTemplate;
        [SerializeField] private RandomEntityCreator _entityCreator;
        [SerializeField] private Generator _generator;
        [SerializeField] private GameState _gameState;

        private Pool<Hole> _holePool;
        private List<Hole> _holes = new List<Hole>();
        private Hole _lastHole = null;

        public Counter GridDimension => _gridCreator.Dimension;

        #region Unity
        private void OnEnable()
        {
            _gridCreator.Dimension.Changed += Fill;
            _generator.Impulse += SpawnRandomEntityInRandomHole;
        }

        private void OnDisable()
        {
            _gridCreator.Dimension.Changed -= Fill;
            _generator.Impulse -= SpawnRandomEntityInRandomHole;
        }
        #endregion

        public void Init()
        {
            _holePool = new Pool<Hole>(_content);
            Fill(_gridCreator.Dimension.Value);
            _entityCreator.Init();
        }

        public void SpawnRandomEntityInRandomHole(float residenceTime)
        {
            Hole hole = GetRandomEmptyHole();
            hole.ToPlace(_entityCreator.GetRandomEntity(hole.transform), residenceTime);
        }

        private void Fill(int gridDimension)
        {
            Clear();
            _gridCreator.Create(gridDimension);
            _holes = CreateHoles(gridDimension);
        }

        private void Clear()
        {
            _holes.ForEach(hole => _holePool.Return(hole));
            _holes.Clear();
        }

        private List<Hole> CreateHoles(int gridDimension)
        {
            List<Hole> holes = new List<Hole>();

            for (int i = 0; i < gridDimension * gridDimension; i++)
            {
                Hole hole = _holePool.Get(_holeTemplate, _content);
                hole.Init(_gameState, _entityCreator);
                holes.Add(hole);
            }

            return holes;
        }

        private Hole GetRandomEmptyHole()
        {
            List<Hole> emptyHoles = _holes.Where(hole => hole.IsEmpty).ToList();
            Hole newHole = emptyHoles[Random.Range(0, emptyHoles.Count)];

            while (_lastHole == newHole)
                newHole = emptyHoles[Random.Range(0, emptyHoles.Count)];

            _lastHole = newHole;
            return newHole;
        }
    }
}
