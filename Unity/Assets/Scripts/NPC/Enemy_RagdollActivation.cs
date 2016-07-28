using UnityEngine;
using System.Collections;

public class Enemy_RagdollActivation : MonoBehaviour
{
	[SerializeField]
	EnemyMaster enemyMaster;

	[SerializeField]
	Collider myCollider;

    [SerializeField]
    Rigidbody myRigidbody;

	void OnEnable()
	{
		enemyMaster.EventEnemyDie += ActivateRagDoll;
	}

	void OnDisable()
	{
		enemyMaster.EventEnemyDie -= ActivateRagDoll;
	}
	
	void ActivateRagDoll()
	{
		if (myRigidbody != null)
		{
			myRigidbody.isKinematic = false;
			myRigidbody.useGravity = true;
		}
		
		if (myCollider != null)
		{
			myCollider.isTrigger = false;
			myCollider.enabled = true;
		}
	}	
}