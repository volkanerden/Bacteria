using System;
using UnityEngine;

public class EnemyLife : MonoBehaviour, IPoolable
{
    private Transform player;
    private RageManager rageManager;
    
    [SerializeField] private float disappearDistance = 8f;
    [SerializeField] private float fullHealth = 8f;
    public float currentHealth;

    public event Action<IPoolable> OnPoolableReleased;

    private void OnEnable()
    {
        transform.localScale = new Vector3(0.5f, 0.5f, 0);
        currentHealth = fullHealth;
    }

    void Awake()
    {
        rageManager = FindObjectOfType<RageManager>();
    }

    void Update()
    {
        if(Vector2.Distance(player.position, transform.position) > disappearDistance)
        {
            Disappear();
        }
    }
    
    public void SetPlayer(Transform player)
    {
        this.player = player;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if (collision.gameObject.CompareTag("Bullet"))
            {
                Bullet bullet = collision.gameObject.GetComponent<Bullet>();
                if (bullet != null)
                {
                    DecreaseHealth(bullet.Damage);
                }
            }
        }
    }

    private void DecreaseHealth(float damage)
    {
        if (currentHealth > 0)
        {
            transform.localScale -= new Vector3(0.1f, 0.1f, 0);
            currentHealth -= damage;
        }

        if (currentHealth <= 0)
        {
            rageManager.IncrementKillCount();
            Disappear();
        }
    }

    private void Disappear()
    {
        OnPoolableReleased?.Invoke(this);
    }

    public void OnPoolableGet()
    {
        gameObject.SetActive(true);
    }

    public void OnPoolableRelease()
    {
        gameObject.SetActive(false);
    }

    public void OnPoolableDestroy()
    {
        Destroy(gameObject);
    }

    public void InitializeRageManager(RageManager manager)
    {
        rageManager = manager;
    }
}