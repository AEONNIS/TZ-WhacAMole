using UnityEngine;

namespace WhacAMole.Model
{
    [CreateAssetMenu(fileName = "DeltasSet", menuName = "WhacAMole/Model/DeltasSet")]
    public class DeltasSet : ScriptableObject
    {
        [SerializeField] private Deltas _hitDeltas;
        [SerializeField] private Deltas _missDeltas;

        public Deltas HitDeltas => _hitDeltas;
        public Deltas MissDeltas => _missDeltas;
    }
}
