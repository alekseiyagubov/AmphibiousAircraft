using System;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Input
{
    public class MobileInputSystem : MonoBehaviour, IInputSystem
    {
        [SerializeField] private Joystick _joystick;
        [SerializeField] private Button _fireButton;

        public Action FireButtonClicked { get; set; }

        private void OnEnable()
        {
            _fireButton.onClick.AddListener(OnFireButtonClicked);
        }

        public float GetHorizontalInput()
        {
            return _joystick.Horizontal;
        }

        public float GetVerticalInput()
        {
            return _joystick.Vertical;
        }

        private void OnFireButtonClicked()
        {
            FireButtonClicked?.Invoke();
        }

        private void OnDisable()
        {
            _fireButton.onClick.RemoveListener(OnFireButtonClicked);
        }
    }
}