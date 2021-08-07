using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTower : Tower
{
    
    [SerializeField] protected List<Vector3> firePoints = new List<Vector3>();

    protected override void Shoot()
    {
        if (CurrentEnemyTarget && Time.time >= _reloadTimer + reloadTime)
        {
            for (int i = 0; i < firePoints.Count; i++)
            {
                GameObject currentProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
                TowerProjectile firedProjectile = currentProjectile.GetComponent<TowerProjectile>();
                firedProjectile.Target = transform.position - firePoints[i];
                _reloadTimer = Time.time;
            }
        }
    }

}
