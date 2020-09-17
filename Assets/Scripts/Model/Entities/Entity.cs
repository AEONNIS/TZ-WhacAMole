using UnityEngine;

namespace WhacAMole.Model
{
    public class Entity : MonoBehaviour, INameable
    {
        [SerializeField] private EntityDeltasSet _deltasSet;
        [SerializeField] private Type _type;
        private RandomEntityCreator _entityCreator;
        private GameState _gameState;

        private enum Type { Mole, AntiMole, Leprechaun }

        public string Name { get; private set; }

        public void Init(RandomEntityCreator entityCreator, GameState gameState)
        {
            _entityCreator = entityCreator;
            _gameState = gameState;
            Name = _type.ToString();
        }

        public void Hit()
        {
            _gameState.Change(_deltasSet.OnHitDeltas);
            _entityCreator.RemoveEntity(this);
        }

        public void Hiding()
        {
            _gameState.Change(_deltasSet.OnHidingDeltas);
            _entityCreator.RemoveEntity(this);
        }
    }
}
