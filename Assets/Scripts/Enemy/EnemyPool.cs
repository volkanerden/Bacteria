using UnityEngine;

public class EnemyPool : PoolBase<EnemyPool>
{
    public RageManager rageManager;
    protected override string PoolName => "Enemy Pool";

    protected override IPoolable CreateFunc()
    {
        return Instantiate(prefab, poolParent).GetComponent<IPoolable>();
    }

    protected override void GetFunc(IPoolable poolable)
    {
        poolable.OnPoolableGet();
    }

    protected override void ReleaseFunc(IPoolable poolable)
    {
        poolable.OnPoolableRelease();
    }

    protected override void DestroyFunc(IPoolable poolable)
    {
        poolable.OnPoolableReleased -= OnReleased;
        poolable.OnPoolableDestroy();
    }

    public GameObject GetEnemy()
    {
        EnemyLife enemy = GetFromPool<EnemyLife>();

        enemy.InitializeRageManager(rageManager);

        enemy.OnPoolableReleased += OnReleased;
        return enemy.gameObject;
    }

    private void OnReleased(IPoolable poolable)
    {
        poolable.OnPoolableReleased -= OnReleased;
        ReleaseToPool(poolable);
    }


}
