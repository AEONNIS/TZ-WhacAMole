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
        [SerializeField] private List<Entity> _entityTemplates;

        private List<Hole> _holes = new List<Hole>();

        #region Unity
        private void Awake()
        {
            Fill();
            CalculateAbsoluteSpawnFrequencies();

            for (int i = 0; i < _holes.Count; i++)
                Spawn();
        }
        #endregion

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

        private void CalculateAbsoluteSpawnFrequencies()
        {
            float sumRelativeSpawnFrequencies = 0f;

            foreach (var entity in _entityTemplates)
                sumRelativeSpawnFrequencies += entity.RelativeSpawnFrequency;

            foreach (var entity in _entityTemplates)
                entity.Init(entity.RelativeSpawnFrequency / sumRelativeSpawnFrequencies);
        }

        private void Spawn()
        {
            GetRandomEmptyHole().Spawn(GetRandomEntity());
        }

        private Hole GetRandomEmptyHole()
        {
            List<Hole> emptyHoles = _holes.Where(hole => hole.IsEmpty).ToList();
            return emptyHoles[Random.Range(0, emptyHoles.Count)];
        }

        private Entity GetRandomEntity()
        {
            float spawn = Random.Range(0f, 1f);
            float sumSpawnFrequencies = 0f;

            for (int i = 0; i < _entityTemplates.Count; i++)
            {
                sumSpawnFrequencies += _entityTemplates[i].AbsoluteSpawnFrequency;

                if (spawn <= sumSpawnFrequencies)
                    return Instantiate(_entityTemplates[i]);
            }

            return null;
        }
    }
}
