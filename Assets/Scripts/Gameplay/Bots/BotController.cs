using UnityEngine;

namespace Gameplay
{
    public class BotController : MonoBehaviour
    {
        [SerializeField] private AircraftController _aircraft;
        [SerializeField] private AircraftBotRotation _aircraftRotation;
        
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private float _firingDistance;
        
        [SerializeField] private SignalBus _signalBus;

        [SerializeField] private Target _target;
        [SerializeField] private Target _spottedTarget;

        private bool _isRoundFinished;

        private void OnEnable()
        {
            _signalBus.EnemyInTargetStateChanged += OnEnemyInTargetStateChanged;
            _signalBus.PlayerKilled += OnPlayerKilled;
        }

        private void Start()
        {
            _aircraftRotation.SetTarget(_playerTransform);
        }

        private void Update()
        {
            if (_isRoundFinished)
            {
                _aircraftRotation.SetFreeRotationState(true);
                return;
            }

            var distanceToPlayer = Vector3.Distance(transform.position, _playerTransform.position);
            if (!_isRoundFinished && distanceToPlayer  <= _firingDistance)
            {
                _aircraft.Fire();
            }

            var angleToPlayer = Vector3.Angle(transform.forward, _playerTransform.forward);
            if (distanceToPlayer < 100 && angleToPlayer > 160 && angleToPlayer < 200)
            {
                _aircraftRotation.SetFreeRotationState(true);
                return;
            }
            
            _aircraftRotation.SetFreeRotationState(false);
        }

        private void OnEnemyInTargetStateChanged(bool state)
        {
            _target.enabled = !state;
            _spottedTarget.enabled = state;
        }

        private void OnPlayerKilled(Vector3 killPosition)
        {
            _isRoundFinished = true;
        }

        private void OnDisable()
        {
            _signalBus.EnemyInTargetStateChanged -= OnEnemyInTargetStateChanged;
            _signalBus.PlayerKilled -= OnPlayerKilled;
        }
    }
}