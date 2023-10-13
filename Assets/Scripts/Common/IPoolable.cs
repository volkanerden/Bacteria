using System;

public interface IPoolable
{
    event Action<IPoolable> OnPoolableReleased;

    void OnPoolableGet();
    void OnPoolableRelease();
    void OnPoolableDestroy();
}