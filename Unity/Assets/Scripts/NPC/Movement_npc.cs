using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Movement_npc : MonoBehaviour
{
    [SerializeField]
    private EnemyMaster enemyMaster;

    [SerializeField]
    private NavMeshAgent myNavMeshAgent;
    
    [SerializeField]
    private Transform myTransform;

    [SerializeField]
    private float wanderRange = 10;

    [SerializeField]
    private NavMeshHit navHit;

    [SerializeField]
    private Vector3 wanderTarget;

    private Vector3 RandomPoint;

    void OnEnable()
    {
        SetInitialReferences();
        enemyMaster.EventEnemyDie += DisableThis;
    }

    void OnDisable()
    {
        enemyMaster.EventEnemyDie -= DisableThis;
    }

    void update()
    {
        CheckIfIShouldWander();
    }

    void SetInitialReferences()
    {
        myTransform = transform;
    }

    void CheckIfIShouldWander()
    {
        if (enemyMaster.myTarget == null && !enemyMaster.isOnRoute && !enemyMaster.isNavPaused)
        {
            if (RandomWanderTarget(myTransform.position, wanderRange, out wanderTarget))
            {
                myNavMeshAgent.SetDestination(wanderTarget);
                enemyMaster.isOnRoute = true;
                enemyMaster.CallEventEnemyWalking();
            }
        }
    }

    bool RandomWanderTarget(Vector3 centre, float range, out Vector3 result)
    {
        RandomPoint = centre + Random.insideUnitSphere * wanderRange;
        if (NavMesh.SamplePosition(RandomPoint, out navHit, 1.0f, NavMesh.AllAreas))
        {
            result = navHit.position;
            return true;
        }
        else
        {
            result = centre;
            return false;
        }
    }

    void DisableThis()
    {
        this.enabled = false;
    }
}

