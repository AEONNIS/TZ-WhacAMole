using UnityEngine;
using UnityEngine.UI;

namespace WhacAMole.Infrastructure
{
    public class GridCreator : MonoBehaviour
    {
        [SerializeField] private RectTransform _transform;
        [SerializeField] private GridLayoutGroup _layout;
        [Range(0f, 3f)]
        [SerializeField] private float _cellSpacingFactor = 0.5f;
        [SerializeField] private Counter _dimension;

        public Counter Dimension => _dimension;

        public void Create(int dimension)
        {
            float cellSide = _transform.rect.width / (dimension + (dimension + 1) * _cellSpacingFactor);
            int spaceSide = (int)(_cellSpacingFactor * cellSide);

            _layout.cellSize = new Vector2(cellSide, cellSide);
            _layout.padding = new RectOffset(spaceSide, spaceSide, spaceSide, spaceSide);
            _layout.spacing = new Vector2(spaceSide, spaceSide);
        }
    }
}
