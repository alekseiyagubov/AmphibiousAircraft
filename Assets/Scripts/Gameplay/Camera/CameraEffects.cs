using UnityEngine;

namespace HeneGames.Airplane
{
    public class CameraEffects : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private float _cameraDefaultFov = 60f;
        [SerializeField] private float _cameraUnderWaterFov = 40f;
        [SerializeField] private float _fovChangingSpeed = 100.0f;

        private bool _isUnderWaterState;

        public void SetState(bool isUnderWater)
        {
            _isUnderWaterState = isUnderWater;
        }
        
        private void Update()
        {
            CameraFovUpdate();
        }

        private void CameraFovUpdate()
        {
            var targetFov = _isUnderWaterState ? _cameraUnderWaterFov : _cameraDefaultFov;
            ChangeCameraFov(targetFov);
        }

        private void ChangeCameraFov(float fov)
        {
            var deltatime = Time.deltaTime * _fovChangingSpeed;
            _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, fov, deltatime);
        }
    }
}