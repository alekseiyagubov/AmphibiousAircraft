using UnityEngine;

namespace Gameplay
{
    public class AircraftMotor : MonoBehaviour
    {
        [SerializeField] private AtmosphereSettings _airSettings;
        [SerializeField] private AtmosphereSettings _waterSettings;

        [SerializeField] private float _inertiaFactor;
        [SerializeField] private float _wingsAngleOfAttack;

        [SerializeField] private float _liftCoef;
        [SerializeField] private float _dragCoef;
        [SerializeField] private float _gravityCoef;

        private float _speed;
        private float _pressure;
        private Vector3 _thrust;

        private void Update()
        {
            UpdateForces();
        }

        private void UpdateForces()
        {
            var lastThrust = _thrust;
            _thrust = transform.forward * _speed;

            var localVelocity = transform.InverseTransformDirection(lastThrust);
            var angleOfAttack = (Mathf.Atan2(-localVelocity.y, localVelocity.z) + _wingsAngleOfAttack * Mathf.Deg2Rad) * Mathf.PI;

            var dragDirection = _thrust.normalized;
            var liftDirection = Vector3.Cross(dragDirection, transform.right);
            var inducedLift = angleOfAttack * _liftCoef;
            var inducedDrag = angleOfAttack * _dragCoef;
            var pressure = _thrust.magnitude * _pressure;

            inducedLift = Mathf.Clamp(inducedLift, -_speed, _speed);
            inducedDrag = Mathf.Clamp(inducedDrag, -_speed, _speed);
            pressure = Mathf.Clamp(pressure, -_speed, _speed);

            var lift = inducedLift * pressure * liftDirection;
            var drag = inducedDrag * pressure * dragDirection;
            var gravity = 9.8f * Vector3.down * _gravityCoef;

            var newThrust = Vector3.Lerp(lastThrust, _thrust, _inertiaFactor * Time.deltaTime);
            var velocity = (newThrust + lift + drag + gravity) * Time.deltaTime;
            transform.position += velocity;
            _thrust = newThrust;
        }

        public void UpdateAtmosphere(bool isUnderWater)
        {
            var settings = isUnderWater ? _waterSettings : _airSettings;
            _speed = settings.Speed;
            _pressure = settings.Pressure;
        }
    }

    [System.Serializable]
    public class AtmosphereSettings
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _pressure;

        public float Speed => _speed;
        public float Pressure => _pressure;
    }
}