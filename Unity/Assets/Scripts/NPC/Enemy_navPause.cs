using UnityEngine;
using System.Collections;

public class Enemy_navPause : MonoBehaviour 
{
    [SerializeField]
    private EnemyMaster enemyMaster;
    [SerializeField]
    private NavMeshAgent myNavMeshAgent;
    [SerializeField]
    private float pauseTime = 1;

    void OnEnable()
    {
        enemyMaster.EventEnemyDie += DisableThis;
        enemyMaster.EventEnemyDeductHealth += PauseNavMeshAgent;
    }

    void OnDisable()
    {
        enemyMaster.EventEnemyDie -= DisableThis;
        enemyMaster.EventEnemyDeductHealth -= PauseNavMeshAgent;
    }

    void PauseNavMeshAgent(int dummy)
    {
        if (myNavMeshAgent != null)
        {
            if (myNavMeshAgent.enabled)
            {
                myNavMeshAgent.ResetPath();
                enemyMaster.isNavPaused = true;
                StartCoroutine(RestartNavMeshAgent());
            }
        }
    }

    IEnumerator RestartNavMeshAgent()
    {
        yield return new WaitForSeconds(pauseTime);
        enemyMaster.isNavPaused = false;
    }

    void DisableThis()
    {
        StopAllCoroutines();
    }
}
