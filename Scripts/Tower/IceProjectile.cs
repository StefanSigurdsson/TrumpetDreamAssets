using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceProjectile : TowerProjectile
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        Enemy enemyHit = other.GetComponent<Enemy>();
        enemyHit.MoveSpeed *= 0.5f;
        other.GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.cyan);
    }
}
