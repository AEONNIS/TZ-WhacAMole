using System.Collections.Generic;
using UnityEngine;
using WhacAMole.Model;

namespace WhacAMole.Infrastructure
{
    public class EntityPool : MonoBehaviour
    {
        [SerializeField] private List<Entity> _etitiyTemplates;

        private List<Entity> _entities = new List<Entity>();
    }
}
