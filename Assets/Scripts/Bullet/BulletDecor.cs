using UnityEngine;

public interface IBullet
{
    void Fire(Vector3 origin, Vector3 direction);
    void FireInterval(float fireInterval);
    void SetColor(Color color);
    float Damage { get; }   

}

public class DefaultBullet : IBullet
{
    private Color bulletColor;
    private float fireInterval;
    public float Damage => .5f;

    public void Fire(Vector3 origin, Vector3 direction)
    {
        IBullet bullet = BulletPool.SharedInstance.GetBullet();

        if (bullet != null)
        {
            bullet.Fire(origin, direction);
        }
    }
    public void SetColor(Color color)
    {
        bulletColor = color;
    }
    public void FireInterval(float interval)
    {
        fireInterval = interval;
    } 
}

public abstract class BulletDecorator : IBullet
{
    protected IBullet decoratedBullet;

    public BulletDecorator(IBullet bullet)
    {
        decoratedBullet = bullet;
    }
    public virtual float Damage => decoratedBullet.Damage;

    public virtual void Fire(Vector3 origin, Vector3 direction)
    {
        decoratedBullet.Fire(origin, direction);
    }
    public virtual void SetColor(Color color)
    {
        decoratedBullet.SetColor(color);
    }
    public virtual void FireInterval(float interval)
    {
        decoratedBullet.FireInterval(interval);
    }
}

public class RageBullet : BulletDecorator
{
    private readonly float rageFireInterval;
    private float nextFireTime;
    private Color rageColor;

    public override float Damage => float.MaxValue;

    public RageBullet(IBullet bullet, float rageInterval, Color color) : base(bullet)
    {
        rageFireInterval = rageInterval;
        rageColor = color;
    }

    public override void Fire(Vector3 origin, Vector3 direction)
    {
        if (Time.time >= nextFireTime)
        {
            base.SetColor(rageColor);
            base.Fire(origin, direction);
            base.FireInterval(rageFireInterval);
            nextFireTime = Time.time + rageFireInterval;
        }

    }
}