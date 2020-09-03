using System;
using UnityEngine;
using UnityEngine.UI;
using WhacAMole.Infrastructure;

namespace WhacAMole.UI
{
    public class CounterIndicator : MonoBehaviour
    {
        [SerializeField] private Counter _counter;
        [SerializeField] private Button _decreaseButton;
        [SerializeField] private Button _increaseButton;
        [SerializeField] private Text _indicator;

        public event Action Decreased
        {
            add => _counter.Decreased += value;
            remove => _counter.Decreased -= value;
        }

        public event Action Increased
        {
            add => _counter.Increased += value;
            remove => _counter.Decreased -= value;
        }

        public int CounterValue => _counter.Value;

        #region Unity
        private void OnEnable()
        {
            _decreaseButton.onClick.AddListener(Decrease);
            _increaseButton.onClick.AddListener(Increase);
        }

        private void OnDisable()
        {
            _decreaseButton.onClick.RemoveListener(Decrease);
            _decreaseButton.onClick.RemoveListener(Increase);
        }
        #endregion

        public void Init() => SetIndicator(_counter.Value);

        private void Decrease() => SetIndicator(_counter.Decrease());

        private void Increase() => SetIndicator(_counter.Increase());

        private void SetIndicator(int value) => _indicator.text = value.ToString();
    }
}
