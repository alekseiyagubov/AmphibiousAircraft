using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector2 _offset;
    [SerializeField] private float _lerpPositionCoef;

    private Vector3 _targetDirection;
    private Vector3 _smoothDampSpeed;

    private Vector3 _targetForward;
    private Vector3 _targetPosition;

    private Vector3 _speed;

    private void Start()
    {
        _targetDirection = _target.forward;
        _targetForward = _target.forward;
        _targetPosition = _target.position;

        var targetPoint = _target.position + _targetDirection * 50;
        var cameraPosition = targetPoint - _offset.x * _targetDirection;
        cameraPosition = new Vector3(cameraPosition.x, cameraPosition.y + _offset.y, cameraPosition.z);
        
        transform.position = cameraPosition;
        transform.rotation = Quaternion.LookRotation(targetPoint - transform.position);
    }

    private void LateUpdate()
    {
        _targetForward = _target.forward;
        _targetPosition = _target.position;
        UpdateCamera();
    }

    private void UpdateCamera()
    {
        _targetDirection = Vector3.Lerp(_targetDirection, _targetForward, _lerpPositionCoef * Time.deltaTime);
        
        var targetPoint = _targetPosition + _targetForward * 50;
        var cameraPosition = _targetPosition - _offset.x * _targetDirection;
        cameraPosition = new Vector3(cameraPosition.x, cameraPosition.y + _offset.y, cameraPosition.z);
        transform.position = cameraPosition;
        
        var targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
        transform.rotation = targetRotation;
    }
}