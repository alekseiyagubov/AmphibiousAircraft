using UnityEngine;

namespace Gameplay
{
    public class AircraftBotRotation : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _freeRotationSpeed;
        [SerializeField] private Transform _planeBody;

        private Transform _target;
        private Vector3 _targetVector;
        private bool _isInFreeRotationState;
        
        public void SetTarget(Transform target)
        {
            _target = target;
        }

        public void SetFreeRotationState(bool state)
        {
            _isInFreeRotationState = state;
        }

        private void Update()
        {
            if (_target.gameObject.activeSelf)
            {
                _targetVector = _target.position - transform.position;
            }
            else
            {
                _targetVector = transform.forward;
            }
            
            var targetRotation = Quaternion.LookRotation(_targetVector);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

            if (_isInFreeRotationState)
            {
                _planeBody.Rotate(0, 0, _freeRotationSpeed * Time.deltaTime, Space.Self);
            }
        }
    }
}