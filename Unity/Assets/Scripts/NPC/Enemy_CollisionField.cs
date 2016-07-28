using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Enemy_CollisionField : NetworkBehaviour
{
	[SerializeField]
	EnemyMaster enemyMaster;

	[SerializeField]
	Rigidbody rigidbodyStrinkingMe;	

	[SerializeField]
	int damageToApply;	
	
	[SerializeField]
	float massRequirement = 50;
	
	[SerializeField]
	float speedRequirement = 5;
	
	[SerializeField]
	float damageFactor = 0.1f;
	

	void OnEnable()
	{
		enemyMaster.EventEnemyDie += DisableThis;
	}


	void OnTriggerEnter(Collider other)
	{
		rigidbodyStrinkingMe = other.GetComponent<Rigidbody>();
		
		if (rigidbodyStrinkingMe.mass >= massRequirement &&
		    rigidbodyStrinkingMe.velocity.sqrMagnitude > speedRequirement * speedRequirement)
		{
			damageToApply = (int)(damageFactor * rigidbodyStrinkingMe.mass * rigidbodyStrinkingMe.velocity.magnitude);
			enemyMaster.CallEventEnemyDeductHealth(damageToApply);
		}		
	}

	void DisableThis()
	{
		gameObject.SetActive(false);
	}
}