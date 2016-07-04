using UnityEngine;
using System.Collections;

public class Enemy_TakeDamage : MonoBehaviour 
{
    //a mettre sur la hitbox de l'enemy

    [SerializeField]
    private EnemyMaster enemyMaster;
    [SerializeField]
    public int damageMultiplier = 1;
    [SerializeField]
    public bool shouldRemoveCollider;
    [SerializeField]
    private Collider myCollider;
    [SerializeField]
    private Rigidbody myRigid;

    void OnEnable()
    {
        SetInitialReferences();
        enemyMaster.EventEnemyDie += RemoveThis;
    }

    void OnDisable()
    {
        enemyMaster.EventEnemyDie -= RemoveThis;
    }

    void SetInitialReferences()
    { 
    
    }

    public void ProcessDamage(int damage)
    {
        int damageToApply = damage * damageMultiplier;
        enemyMaster.CallEventEnemyDeductHealth(damageToApply);
    }

    void RemoveThis()
    {
        if (shouldRemoveCollider)
        {
            if (myCollider != null)
            {
                Destroy(myCollider);
            }

            if (myRigid != null)
            {
                Destroy(myRigid);
            }
            Destroy(this);
        }
    }
}
