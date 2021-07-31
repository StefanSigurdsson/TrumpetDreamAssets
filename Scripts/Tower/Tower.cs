using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private float attackRange = 3f;
    [SerializeField] float reloadTime = 1f;
    [SerializeField] GameObject projectile;
    [SerializeField] int cost;
 
    public Enemy CurrentEnemyTarget { get; set; }
    public int Cost { get; set; }

    private List<Enemy> _enemies;
    private float _reloadTimer;

    private void Start()
    {
        _enemies = new List<Enemy>();
        _reloadTimer = Time.time;
        Cost = cost;
    }

    private void Update()
    {
        GetCurrentEnemyTarget();
        Shoot();
    }

    private void Shoot()
    {
        // Check if the reload time has passed.
        if(CurrentEnemyTarget && Time.time >= _reloadTimer + reloadTime)
        {
            GetCurrentEnemyTarget();
            GameObject currentProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
            TowerProjectile firedProjectile = currentProjectile.GetComponent<TowerProjectile>();
            firedProjectile.Target = CurrentEnemyTarget;
            _reloadTimer = Time.time;
        }

    }

    private void GetCurrentEnemyTarget()
    {
        if (_enemies.Count <= 0)
        {
            CurrentEnemyTarget = null;
            return;
        }

        CurrentEnemyTarget = GetClosestTarget();
        CurrentEnemyTarget.Color = Color.red;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy newEnemy = other.GetComponent<Enemy>();
            _enemies.Add(newEnemy);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (_enemies.Contains(enemy))
            {
                _enemies.Remove(enemy);
            }
        }
    }

    private Enemy GetClosestTarget()
    {
        float closeDistance = attackRange;
        Enemy closeEnemy = _enemies[0];
        foreach (Enemy enemy in _enemies)
        {
            float enemyDistance = (enemy.transform.position - transform.position).magnitude;
            if (enemyDistance < closeDistance)
            {
                closeEnemy = enemy;
                closeDistance = enemyDistance;
            }
        }

        return closeEnemy;
    }
}
