using UnityEngine;
using System.Collections;

public class Enemy_health : MonoBehaviour 
{
    [SerializeField]
    private EnemyMaster enemyMaster;
    [SerializeField]
    public int enemyHealth = 100;

    void OnEnable()
    {
        SetInitialReferences();
        enemyMaster.EventEnemyDeductHealth += DeductHealth;
    }

    void OnDisable()
    {
        enemyMaster.EventEnemyDeductHealth -= DeductHealth;
    }

    void SetInitialReferences()
    { 
    
    }

    void DeductHealth(int healthChange)
    {
        enemyHealth -= healthChange;
        if (enemyHealth <= 0)
        {
            enemyHealth = 0;
            enemyMaster.CallEventEnemyDie();
            Destroy(gameObject, Random.Range(10, 20));
        }
    }
}
