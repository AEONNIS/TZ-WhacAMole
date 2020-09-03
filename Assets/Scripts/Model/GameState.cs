using UnityEngine;
using WhacAMole.Infrastructure;

namespace WhacAMole.Model
{
    public class GameState : MonoBehaviour
    {
        [SerializeField] private Ground _ground;
        [SerializeField] private Generator _generator;
        [SerializeField] private int _livesNumber = 5;

        private int _score = 0;

        public void Init(int gridDimension)
        {
            _ground.Init(gridDimension);
            _generator.Init();
        }

        public void FillGround(int gridDimension)
        {
            _ground.Fill(gridDimension);
        }
    }
}