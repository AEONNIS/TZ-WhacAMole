using UnityEngine;
using UnityEngine.EventSystems;
using WhacAMole.Model;

namespace WhacAMole.Controllers
{
    public class EntityController : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Entity _entity;

        #region Unity
        public void OnPointerClick(PointerEventData eventData)
        {
            _entity.Hit();
        }
        #endregion
    }
}