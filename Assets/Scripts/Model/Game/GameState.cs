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
        [SerializeField] private GameDeltas _initDeltas = new GameDeltas(5, 0);
        [SerializeField] private UserInterface _userInterface;
        private GameDeltas _currentDeltas;

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
            _currentDeltas = _initDeltas;
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
            _currentDeltas.ChangeTo(deltas);
            StateChanged?.Invoke(_currentDeltas);
        }
    }
}