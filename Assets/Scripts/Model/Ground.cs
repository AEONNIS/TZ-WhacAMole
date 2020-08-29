using System.Collections.Generic;
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

        private List<Hole> _holes;

        #region Unity
        private void Awake()
        {
            Fill();
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
            foreach (Transform child in _content)
                Destroy(child.gameObject);
        }

        private List<Hole> CreateHoles()
        {
            List<Hole> holes = new List<Hole>();

            for (int i = 0; i < _gridDimension * _gridDimension; i++)
                holes.Add(Instantiate(_holeTemplate, _content));

            return holes;
        }
    }
}
