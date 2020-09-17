using System.Collections.Generic;
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
        [SerializeField] private RandomEntityCreator _entitySelector;
        [SerializeField] private Generator _generator;

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
            Fill(_gridCreator.Dimension.Value);
            _entitySelector.Init();
        }

        public void SpawnRandomEntityInRandomHole(float residenceTime)
        {
            GetRandomEmptyHole().Spawn(_entitySelector.GetRandomEntity(), residenceTime);
        }

        private void Fill(int gridDimension)
        {
            Clear();
            _gridCreator.Create(gridDimension);
            _holes = CreateHoles(gridDimension);
        }

        private void Clear()
        {
            _holes.ForEach(hole => Destroy(hole.gameObject));
            _holes.Clear();
        }

        private List<Hole> CreateHoles(int gridDimension)
        {
            List<Hole> holes = new List<Hole>();

            for (int i = 0; i < gridDimension * gridDimension; i++)
                holes.Add(Instantiate(_holeTemplate, _content));

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
