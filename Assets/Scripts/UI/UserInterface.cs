using UnityEngine;
using UnityEngine.UI;
using WhacAMole.Model;

namespace WhacAMole.UI
{
    public class UserInterface : MonoBehaviour
    {
        [SerializeField] private CounterIndicator _gridDimension;
        [SerializeField] private Button _startButton;
        [SerializeField] private GameState _gameState;

        #region Unity
        private void Awake()
        {
            _gridDimension.Init();
            _gameState.Init(_gridDimension.CounterValue);
        }

        private void OnEnable()
        {
            _gridDimension.Decreased += OnGridDimensionDecreased;
            _gridDimension.Increased += OnGridDimensionIncreased;
        }

        private void OnDisable()
        {
            _gridDimension.Decreased -= OnGridDimensionDecreased;
            _gridDimension.Increased -= OnGridDimensionIncreased;
        }
        #endregion

        private void OnGridDimensionDecreased()
        {
            _gameState.FillGround(_gridDimension.CounterValue);
        }

        private void OnGridDimensionIncreased()
        {
            _gameState.FillGround(_gridDimension.CounterValue);
        }
    }
}
