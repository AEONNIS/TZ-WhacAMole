using UnityEngine;

namespace WhacAMole.Model
{
    public class Entity : MonoBehaviour
    {
        [SerializeField] EntityType _type;
        [Range(0f, 1f)]
        [SerializeField] private float _relativeSpawnFrequency = 0.5f;
        [SerializeField] private DeltasSet _deltas;

        private float _absoluteSpawnFrequency;
        private GameState _gameState;

        public float RelativeSpawnFrequency => _relativeSpawnFrequency;
        public float AbsoluteSpawnFrequency => _absoluteSpawnFrequency;

        public void Init(float absoluteSpawnFrequency, GameState gameState)
        {
            _absoluteSpawnFrequency = absoluteSpawnFrequency;
            _gameState = gameState;
        }

        public void Hit()
        {
            if (_gameState != null)
                _gameState.Change(_deltas.OnHitDeltas);
        }

        public void Hiding()
        {
            if (_gameState != null)
                _gameState.Change(_deltas.OnHidingDeltas);
        }
    }

    public enum EntityType { Mole, AntiMole, Leprechaun }
}
