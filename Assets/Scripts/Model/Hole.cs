using UnityEngine;

namespace WhacAMole.Model
{
    public class Hole : MonoBehaviour
    {
        private Entity _entity = null;

        public bool IsEmpty => _entity == null;

        public void Spawn(Entity entityTemplate)
        {
            _entity = Instantiate(entityTemplate, transform);
        }

        public void Remove()
        {
            if (IsEmpty == false)
            {
                Destroy(_entity.gameObject);
                _entity = null;
            }
        }
    }
}
