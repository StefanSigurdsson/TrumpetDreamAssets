using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    [SerializeField] public TowerInfo towerInfo;
    [SerializeField] protected float reloadTime = 1f;
    [SerializeField] protected GameObject projectile;
 
    public Enemy CurrentEnemyTarget { get; set; }
    public int Cost { get; set; }
    public float AttackRange { get; set; }

    protected List<Enemy> _enemies;
    protected float _reloadTimer;

    private void Start()
    {
        _enemies = new List<Enemy>();
        _reloadTimer = Time.time;
        Cost = towerInfo.TowerCost;
        AttackRange = towerInfo.AttackRange;
    }

    private void Update()
    {
        GetCurrentEnemyTarget();
        Shoot();
    }

    protected virtual void Shoot()
    {
        // Check if the reload time has passed.
        if(CurrentEnemyTarget && Time.time >= _reloadTimer + reloadTime)
        {
            GetCurrentEnemyTarget(); //Maybe redundant?
            GameObject currentProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
            TowerProjectile firedProjectile = currentProjectile.GetComponent<TowerProjectile>();
            firedProjectile.Target = CurrentEnemyTarget.transform.position;
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
        float closeDistance = AttackRange;
        Enemy closeEnemy = _enemies[0];
        foreach (Enemy enemy in _enemies)
        {
            float enemyDistance = (enemy.transform.position - transform.position).magnitude;
            if ((enemyDistance < closeDistance) && enemy)
            {
                closeEnemy = enemy;
                closeDistance = enemyDistance;
            }
        }

        return closeEnemy;
    }
}
