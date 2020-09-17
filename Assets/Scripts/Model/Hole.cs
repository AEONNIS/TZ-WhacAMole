using UnityEngine;
using WhacAMole.Infrastructure;

namespace WhacAMole.Model
{
    public class Hole : MonoBehaviour
    {
        [SerializeField] private Timer _timer;

        private Entity _entity = null;

        public bool IsEmpty => _entity == null;

        public void Spawn(Entity entity, float residenceTime)
        {
            if (IsEmpty)
            {
                _entity = Instantiate(entity, transform);
                _timer.StartOff(residenceTime, Remove);
            }
        }

        public void Remove()
        {
            if (IsEmpty == false)
            {
                _entity.Hiding();
                Destroy(_entity.gameObject);
                _entity = null;
                _timer.Stop();
            }
        }
    }
}
