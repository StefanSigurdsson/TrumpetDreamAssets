using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] int maxHealth;
    [SerializeField] int moneyOnDeath;

    public int CurrentHealth { get; set; }
    public int MoneyOnDeath { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = maxHealth;
        MoneyOnDeath = moneyOnDeath;
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
            Die();
        }
    }

    private void Die()
    {
        if(gameObject) {Destroy(gameObject);}
        LevelManager.Instance.CurrentMoney += MoneyOnDeath;
        LevelManager.Instance.UpdateMoneyText();
    }
}
