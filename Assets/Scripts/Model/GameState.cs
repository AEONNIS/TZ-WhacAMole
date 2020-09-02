using UnityEngine;
using WhacAMole.Infrastructure;

namespace WhacAMole.Model
{
    public class GameState : MonoBehaviour
    {
        [SerializeField] private Ground _ground;
        [SerializeField] private Generator _generator;
        [SerializeField] private int _initialLivesNumber = 5;

        private int _score = 0;

        #region Unity
        private void Awake()
        {
            _ground.Init();
            _generator.Init();
        }
        #endregion
    }
}