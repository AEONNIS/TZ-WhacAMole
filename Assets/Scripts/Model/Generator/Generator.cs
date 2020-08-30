using System;
using UnityEngine;
using WhacAMole.Infrastructure;
using Random = UnityEngine.Random;

namespace WhacAMole.Model
{
    public partial class Generator : MonoBehaviour
    {
        [SerializeField] private ChangingRange _spawnRate;
        [SerializeField] private ChangingRange _residenceTime;
        [SerializeField] private AccelerationRange _accelerationRange;
        [SerializeField] private Timer _timer;

        private int _currentImpulse = 0;
        private int _acceleratingImpulse;

        public event Action<float> Impulse;

        public void Init()
        {
            _spawnRate.Init();
            _residenceTime.Init();
        }

        public void Run()
        {
            _acceleratingImpulse = Random.Range(_accelerationRange.MinImpulses, _accelerationRange.MaxImpulses);
            SendImpulse();
        }

        public void Stop()
        {
            _timer.Stop();
            _currentImpulse = 0;
            _spawnRate.Reset();
            _residenceTime.Reset();
        }

        private void Accelerate()
        {
            _currentImpulse = 0;
            _acceleratingImpulse = Random.Range(_accelerationRange.MinImpulses, _accelerationRange.MaxImpulses);
            _spawnRate.Accelerate();
            _residenceTime.Accelerate();
        }

        private void SendImpulse()
        {
            (float minRate, float maxRate) = _spawnRate.CurrentRange;
            _timer.StartOff(Random.Range(minRate, maxRate), SendImpulse);

            (float minTime, float maxTime) = _residenceTime.CurrentRange;
            Impulse?.Invoke(Random.Range(minRate, maxRate));
            _currentImpulse++;

            if (_currentImpulse == _acceleratingImpulse)
                Accelerate();
        }
    }
}
