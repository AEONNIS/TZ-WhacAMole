using System;
using System.Collections.Generic;
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
        [SerializeField] private HolesSettings _holesSettings;
        private List<TestHole> _holes = new List<TestHole>();

        #region Unity
        private void Awake()
        {
            for (int i = 0; i <= _holesSettings.Count; i++)
            {
                HoleParameters holeParameters = new HoleParameters(_baseScaler, _holesSettings, _holes);

                if (holeParameters.IsValid)
                    Create(holeParameters.Position, holeParameters.Diameter);
                else
                    Debug.LogWarning($"({_holes.Count}: ){ _holesSettings.CreationImpossibilityMessage}");
            }
        }
        #endregion

        public TestHole Create(Vector2 position, float diameter)
        {
            TestHole hole = Instantiate(_holeTemplate, _ground);
            hole.Init(position, diameter);
            return hole;
        }

        [Serializable]
        private class HolesSettings
        {
            [Range(0, 100)]
            [SerializeField] private int _count = 30;
            [SerializeField] private Vector2 _diameterRange = new Vector2(90, 110);
            [SerializeField] private float _minEdgeDistance = 0f;
            [SerializeField] private float _minDistanceBetween = 0f;
            [SerializeField] private int _maxAttemptsToFindPosition = 100;
            [TextArea(1, 3)]
            [SerializeField] private string _creationImpossibilityMessage = "it is impossible to create a hole with the given settings";

            public int Count => _count;
            public Vector2 DiameterRange => _diameterRange;
            public float MinEdgeDistance => _minEdgeDistance;
            public float MinDistanceBetween => _minDistanceBetween;
            public int MaxAttemptsToFindPosition => _maxAttemptsToFindPosition;
            public string CreationImpossibilityMessage => _creationImpossibilityMessage;
        }

        private class HoleParameters
        {
            private float _radius;
            private float _diameter;
            private Vector2 _min;
            private Vector2 _max;
            private Vector2? _position;

            public HoleParameters(CanvasScaler scaler, HolesSettings settings, List<TestHole> holes)
            {
                float diameter = Random.Range(settings.DiameterRange.x, settings.DiameterRange.y);
                _radius = 0.5f * diameter;
                _diameter = diameter;
                float radiusAndDistance = _radius + settings.MinEdgeDistance;
                _min = new Vector2(radiusAndDistance, -radiusAndDistance);
                _max = new Vector2(scaler.referenceResolution.x - radiusAndDistance, -scaler.referenceResolution.y + radiusAndDistance);
                _position = GetValidPositionIfExists(scaler, settings, holes);
            }

            public bool IsValid => _position.HasValue;
            public Vector2 Position => _position.Value;
            public float Diameter => _diameter;

            private Vector2? GetValidPositionIfExists(CanvasScaler scaler, HolesSettings settings, List<TestHole> holes)
            {
                Vector2? position = null;

                for (int i = 0; i <= settings.MaxAttemptsToFindPosition; i++)
                {
                    position = GetRandomPosition(_min, _max);

                    if (CheckPosition(position.Value, _radius, holes, settings.MinDistanceBetween))
                        return position;
                }

                return null;
            }

            private bool CheckPosition(Vector2 position, float radius, List<TestHole> holes, float minDistanceBetween)
            {
                foreach (var hole in holes)
                {
                    if (CheckDistanceForMin(position, hole.Position, minDistanceBetween + radius + hole.Radius) == false)
                        return false;
                }

                return true;
            }

            private bool CheckDistanceForMin(Vector2 aPosition, Vector2 bPosition, float minDistance)
            {
                return Mathf.Abs((aPosition - bPosition).sqrMagnitude) >= minDistance * minDistance;
            }

            private Vector2 GetRandomPosition(Vector2 min, Vector2 max)
            {
                return new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
            }
        }
    }
}
