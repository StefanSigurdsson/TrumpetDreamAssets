using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Enemy"))
        {
            LevelManager.Instance.LoseLife(1);
            Destroy(other);
        }
    }
}
