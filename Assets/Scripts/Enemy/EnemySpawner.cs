using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private float spawnInterval = 10.0f;
    float nextSpawnTime = 0.0f;

    [SerializeField]
    private Transform player;

    private RageManager rageManager;

    private void Start()
    {
        rageManager = FindObjectOfType<RageManager>();
    }

    void Update()
    {
        if (nextSpawnTime < Time.time)
        {
            Spawn();

            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void Spawn()
    {
        GameObject enemy = EnemyPool.SharedInstance.GetEnemy();

        if (enemy != null)
        {
            enemy.transform.position = transform.position;
            enemy.transform.rotation = transform.rotation;
            enemy.SetActive(true);

            EnemyLife enemyLife = enemy.GetComponent<EnemyLife>();
            if (enemyLife != null)
            {
                enemyLife.SetPlayer(player);
                enemyLife.InitializeRageManager(rageManager);
            }
        }
    }


}
