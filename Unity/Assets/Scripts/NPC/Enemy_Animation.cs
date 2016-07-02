using UnityEngine;
using System.Collections;

public class Enemy_Animation : MonoBehaviour 
{
    [SerializeField]
    private EnemyMaster enemyMaster;
    [SerializeField]
    private Animator myAnimator;

    void OnEnable()
    {
        enemyMaster.EventEnemyDie += DisableAnimator;
        enemyMaster.EventEnemyWalking += SetAnimationToWalk;
        enemyMaster.EventEnemyReachedNavTarget += SetAnimationToIdle;
        enemyMaster.EventEnemyAttack += SetAnimationToAttack;
    }

    void OnDisable()
    {
        enemyMaster.EventEnemyDie -= DisableAnimator;
        enemyMaster.EventEnemyWalking -= SetAnimationToWalk;
        enemyMaster.EventEnemyReachedNavTarget -= SetAnimationToIdle;
        enemyMaster.EventEnemyAttack -= SetAnimationToAttack;
    }

    void SetAnimationToAttack()
    {
        if (myAnimator != null)
        {
            if (myAnimator.enabled)
            {
                myAnimator.SetTrigger("Attack");
            }
        }
    }

    void SetAnimationToIdle()
    {
        if (myAnimator != null)
        {
            if (myAnimator.enabled)
            {
                myAnimator.SetBool("isPursuing", false);
            }
        }
    }

    void SetAnimationToWalk()
    {
        if (myAnimator != null)
        {
            if (myAnimator.enabled)
            {
                myAnimator.SetBool("isPursuing", true);
            }
        }
    }

    void DisableAnimator()
    {
        if (myAnimator != null)
        {
            myAnimator.enabled = false;
        }
    }
}
