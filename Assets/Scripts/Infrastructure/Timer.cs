using System;
using System.Collections;
using UnityEngine;

namespace WhacAMole.Infrastructure
{
    public class Timer : MonoBehaviour
    {
        private Coroutine _timerRoutine;
        private float _pastTime;

        public void StartOff(float duration, Action onEnd = null)
        {
            StopCoroutine();
            _timerRoutine = StartCoroutine(Run(duration, onEnd));
        }

        public float Stop()
        {
            StopCoroutine();
            return _pastTime;
        }

        private void StopCoroutine()
        {
            if (_timerRoutine != null)
                StopCoroutine(_timerRoutine);
        }

        private IEnumerator Run(float duration, Action onEnd)
        {
            _pastTime = 0.0f;

            while (_pastTime < duration)
            {
                _pastTime += Time.deltaTime;
                yield return null;
            }

            onEnd?.Invoke();
        }
    }
}
