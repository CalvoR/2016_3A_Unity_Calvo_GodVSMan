using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class gvmNPCManager : NetworkBehaviour {

    [SerializeField]
    private GameObject zombiesContainer;
    [SerializeField]
    private GameObject humansContainer;
    private GameObject[] humans;
    private GameObject[] zombies;
    [SerializeField]
    private gvmGodRessourcesManager GodResourcesManager;

    void Awake() { 
            zombies = new GameObject[zombiesContainer.transform.childCount];
            humans = new GameObject[humansContainer.transform.childCount];
            for (int i = 0; i < zombiesContainer.transform.childCount; i++) {
                zombies[i] = zombiesContainer.transform.GetChild(i).gameObject;
                zombies[i].SetActive(true);
                //zombies[i].GetComponent<Enemy_Attack>().SetZombieManager(this);
                //RpcEnableNPC(zombies[i].GetComponent<NetworkIdentity>().netId);
            }
            for (int i = 0; i < humansContainer.transform.childCount; i++) {
                humans[i] = humansContainer.transform.GetChild(i).gameObject;
                humans[i].SetActive(true);
            }
            GodResourcesManager.NPCCounter = humans.Length;
    }
    
    public void zombieDealDamage(int damage) {
        HeroStats.takeDamage(damage);
        RpcZombieDealDamage(damage);
    }

    [ClientRpc]
    public void RpcZombieDealDamage(int damage) {
        HeroStats.takeDamage(damage);
    }
}
