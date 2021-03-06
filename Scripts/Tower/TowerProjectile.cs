using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerProjectile : MonoBehaviour
{

    [SerializeField] float moveSpeed = 5;
    [SerializeField] int bulletDamage = 2;
    [SerializeField] int pierce = 1;

    public Vector3 Target { get; set; }
    
    private int enemiesHit = 0;
    private Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = (Target - transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position += targetPosition * moveSpeed * Time.deltaTime;   
    }

    private void OnBecameInvisible() 
    {
        Destroy(gameObject);
    }

    virtual protected void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Enemy"))
        {
            if(enemiesHit<pierce)
            {
                EnemyHealth enemyHealth = other.GetComponentInParent<EnemyHealth>();
                enemyHealth.TakeDamage(bulletDamage);
                enemiesHit++;
            }
            if(enemiesHit>=pierce)
            {
                Destroy(gameObject);
            }
        }
    }
    
}
