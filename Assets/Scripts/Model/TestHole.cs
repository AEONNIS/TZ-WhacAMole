using UnityEngine;

namespace WhacAMole.Model
{
    public class TestHole : MonoBehaviour
    {
        [SerializeField] private RectTransform _transform;

        public Vector2 Position => _transform.anchoredPosition;
        public float Radius => 0.5f * _transform.sizeDelta.x;

        public void Init(Vector2 position, float diameter)
        {
            _transform.anchoredPosition = position;
            _transform.sizeDelta = new Vector2(diameter, diameter);
        }
    }
}
