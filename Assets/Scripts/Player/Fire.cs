using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public float fireInterval;
    private float defaultFireInterval = 1.0f;
    private float rageFireInterval = 0.2f;
    private float nextFireTime = 0.0f;
    private bool isFiring = true;

    public event Action<bool> OnRageModeChanged;

    public RageManager rageManager;

    void Start()
    {
        fireInterval = defaultFireInterval;
    }

    void Update()
    {
        if (isFiring)
        {
            if (nextFireTime < Time.time)
            {
                Shoot();
                nextFireTime = Time.time + fireInterval;
            }
        }
    }

    public void StartFiring()
    {
        isFiring = true;
    }

    public void StopFiring()
    {
        isFiring = false;
    }

    void Shoot()
    {
        IBullet bullet = BulletPool.SharedInstance.GetBullet();

        if (bullet != null)
        {
            if (rageManager.isRageMode)
            {
                bullet = new RageBullet(bullet, rageFireInterval, Color.black);
                bullet.SetColor(Color.black);
            }
            else
            {
                bullet.SetColor(Color.red);
                bullet.FireInterval(defaultFireInterval);
            }
            bullet.Fire(transform.position + transform.right, transform.right);
        }
    }

    public void InvokeRageModeChanged(bool isRageMode)
    {
        OnRageModeChanged?.Invoke(isRageMode);
    }
}