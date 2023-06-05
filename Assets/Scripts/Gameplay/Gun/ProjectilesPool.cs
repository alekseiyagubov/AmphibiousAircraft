using UnityEngine;
using UnityEngine.Pool;

public class ProjectilesPool : MonoBehaviour
{
    public Projectile poolablePrefab;

    public int defaultCapacity = 50;
    public int maxPoolSize = 50;

    public IObjectPool<Projectile> Pool { get; private set; }

    private void Awake()
    {
        Pool = new ObjectPool<Projectile>(CreatePooledItem, null, OnReturnedToPool, 
            OnDestroyPoolObject, true, defaultCapacity, maxPoolSize);
    }

    private Projectile CreatePooledItem()
    {
        var instance = Instantiate(poolablePrefab.gameObject, transform, true);
        var projectile = instance.GetComponent<Projectile>();
        projectile.Initialize(Pool);
        return projectile;
    }

    private void OnReturnedToPool(Projectile obj) => obj.gameObject.SetActive(false);

    private void OnDestroyPoolObject(Projectile obj) => Destroy(obj);
}
