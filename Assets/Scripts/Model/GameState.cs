using UnityEngine;
using WhacAMole.Infrastructure;
using WhacAMole.UI;

namespace WhacAMole.Model
{
    public class GameState : MonoBehaviour
    {
        [SerializeField] private Ground _ground;
        [SerializeField] private Generator _generator;
        [SerializeField] private int _lifes = 5;
        [SerializeField] private UserInterface _userInterface;

        private int _score = 0;

        #region Unity
        private void Awake()
        {
            _ground.Init();
            _generator.Init();
            _userInterface.Init(_ground.GridDimension);
        }
        #endregion

        public void StartGame() => _generator.Run();

        public void StopGame() => _generator.Stop();
    }
}