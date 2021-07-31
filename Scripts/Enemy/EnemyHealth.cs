using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] int maxHealth;

    public int CurrentHealth { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damageAmount)
    {
        CurrentHealth -= damageAmount;
        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
