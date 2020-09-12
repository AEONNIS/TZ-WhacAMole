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
        [SerializeField] private int _initLifes = 5;
        [SerializeField] private UserInterface _userInterface;

        private int _lifes;
        private int _scores = 0;

        public event Action GameStarted;
        public event Action GameStopped;

        public event Action<int> LifesChanged;
        public event Action<int> ScoresChanged;

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
            _generator.Run();
            _lifes = _initLifes;
            _scores = 0;
            GameStarted?.Invoke();
        }

        public void GameStop()
        {
            _generator.Stop();
            GameStopped?.Invoke();
        }

        public void Change(Deltas deltas)
        {
            LifesChange(deltas.Lifes);
            ScoresChange(deltas.Scores);
        }

        private void LifesChange(int delta)
        {
            _lifes += delta;
            LifesChanged?.Invoke(_lifes);

            if (_lifes <= 0)
                GameStop();
        }

        private void ScoresChange(int delta)
        {
            _scores += delta;
            ScoresChanged?.Invoke(_scores);
        }
    }
}