using System;
using UnityEngine;

public class Bullet : MonoBehaviour, IPoolable, IBullet
{
    float speed = 4f;
    public float damage = 2f;
    
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    private Fire fire;

    public event Action<IPoolable> OnPoolableReleased;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        fire = FindObjectOfType<Fire>();
    }

    private void OnDisable()
    {
        rb.velocity = Vector2.zero;

        StopAllCoroutines();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            ReleaseBullet();
        }
    }

    public void OnPoolableGet()
    {
        gameObject.SetActive(true);

        Invoke(nameof(ReleaseBullet), 5f);
    }

    private void ReleaseBullet()
    {
        OnPoolableReleased?.Invoke(this);
    }

    public void OnPoolableRelease()
    {
        gameObject.SetActive(false);
    }

    public void OnPoolableDestroy()
    {
        Destroy(gameObject);
    }

    public void Fire(Vector3 origin, Vector3 direction)
    {
        transform.position = origin;
        transform.right = direction;
        rb.velocity = direction * speed;
    }
    public void FireInterval(float fireInterval)
    {
        fire.fireInterval = fireInterval;
    }

    public void SetColor(Color color)
    {
        spriteRenderer.color = color;
    }

    public float Damage
    {
        get { return damage; }
    }

}
