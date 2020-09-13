using UnityEngine;

namespace WhacAMole.Model
{
    public class Entity : MonoBehaviour
    {
        [Range(0f, 1f)]
        [SerializeField] private float _relativeSpawnFrequency = 0.5f;
        [SerializeField] private EntityDeltasSet _deltasSet;
        private GameState _gameState;

        public float RelativeSpawnFrequency => _relativeSpawnFrequency;

        public void Init(GameState gameState)
        {
            _gameState = gameState;
        }

        public void Hit()
        {
            _gameState.Change(_deltasSet.OnHitDeltas);
        }

        public void Hiding()
        {
            _gameState.Change(_deltasSet.OnHidingDeltas);
        }
    }
}
