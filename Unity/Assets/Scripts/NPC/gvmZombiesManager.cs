using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class gvmZombiesManager : NetworkBehaviour
{
    [SerializeField]
    private GameObject[] zombies;
    public GameObject[] humans;

    void Awake()
    {
        for (int i = 0; i < zombies.Length; i++)
        {
            zombies[i].SetActive(false);
        }
        for (int i = 0; i < humans.Length; i++) {
            humans[i].SetActive(true);
        }
    }

    public void zombieDealDamage(int damage)
    {
        HeroStats.takeDamage(damage);
        RpcZombieDealDamage(damage);
    }

    [ClientRpc]
    public void RpcZombieDealDamage(int damage) {
        HeroStats.takeDamage(damage);
    }
}
