using UnityEngine;
using WhacAMole.Infrastructure;
using WhacAMole.Model;

namespace WhacAMole.UI
{
    public class UserInterface : MonoBehaviour
    {
        [SerializeField] private CounterController _gridDimensionController;
        [SerializeField] private ChangedButton _startButton;
        [SerializeField] private GameState _gameState;

        public void Init(Counter gridDimension)
        {
            _gridDimensionController.Init(gridDimension);
            _startButton.Init(OnStart, OnStop);
        }

        private void OnStart()
        {
            _gameState.StartGame();
            _gridDimensionController.Disable();
            _startButton.ChangeState();
        }

        private void OnStop()
        {
            _gameState.StopGame();
            _gridDimensionController.Enable();
            _startButton.ChangeState();
        }
    }
}
