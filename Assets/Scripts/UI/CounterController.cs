using UnityEngine;
using UnityEngine.UI;
using WhacAMole.Infrastructure;

namespace WhacAMole.UI
{
    public class CounterController : MonoBehaviour
    {
        [SerializeField] private Button _decreaseButton;
        [SerializeField] private Button _increaseButton;
        [SerializeField] private Text _indicator;

        private Counter _counter;

        #region Unity
        private void OnEnable()
        {
            _decreaseButton.onClick.AddListener(Decrease);
            _increaseButton.onClick.AddListener(Increase);
        }

        private void OnDisable()
        {
            _decreaseButton.onClick.RemoveListener(Decrease);
            _increaseButton.onClick.RemoveListener(Increase);
        }
        #endregion

        public void Init(Counter counter)
        {
            _counter = counter;
            SetIndicator(counter.Value);
        }

        private void Decrease() => SetIndicator(_counter.Decrease());

        private void Increase() => SetIndicator(_counter.Increase());

        private void SetIndicator(int value) => _indicator.text = value.ToString();
    }
}
