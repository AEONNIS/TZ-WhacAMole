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

        public float RelativeSpawnFrequency => _relativeSpawnFrequency;
        public float AbsoluteSpawnFrequency => _absoluteSpawnFrequency;

        public void Init(float absoluteSpawnFrequency)
        {
            _absoluteSpawnFrequency = absoluteSpawnFrequency;
        }

        public void OnHit()
        {
            Debug.Log("Hit");
        }

        public void OnHiding()
        {
            Debug.Log("Hiding");
        }
    }

    public enum EntityType { Mole, AntiMole, Leprechaun }
}
