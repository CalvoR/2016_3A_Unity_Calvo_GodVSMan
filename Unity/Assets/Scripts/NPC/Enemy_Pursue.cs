using UnityEngine;
using System.Collections;

public class Enemy_Pursue : MonoBehaviour
{
    [SerializeField]
    private EnemyMaster enemyMaster;
    [SerializeField]
    private NavMeshAgent myNavMeshAgent;
    [SerializeField]
    private float checkRate;
    [SerializeField]
    private float nextCheck;


    void OnEnable()
    {
        enemyMaster.EventEnemyDie += DisableThis;
    }

    void OnDisable()
    {
        enemyMaster.EventEnemyDie -= DisableThis;
    }

    void Update()
    { 
        if (Time.time > nextCheck)
        {
            nextCheck = Time.time + checkRate;
            TryToChaseTarget();
        }
    }

    void SetInitialReferences()
    {
        checkRate = Random.Range(0.1f, 0.2f);
    }

    void TryToChaseTarget()
    { 
        if (enemyMaster.myTarget != null && myNavMeshAgent != null && !enemyMaster.isNavPaused)
        {
            myNavMeshAgent.SetDestination(enemyMaster.myTarget.position);

            if (myNavMeshAgent.remainingDistance > myNavMeshAgent.stoppingDistance)
            {
                enemyMaster.CallEventEnemyWalking();
                enemyMaster.isOnRoute = true;
            }
        }
    }

    void DisableThis()
    { 
        if (myNavMeshAgent != null)
        {
            myNavMeshAgent.enabled = false;
        }
        this.enabled = false;
    }

}
