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
        [SerializeField] private int _gridDimension = 3;
        [SerializeField] private RandomEntitySelector _entitySelector;
        [SerializeField] private Generator _generator;

        private List<Hole> _holes = new List<Hole>();
        private Hole _lastHole = null;

        #region Unity
        private void OnEnable()
        {
            _generator.Impulse += SpawnRandomEntityInRandomHole;
        }

        private void OnDisable()
        {
            _generator.Impulse -= SpawnRandomEntityInRandomHole;
        }
        #endregion

        public void Init()
        {
            Fill();
            _entitySelector.Init();
        }

        public void SpawnRandomEntityInRandomHole(float residenceTime)
        {
            GetRandomEmptyHole().Spawn(_entitySelector.GetRandomEntityTemplate(), residenceTime);
        }

        private void Clear()
        {
            _holes.ForEach(hole => Destroy(hole.gameObject));
            _holes.Clear();
        }

        private void Fill()
        {
            Clear();
            _gridCreator.Create(_gridDimension);
            _holes = CreateHoles();
        }

        private List<Hole> CreateHoles()
        {
            List<Hole> holes = new List<Hole>();

            for (int i = 0; i < _gridDimension * _gridDimension; i++)
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
