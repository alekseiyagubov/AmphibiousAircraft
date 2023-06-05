using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private ProjectilesPool _pool;
    [SerializeField] private Transform _launchPoint;
    [SerializeField] private float _reloadingTime;
    [SerializeField] private float _fireTimeout;
    [SerializeField] private int _ammoPerCartridge;

    private float _timeoutTimer;
    private float _reloadingTimer;
    private int _ammoCounter;

    public bool IsReloading => _reloadingTimer > 0;

    private void Start()
    {
        _ammoCounter = _ammoPerCartridge;
    }

    public void Fire(Vector3 direction)
    {
        if (IsReloading)
        {
            return;
        }
        
        if (_ammoPerCartridge > 0 && _ammoCounter == 0)
        {
            _reloadingTimer = _reloadingTime;
            return;
        }
        
        if (_timeoutTimer > 0)
        {
            return;
        }
        
        var projectile = _pool.Pool.Get();
        projectile.transform.SetPositionAndRotation(_launchPoint.position, _launchPoint.rotation);
        projectile.gameObject.SetActive(true);
        projectile.Launch(direction);

        _timeoutTimer = _fireTimeout;
    }

    private void Update()
    {
        if (_timeoutTimer > 0)
        {
            _timeoutTimer -= _fireTimeout * Time.deltaTime;
        }

        if (_reloadingTimer > 0)
        {
            _reloadingTimer -= _reloadingTime * Time.deltaTime;
        }
    }
}
