using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace WhacAMole.UI
{
    public class ChangedButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private ButtonState _initState;
        [SerializeField] private ButtonState _changedState;

        private bool _isInitState = false;

        public void Init(Action initAction, Action changedAction)
        {
            _initState.Init(initAction);
            _changedState.Init(changedAction);
            SetState(_initState);
            _isInitState = true;
        }

        public void ChangeState()
        {
            if (_isInitState)
                SetState(_changedState);
            else
                SetState(_initState);

            _isInitState = !_isInitState;
        }

        private void SetState(ButtonState state)
        {
            _image.color = state.ButtonColor;
            _text.color = state.NameColor;
            _text.text = state.Name;
            _button.onClick.RemoveAllListeners();

            if (state.Action != null)
                _button.onClick.AddListener(state.Action.Invoke);
        }

        [Serializable]
        private struct ButtonState
        {
            [SerializeField] private Color _buttonColor;
            [SerializeField] private Color _nameColor;
            [SerializeField] private string _name;

            private Action _action;

            public Color ButtonColor => _buttonColor;
            public Color NameColor => _nameColor;
            public string Name => _name;
            public Action Action => _action;

            public void Init(Action action) => _action = action;
        }
    }
}
