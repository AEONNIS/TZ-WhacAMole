using System;
using UnityEngine;
using UnityEngine.UI;

namespace WhacAMole.UI
{
    public class CounterIndicator : MonoBehaviour
    {
        [SerializeField] private Button _decreaseCounterButton;
        [SerializeField] private Button _increaseCounterButton;
        [SerializeField] private Text _counter;

        private Action _decreaseAction;
        private Action _increaseAction;
        private int _value = 0;

        #region Unity
        private void OnEnable()
        {
            _decreaseCounterButton.onClick.AddListener(Decrease);
            _increaseCounterButton.onClick.AddListener(Increase);
        }

        private void OnDisable()
        {
            _decreaseCounterButton.onClick.RemoveListener(Decrease);
            _decreaseCounterButton.onClick.RemoveListener(Increase);
        }
        #endregion

        public void Init(int value, Action decreaseAction = null, Action increaseAction = null)
        {
            SetCounter(value);
            _decreaseAction = decreaseAction;
            _increaseAction = increaseAction;
        }

        private void Decrease()
        {
            SetCounter(--_value);
            _decreaseAction?.Invoke();
        }

        private void Increase()
        {
            SetCounter(++_value);
            _increaseAction?.Invoke();
        }

        private void SetCounter(int value)
        {
            _value = value;
            _counter.text = value.ToString();
        }
    }
}
