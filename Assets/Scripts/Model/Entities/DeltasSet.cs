using UnityEngine;

namespace WhacAMole.Model
{
    [CreateAssetMenu(fileName = "DeltasSet", menuName = "WhacAMole/Model/DeltasSet")]
    public class DeltasSet : ScriptableObject
    {
        [SerializeField] private Deltas _onHitDeltas;
        [SerializeField] private Deltas _onHidingDeltas;

        public Deltas OnHitDeltas => _onHitDeltas;
        public Deltas OnHidingDeltas => _onHidingDeltas;
    }
}
