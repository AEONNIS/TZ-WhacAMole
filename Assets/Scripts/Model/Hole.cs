using UnityEngine;

namespace WhacAMole.Model
{
    public class Hole : MonoBehaviour
    {
        private Entity _entity = null;

        public bool IsEmpty => _entity == null;

        public void Spawn(Entity entity)
        {
            entity.transform.SetParent(transform);
            entity.transform.position = transform.position;
            _entity = entity;
        }

        public void Remove()
        {
            Destroy(_entity.gameObject);
        }
    }
}
