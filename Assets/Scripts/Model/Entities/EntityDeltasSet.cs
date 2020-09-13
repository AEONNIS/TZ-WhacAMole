using UnityEngine;

namespace WhacAMole.Model
{
    [CreateAssetMenu(fileName = "EntityDeltasSet", menuName = "WhacAMole/Entities/EntityDeltasSet")]
    public class EntityDeltasSet : ScriptableObject
    {
        [SerializeField] private GameDeltas _onHitDeltas;
        [SerializeField] private GameDeltas _onHidingDeltas;

        public GameDeltas OnHitDeltas => _onHitDeltas;
        public GameDeltas OnHidingDeltas => _onHidingDeltas;
    }
}
