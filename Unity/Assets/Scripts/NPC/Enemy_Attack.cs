using UnityEngine;
using System.Collections;

public class Enemy_Attack : MonoBehaviour 
{
    [SerializeField]
    private EnemyMaster enemyMaster;
    [SerializeField]
    private Transform attackTarget;
    [SerializeField]
    private Transform myTransform;
    [SerializeField]
    public float attackRate = 1;
    [SerializeField]
    private float nextAttack;
    [SerializeField]
    public float attackRange = 3.5f;
    [SerializeField]
    public int attackDamage = 10;

    void OnEnable()
    {
        SetInitialReferences();
        enemyMaster.EventEnemyDie += DisableThis;
        enemyMaster.EventEnemySetNavTarget += SetAttackTarget;
    }

    void OnDisable()
    {
        enemyMaster.EventEnemyDie -= DisableThis;
        enemyMaster.EventEnemySetNavTarget -= SetAttackTarget;
    }

    void Update()
    {
        TryToAttack();
    }

    void SetInitialReferences()
    {
        myTransform = transform;
    }

    void SetAttackTarget(Transform targetTransform)
    {
        attackTarget = targetTransform;
    }

    void TryToAttack()
    {
        if (attackTarget != null)
        {
            if (Time.time > nextAttack)
            {
                nextAttack = Time.time + attackRate;
                if (Vector3.Distance(myTransform.position, attackTarget.position) <= attackRange)
                {
                    Vector3 lookAtVector = new Vector3(attackTarget.position.x, myTransform.position.y, attackTarget.position.z);
                    myTransform.LookAt(lookAtVector);
                    enemyMaster.CallEventEnemyAttack();
                    enemyMaster.isOnRoute = false;
                }
            }
        }
    }

    public void OnEnemyAttack()
    {
        if (attackTarget != null)
        {
            if (Vector3.Distance(myTransform.position, attackTarget.position) <= attackRange)
            {
                Vector3 toOther = attackTarget.position - myTransform.position;

                if (Vector3.Dot(toOther, myTransform.forward) > 0.5f)
                { 
                    //appelle de la fonction qui deduis les pv du joueur
                }
            }
        }
    }

    void DisableThis()
    {
        this.enabled = false;
    }
	
}
