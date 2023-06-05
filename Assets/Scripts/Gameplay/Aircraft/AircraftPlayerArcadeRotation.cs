using UnityEngine;

namespace Gameplay
{
    public class AircraftPlayerArcadeRotation : PlayerAircraftRotationBase
    {
        [SerializeField] private float _pitchSpeed;
        [SerializeField] private float _yawSpeed;
        
        public override void SetInput(float inputHorizontal, float inputVertical)
        {
            transform.Rotate(0,Time.deltaTime * _yawSpeed * inputHorizontal, 0, Space.Self);
            transform.Rotate( Time.deltaTime * _pitchSpeed * inputVertical, 0, 0, Space.Self);
        }
    }
}