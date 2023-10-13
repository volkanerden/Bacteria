using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public abstract class PoolBase<T> : MonoBehaviour
    where T : PoolBase<T>
{
    protected abstract string PoolName { get; }

    [SerializeField]
    protected GameObject prefab;

    [SerializeField]
    private int poolAmount;

    public static T SharedInstance { get; private set; }

    private IObjectPool<IPoolable> pool;
    //private Queue<IPoolable> pool;
    protected Transform poolParent;

    void Awake()
    {
        poolParent = new GameObject(PoolName).transform;
        pool = new ObjectPool<IPoolable>(CreateFunc, GetFunc, ReleaseFunc, DestroyFunc, true, poolAmount);
        //pool = new Queue<IPoolable>();

        if (SharedInstance == null)
        {
            SharedInstance = this as T;
        }
    }
    private void OnDestroy()
    {
        //foreach(var item in pool)
        //{
        //    DestroyFunc(item);
        //}
        pool.Clear();
    }

    protected abstract IPoolable CreateFunc();

    protected abstract void GetFunc(IPoolable poolable);

    protected abstract void ReleaseFunc(IPoolable poolable);

    protected abstract void DestroyFunc(IPoolable poolable);

    protected T GetFromPool<T>() where T : IPoolable => (T)pool.Get();

    //protected T GetFromPool<T>() where T : IPoolable
    //{
    //    IPoolable poolable;
    //    if (pool.Count > 0)
    //    {
    //        poolable = pool.Dequeue();
    //    }
    //    else
    //    {

    //        poolable = (T)CreateFunc();
    //    }
    //    GetFunc(poolable);
    //    return (T)poolable;
    //}

    protected void ReleaseToPool(IPoolable poolable) => pool.Release(poolable);
    //protected void ReleaseToPool(IPoolable poolable)
    //{
    //    ReleaseFunc(poolable);
    //    pool.Enqueue(poolable);
    //}
}
