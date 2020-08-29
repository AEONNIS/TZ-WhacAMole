using UnityEngine;

namespace WhacAMole.Model
{
    public class Entity : MonoBehaviour
    {
        [SerializeField] private DeltasSet _deltas;
        [Range(0f, 1f)]
        [SerializeField] private float _spawnFrequency = 0.5f;

        public void OnHit()
        {
            Debug.Log("Hit");
        }

        public void OnHiding()
        {
            Debug.Log("Hiding");
        }
    }
}
