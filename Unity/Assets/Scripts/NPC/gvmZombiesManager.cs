using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class gvmZombiesManager : NetworkBehaviour
{
    [SerializeField]
    private Transform humanContainer;

    private GameObject[] humans;
    [SerializeField]
    private GvmGameControler controler;

    void Awake()
    {
        humans = new GameObject[humanContainer.childCount];
        for (int i = 0; i < humanContainer.childCount; i++) {
            humans[i] = humanContainer.GetChild(i).gameObject;
            humans[i].SetActive(true);
        }
        controler.setNPCCounter(humans.Length);
    }

    public void zombieDealDamage(int damage)
    {
        Debug.LogError("attack: " + damage);
        HeroStats.takeDamage(damage);
        RpcZombieDealDamage(damage);
    }

    [ClientRpc]
    public void RpcZombieDealDamage(int damage) {
        HeroStats.takeDamage(damage);
    }
}
