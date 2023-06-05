using System;
using UnityEngine;

namespace Gameplay
{
    public class KeyboardInputSystem : MonoBehaviour, IInputSystem
    {
        public Action FireButtonClicked { get; set; }
        
        public float GetHorizontalInput()
        {
            return UnityEngine.Input.GetAxis("Horizontal");
        }

        public float GetVerticalInput()
        {
            return UnityEngine.Input.GetAxis("Vertical");
        }

        private void Update()
        {
            if (UnityEngine.Input.GetMouseButton(0))
            {
                FireButtonClicked?.Invoke();
            }
        }
    }
}