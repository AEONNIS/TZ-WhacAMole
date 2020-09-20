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
        [SerializeField] private HoleSettings _holeSettings;
        private List<TestHole> _holes = new List<TestHole>();

        private float MinX => _holeSettings.Radius + _holeSettings.MinEdgeDistance;
        private float MaxX => _baseScaler.referenceResolution.x - _holeSettings.Radius - _holeSettings.MinEdgeDistance;
        private float MinY => -_holeSettings.Radius - _holeSettings.MinEdgeDistance;
        private float MaxY => -_baseScaler.referenceResolution.y + _holeSettings.Radius + _holeSettings.MinEdgeDistance;
        private Vector2 RandomPosition => new Vector2(Random.Range(MinX, MaxX), Random.Range(MinY, MaxY));

        #region Unity
        private void Awake()
        {
            Creates(_holeSettings.Count);
        }
        #endregion

        private void Creates(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Vector2 position = RandomPosition;

                for (int a = 0; a <= _holeSettings.MaxAttemptsToFindPosition; a++)
                {
                    if (CheckWithAllPositions(position))
                    {
                        _holes.Add(Create(position));
                        break;
                    }
                    else
                    {
                        position = RandomPosition;
                    }

                    if (a == _holeSettings.MaxAttemptsToFindPosition)
                    {
                        Debug.LogWarning($"({_holes.Count}: ){ _holeSettings.CreationImpossibilityMessage}");
                        return;
                    }
                }
            }
        }

        private TestHole Create(Vector2 position)
        {
            TestHole hole = Instantiate(_holeTemplate, _ground);
            hole.Init(position, _holeSettings.Diameter);
            return hole;
        }

        private bool CheckWithAllPositions(Vector2 position)
        {
            foreach (var hole in _holes)
            {
                if (CheckPosition(hole.Position, position) == false)
                    return false;
            }

            return true;
        }

        private bool CheckPosition(Vector2 aPosition, Vector2 bPosition)
        {
            return Mathf.Abs((aPosition - bPosition).sqrMagnitude) >= _holeSettings.MinCentersDistance * _holeSettings.MinCentersDistance;
        }

        [Serializable]
        private class HoleSettings
        {
            [Range(0, 100)]
            [SerializeField] private int _count = 30;
            [SerializeField] private float _diameter = 100f;
            [SerializeField] private float _minEdgeDistance = 10f;
            [SerializeField] private float _minDistanceBetween = 10f;
            [SerializeField] private int _maxAttemptsToFindPosition = 100;
            [TextArea(1, 3)]
            [SerializeField] private string _creationImpossibilityMessage = "it is impossible to create a hole with the given settings";

            public int Count => _count;
            public float Radius => _diameter * 0.5f;
            public float Diameter => _diameter;
            public float MinEdgeDistance => _minEdgeDistance;
            public float MinCentersDistance => _diameter + _minDistanceBetween;
            public int MaxAttemptsToFindPosition => _maxAttemptsToFindPosition;
            public string CreationImpossibilityMessage => _creationImpossibilityMessage;
        }
    }
}
