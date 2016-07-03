using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Enemy_Detection : NetworkBehaviour {

	// Use this for initialization
    [SerializeField]
    private EnemyMaster enemyMaster;
    [SerializeField]
    private Transform myTransform;
    [SerializeField]
    public Transform head;
    [SerializeField]
    public LayerMask playerLayer;
    [SerializeField]
    public LayerMask sightLayer;
    [SerializeField]
    private float checkRate;
    [SerializeField]
    private float nextCheck;
    [SerializeField]
    private float detectRadius = 80;
    [SerializeField]
    private RaycastHit hit;

    void OnEnable()
    {
        SetInitialReferences();
        enemyMaster.EventEnemyDie += DisableThis;
    }

    void OnDisable()
    {
        enemyMaster.EventEnemyDie -= DisableThis;
    }

    void Update()
    {
        CarryOutDetection();
    }

    void SetInitialReferences()
    {
        myTransform = transform;
        if (head == null)
            head = myTransform;
        checkRate = Random.Range(0.8f, 1.2f);
    }

    void CarryOutDetection()
    {
        if (Time.time > nextCheck)
        {
            nextCheck = Time.time + checkRate;
            Collider[] colliders = Physics.OverlapSphere(myTransform.position, detectRadius, playerLayer);

            if (colliders.Length > 0)
            {
                foreach (Collider potentialTargetCollider in colliders)
                {
                    if (potentialTargetCollider.CompareTag("ElectCharacter"))
                    {
                        if(CanPotentialTargetBeSeen(potentialTargetCollider.transform))
                            break;
                    }
                }
            }
            else
            {
                enemyMaster.CallEventEnemyLostTarget();
            }
        }
    }

    bool CanPotentialTargetBeSeen(Transform potentialTarget)
    {
        if (Physics.Linecast(head.position, potentialTarget.position, out hit, sightLayer))
        {
            if (hit.transform == potentialTarget)
            {
                enemyMaster.CallEventEnemySetNavTarget(potentialTarget);
                return true;
            }
            else
            {
                enemyMaster.CallEventEnemyLostTarget();
                return false;
            }
        }
        else
        {
            enemyMaster.CallEventEnemyLostTarget();
            return false;
        }
    }

    void DisableThis()
    {
        this.enabled = false;
    }
}
