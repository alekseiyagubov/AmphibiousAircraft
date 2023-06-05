using HeneGames.Airplane;
using UnityEngine;

namespace Gameplay
{
    public class AircraftController : MonoBehaviour, IDamageable
    {
        [SerializeField] private SignalBus _signalBus;
        
        [SerializeField] private Gun _gun;
        [SerializeField] private AircraftMotor _motor;

        [SerializeField] private Transform _waterPlane;
        [SerializeField] private CameraEffects _cameraEffects;
        
        [SerializeField] private bool _isPlayer;
        [SerializeField] LayerMask _damageableLayerMask;
        [SerializeField] private float _checkEnemyDistance;

        private float _lastYPosition;
        private float _waterPositionY;
        private bool _enemyInTarget;

        private bool IsUnderWater => transform.position.y <= _waterPositionY;

        private void Start()
        {
            _waterPositionY = _waterPlane.position.y;
            _motor.UpdateAtmosphere(IsUnderWater);
            _enemyInTarget = IsEnemyInTarget();
        }
        
        public void Fire()
        {
            _gun.Fire(transform.forward);
        }

        public void MakeDamage()
        {
            gameObject.SetActive(false);
            _signalBus.InvokePlayerKilled(transform.position);
        }

        private void Update()
        {
            CheckForAtmosphereChanging();
            _lastYPosition = transform.position.y;

            if (_isPlayer)
            {
                var enemyInTarget = IsEnemyInTarget();
                if (_enemyInTarget != enemyInTarget)
                {
                    _signalBus.InvokeEnemyInTargetStateChanged(enemyInTarget);
                    _enemyInTarget = enemyInTarget;
                }
            }
        }

        private void CheckForAtmosphereChanging()
        {
            if (IsUnderWater && _lastYPosition >= _waterPositionY)
            {
                UpdateAtmosphere(true);
                return;
            }
            
            if (!IsUnderWater && _lastYPosition <= _waterPositionY)
            {
                UpdateAtmosphere(false);
            }
        }

        private void UpdateAtmosphere(bool isUnderWater)
        {
            _motor.UpdateAtmosphere(isUnderWater);
            _cameraEffects?.SetState(isUnderWater);
        }
        
        private bool IsEnemyInTarget()
        {
            var ray = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(ray, out var hitInfo, _checkEnemyDistance, _damageableLayerMask))
            {
                return true;
            }

            return false;
        }
    }
}