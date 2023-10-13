using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthIndicator : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    private PlayerHealth playerHealth;

    void Awake()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    void Start()
    {
        healthText.color = Color.green;
    }


    void Update()
    {
        healthText.text = ((int)playerHealth.currentHealth).ToString();
        if(playerHealth.currentHealth <= 60 && playerHealth.currentHealth > 30)
        {
            healthText.color = Color.yellow;
        }
        else if (playerHealth.currentHealth <= 30)
        {
            healthText.color = Color.red;
        }
    }
}
