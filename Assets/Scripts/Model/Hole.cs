using UnityEngine;
using WhacAMole.Infrastructure;

namespace WhacAMole.Model
{
    public class Hole : MonoBehaviour
    {
        [SerializeField] private Timer _timer;

        private Entity _entity = null;

        public bool IsEmpty => _entity == null;

        public void Spawn(Entity entityTemplate, float residenceTime)
        {
            if (IsEmpty)
                _entity = Instantiate(entityTemplate, transform);

            _timer.StartOff(residenceTime, Remove);
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
