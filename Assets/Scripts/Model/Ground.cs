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

        #region Unity
        private void Awake()
        {
            Fill();
            _entitySelector.Init();
            _generator.Init();
            _generator.Impulse += SpawnRandomEntityInRandomHole;
        }

        private void Start()
        {
            _generator.Run();
        }
        #endregion

        public void SpawnRandomEntityInRandomHole(float residenceTime)
        {
            GetRandomEmptyHole().Spawn(_entitySelector.GetRandomEntityTemplate(), residenceTime);
        }

        private void Fill()
        {
            Clear();
            _gridCreator.Create(_gridDimension);
            _holes = CreateHoles();
        }

        private void Clear()
        {
            foreach (Hole hole in _holes)
                Destroy(hole.gameObject);

            _holes.Clear();
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
            int index = Random.Range(0, emptyHoles.Count);
            return emptyHoles[index];
        }
    }
}
