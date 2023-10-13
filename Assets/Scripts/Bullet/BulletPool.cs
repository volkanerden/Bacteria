using System;
using System.Collections;
using UnityEngine;

public class BulletPool : PoolBase<BulletPool>
{
    protected override string PoolName => "Bullet Pool";

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

    private void OnReleased(IPoolable poolable)
    {
        poolable.OnPoolableReleased -= OnReleased;
        ReleaseToPool(poolable);
    }

    public Bullet GetBullet()
    {
        Bullet bullet = GetFromPool<Bullet>();
        bullet.OnPoolableReleased += OnReleased;
        return bullet;
    }
}
