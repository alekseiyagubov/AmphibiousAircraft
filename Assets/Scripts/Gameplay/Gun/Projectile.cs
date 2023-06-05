using Gameplay;
using UnityEngine;
using UnityEngine.Pool;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime;
    [SerializeField] LayerMask _damageableLayerMask;
    [SerializeField] private float _raycastDistance;

    private IObjectPool<Projectile> _pool;
    
    private Vector3 _direction;
    private float _lifeTimer;

    public void Initialize(IObjectPool<Projectile> pool)
    {
        _pool = pool;
    }
    
    public void Launch(Vector3 direction)
    {
        _direction = direction.normalized;
        _lifeTimer = _lifeTime;
    }

    private void Update()
    {
        if (_lifeTimer <= 0)
        {
            Release();
            return;
        }

        if (CheckForCollision())
        {
            Release();
            return;
        }
        
        Move();
        _lifeTimer -= Time.deltaTime;
    }

    private bool CheckForCollision()
    {
        var ray = new Ray(transform.position, _direction.normalized);
        if (Physics.Raycast(ray, out var hitInfo, _raycastDistance, _damageableLayerMask))
        {
            var damageableComponent = hitInfo.transform.GetComponentInParent<IDamageable>();
            if (damageableComponent != null)
            {
                damageableComponent.MakeDamage();
                return true;
            }
        }

        return false;
    }
    
    private void Move()
    {
        transform.position += _direction * _speed * Time.deltaTime;
    }

    private void Release()
    {
        _pool.Release(this);
    }
}
