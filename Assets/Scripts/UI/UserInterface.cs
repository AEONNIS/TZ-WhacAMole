using UnityEngine;
using UnityEngine.UI;
using WhacAMole.Infrastructure;
using WhacAMole.Model;

namespace WhacAMole.UI
{
    public class UserInterface : MonoBehaviour
    {
        [SerializeField] private CounterController _gridDimensionController;
        [SerializeField] private Button _startButton;
        [SerializeField] private GameState _gameState;

        public void Init(Counter gridDimension)
        {
            _gridDimensionController.Init(gridDimension);
        }
    }
}
