using UnityEngine;
using WhacAMole.Infrastructure;

namespace WhacAMole.Model
{
    public class Hole : MonoBehaviour
    {
        [SerializeField] private Timer _timer;

        private GameState _gameState;
        private RandomEntityCreator _entityCreator;

        private Entity _entity = null;

        public bool IsEmpty => _entity == null;

        public void Init(GameState gameState, RandomEntityCreator entityCreator)
        {
            _gameState = gameState;
            _entityCreator = entityCreator;
        }

        public void ToPlace(Entity entity, float residenceTime)
        {
            if (IsEmpty)
            {
                _entity = entity;
                _entity.Init(_entityCreator, _gameState);
                _timer.StartOff(residenceTime, Remove);
            }
        }

        public void Remove()
        {
            if (IsEmpty == false)
            {
                _entity.Hiding();
                _entity = null;
                _timer.Stop();
            }
        }
    }
}
