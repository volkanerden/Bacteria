using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private float rotationSpeed = 0.001f;
    private Transform target;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        if (target == null)
        {
            GetTarget();
        }
        else
        {
            Rotate();
        }

        rb.velocity = transform.right * speed;
    }
    private void Rotate()
    {
        Vector2 targetDirection = target.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.Euler(0, 0, angle);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, q, rotationSpeed);
        
    }

    private void GetTarget()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
}