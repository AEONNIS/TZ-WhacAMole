using UnityEngine;
using WhacAMole.Infrastructure;
using WhacAMole.Model;

namespace WhacAMole.UI
{
    public class UserInterface : MonoBehaviour
    {
        [SerializeField] private CounterPresenter _gridDimensionPresenter;
        [SerializeField] private ChangedButton _startButton;
        [SerializeField] private GameState _gameState;

        public void Init(Counter gridDimension)
        {
            _gridDimensionPresenter.Init(gridDimension);
            _startButton.Init(OnStart, OnStop);
        }

        private void OnStart()
        {
            _gameState.StartGame();
            _gridDimensionPresenter.Disable();
            _startButton.ChangeState();
        }

        private void OnStop()
        {
            _gameState.StopGame();
            _gridDimensionPresenter.Enable();
            _startButton.ChangeState();
        }
    }
}
