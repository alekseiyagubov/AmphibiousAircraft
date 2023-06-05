using UnityEngine;

namespace Gameplay
{
    public class PlayerInputHandler : MonoBehaviour
    {
        [SerializeField] private AircraftController _aircraft;
        [SerializeField] private PlayerAircraftRotationBase _aircraftPlayerRotation;
        [SerializeField] private GameObject _mobileInput;
        [SerializeField] private bool _useJoystick;

        private IInputSystem _inputSystem;

        private void Awake()
        {
            if (_useJoystick)
            {
                _mobileInput.SetActive(true);
                _inputSystem = _mobileInput.GetComponent<IInputSystem>();
            }
            else
            {
                _mobileInput.SetActive(false);
                _inputSystem = gameObject.AddComponent<KeyboardInputSystem>();
            }
        }

        private void OnEnable()
        {
            _inputSystem.FireButtonClicked = OnFireButtonClick;
        }

        private void Update()
        {
            var horizontalInput = _inputSystem.GetHorizontalInput();
            var verticalInput = _inputSystem.GetVerticalInput();
            _aircraftPlayerRotation.SetInput(horizontalInput, verticalInput);
        }

        private void OnFireButtonClick()
        {
            _aircraft.Fire();
        }
        
        private void OnDisable()
        {
            _inputSystem.FireButtonClicked = null;
        }
    }
}