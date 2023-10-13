using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private float maxHealth = 100.0f;
    public float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if(currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            currentHealth -= .5f;
        }
    }
}