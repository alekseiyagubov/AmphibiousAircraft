using System;

namespace Gameplay
{
    public interface IInputSystem
    {
        public Action FireButtonClicked { get; set; }
        public float GetHorizontalInput();
        public float GetVerticalInput();
    }
}