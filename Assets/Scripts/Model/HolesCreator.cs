using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace WhacAMole.Model
{
    public class HolesCreator : MonoBehaviour
    {
        [SerializeField] private CanvasScaler _baseScaler;
        [SerializeField] private RectTransform _ground;
        [SerializeField] private TestHole _holeTemplate;
        [SerializeField] private Settings _settings;

        #region Unity
        private void Awake()
        {
            Create();
        }
        #endregion

        private void Create()
        {
            TestHole hole = Instantiate(_holeTemplate, _ground);
            hole.Init(GetRandomPosition(), _settings.HoleDiameter);
        }

        private Vector2 GetRandomPosition()
        {
            return new Vector2(1, -1) * _baseScaler.referenceResolution + new Vector2(-_settings.HoleDiameter * 0.5f, _settings.HoleDiameter * 0.5f);
        }

        [Serializable]
        private struct Settings
        {
            [SerializeField] private float _holeDiameter;
            [SerializeField] private float _minEdgeDistance;
            [SerializeField] private float _minDistanceBetweenHoles;

            public float HoleDiameter => _holeDiameter;
            public float MinEdgeDistance => _minEdgeDistance;
            public float MinDistanceBetweenHoles => _minDistanceBetweenHoles;
        }
    }
}
