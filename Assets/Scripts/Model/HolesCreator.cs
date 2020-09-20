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
        [SerializeField] private HolesSettings _settings;
        private List<TestHole> _holes = new List<TestHole>();

        #region Unity
        private void Awake()
        {
            for (int i = 0; i <= _settings.Count; i++)
            {
                HoleParameters parameters = new HoleParameters(_baseScaler, _settings, _holes);

                if (parameters.IsValid)
                {
                    _holes.Add(Create(_holeTemplate, _ground, parameters.Position, parameters.Diameter));
                }
                else
                {
                    Debug.LogWarning($"({_holes.Count}: ){ _settings.CreationImpossibilityMessage}");
                    return;
                }
            }
        }
        #endregion

        public TestHole Create(TestHole template, Transform parent, Vector2 position, float diameter)
        {
            TestHole hole = Instantiate(template, parent);
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
            private readonly float _radius;
            private Vector2 _min;
            private Vector2 _max;
            private Vector2? _position;

            public HoleParameters(CanvasScaler scaler, HolesSettings settings, List<TestHole> holes)
            {
                Diameter = Random.Range(settings.DiameterRange.x, settings.DiameterRange.y);
                _radius = 0.5f * Diameter;
                float radiusAndDistance = _radius + settings.MinEdgeDistance;
                _min = new Vector2(radiusAndDistance, -radiusAndDistance);
                _max = new Vector2(scaler.referenceResolution.x - radiusAndDistance, -scaler.referenceResolution.y + radiusAndDistance);
                _position = GetValidPositionIfExists(settings, holes, _min, _max, _radius);
            }

            public bool IsValid => _position.HasValue;
            public Vector2 Position => _position.Value;
            public float Diameter { get; }

            private Vector2? GetValidPositionIfExists(HolesSettings settings, List<TestHole> holes, Vector2 min, Vector2 max, float radius)
            {
                for (int i = 0; i <= settings.MaxAttemptsToFindPosition; i++)
                {
                    Vector2? position = GetRandomPosition(min, max);
                    if (CheckPosition(holes, position.Value, radius, settings.MinDistanceBetween))
                        return position;
                }

                return null;
            }

            private bool CheckPosition(List<TestHole> holes, Vector2 position, float radius, float minDistanceBetween)
            {
                foreach (var hole in holes)
                {
                    if (DistanceGreaterMin(position, hole.Position, radius + hole.Radius + minDistanceBetween) == false)
                        return false;
                }

                return true;
            }

            private bool DistanceGreaterMin(Vector2 aPosition, Vector2 bPosition, float min)
            {
                return Mathf.Abs((aPosition - bPosition).sqrMagnitude) >= min * min;
            }

            private Vector2 GetRandomPosition(Vector2 min, Vector2 max)
            {
                return new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
            }
        }
    }
}
