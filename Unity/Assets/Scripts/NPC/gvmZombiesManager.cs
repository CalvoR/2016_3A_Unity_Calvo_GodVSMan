using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class gvmZombiesManager : NetworkBehaviour
{
    [SerializeField]
    private GameObject[] zombies;

    void OnEnable()
    {
        for (int i = 0; i < zombies.Length; i++)
        {
            zombies[i].SetActive(true);
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
