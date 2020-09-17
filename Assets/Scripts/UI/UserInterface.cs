using TMPro;
using UnityEngine;
using WhacAMole.Infrastructure;
using WhacAMole.Model;

namespace WhacAMole.UI
{
    public class UserInterface : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _lifesPresenter;
        [SerializeField] private TextMeshProUGUI _scoresPrsenter;
        [SerializeField] private ChangedButton _startGameButton;
        [SerializeField] private CounterPresenter _gridDimensionPresenter;
        [SerializeField] private GameState _gameState;

        #region Unity
        private void OnEnable()
        {
            _gameState.GameStarted += OnGameStarted;
            _gameState.GameStopped += OnGameStopped;
            _gameState.StateChanged += OnStateChanged;
        }

        private void OnDisable()
        {
            _gameState.GameStarted -= OnGameStarted;
            _gameState.GameStopped -= OnGameStopped;
            _gameState.StateChanged -= OnStateChanged;
        }
        #endregion

        public void Init(Counter gridDimension)
        {
            _gridDimensionPresenter.Init(gridDimension);
            _startGameButton.Init(OnStartGamePressed, OnStopGamePressed);
        }

        private void OnStartGamePressed() => _gameState.GameStart();

        private void OnStopGamePressed() => _gameState.GameStop();

        private void OnGameStarted()
        {
            _gridDimensionPresenter.Disable();
            _startGameButton.ChangeState();
        }

        private void OnGameStopped()
        {
            _gridDimensionPresenter.Enable();
            _startGameButton.ChangeState();
        }

        private void OnStateChanged(GameDeltas state)
        {
            _lifesPresenter.text = state.Lifes.ToString();
            _scoresPrsenter.text = state.Scores.ToString();
        }
    }
}
