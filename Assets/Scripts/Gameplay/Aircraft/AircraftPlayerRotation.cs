using UnityEngine;

namespace Gameplay
{
    public class AircraftPlayerRotation : PlayerAircraftRotationBase
    {
        [SerializeField] private float _pitchSpeed;
        [SerializeField] private float _rollSpeed;
        
        public override void SetInput(float inputHorizontal, float inputVertical)
        {
            var pitch = inputVertical * _pitchSpeed * Time.deltaTime;
            var roll = -inputHorizontal * _rollSpeed * Time.deltaTime;
        
            var rotation = transform.rotation * Quaternion.Euler(pitch, 0, roll);
            transform.rotation = rotation;
        }
    }
}