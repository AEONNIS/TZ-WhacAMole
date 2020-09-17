using System;
using UnityEngine;
using WhacAMole.Infrastructure;
using WhacAMole.UI;

namespace WhacAMole.Model
{
    public class GameState : MonoBehaviour
    {
        [SerializeField] private Ground _ground;
        [SerializeField] private Generator _generator;
        [SerializeField] private GameDeltas _initState = new GameDeltas(5, 0);
        [SerializeField] private UserInterface _userInterface;
        private GameDeltas _currentState;

        public event Action GameStarted;
        public event Action GameStopped;
        public event Action<GameDeltas> StateChanged;

        #region Unity
        private void Awake()
        {
            _ground.Init();
            _generator.Init();
            _userInterface.Init(_ground.GridDimension);
        }
        #endregion

        public void GameStart()
        {
            _currentState = _initState;
            _generator.Run();
            GameStarted?.Invoke();
        }

        public void GameStop()
        {
            _generator.Stop();
            GameStopped?.Invoke();
        }

        public void Change(GameDeltas deltas)
        {
            _currentState.ChangeTo(deltas);
            StateChanged?.Invoke(_currentState);
        }
    }
}