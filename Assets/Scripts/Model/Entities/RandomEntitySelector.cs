using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WhacAMole.Model
{
    public class RandomEntitySelector : MonoBehaviour
    {
        [SerializeField] private List<Entity> _entityTemplates;

        public void Init()
        {
            CalculateAbsoluteSpawnFrequencies();
        }

        public Entity GetRandomEntityTemplate()
        {
            float spawn = Random.Range(0f, 1f);
            float sumSpawnFrequencies = 0f;

            foreach (var entity in _entityTemplates)
            {
                sumSpawnFrequencies += entity.AbsoluteSpawnFrequency;

                if ((spawn <= sumSpawnFrequencies))
                    return entity;
            }

            return null;
        }

        private void CalculateAbsoluteSpawnFrequencies()
        {
            float sumRelativeSpawnFrequencies = _entityTemplates.Select(entity => entity.RelativeSpawnFrequency).Sum();
            _entityTemplates.ForEach(entity => entity.Init(entity.RelativeSpawnFrequency / sumRelativeSpawnFrequencies));
        }
    }
}
