﻿using System;
using UnityEngine;
using UnityEngine.UI;

namespace WhacAMole.UI
{
    public class ChangedButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;
        [SerializeField] private Text _text;
        [SerializeField] private ButtonState _initState;
        [SerializeField] private ButtonState _changedState;

        private bool _inChangedState = false;

        public void Init(Action initAction, Action changedAction)
        {
            _initState.Init(initAction);
            _changedState.Init(changedAction);
            SetState(_initState);
        }

        public void ChangeState()
        {
            if (_inChangedState)
                SetState(_initState);
            else
                SetState(_changedState);
        }

        private void SetState(ButtonState state)
        {
            _image.color = state.ButtonColor;
            _text.color = state.NameColor;
            _text.text = state.Name;
            _inChangedState = state.IsInitState;
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
            [SerializeField] private bool _isInitState;

            private Action _action;

            public Color ButtonColor => _buttonColor;
            public Color NameColor => _nameColor;
            public string Name => _name;
            public bool IsInitState => _isInitState;
            public Action Action => _action;

            public void Init(Action action) => _action = action;
        }
    }
}