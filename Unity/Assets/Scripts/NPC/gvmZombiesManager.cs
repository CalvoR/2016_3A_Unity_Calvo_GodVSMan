using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class gvmZombiesManager : NetworkBehaviour
{
    [SerializeField]
    private Transform humanContainer;

    private gvmNPCData[] humans;
    [SerializeField]
    private GvmGameControler controler;

    void Awake()
    {
        humans = new gvmNPCData[humanContainer.childCount];
        for (int i = 0; i < humanContainer.childCount; i++) {
            humans[i] = humanContainer.GetChild(i).gameObject.GetComponent<gvmNPCData>();
            humans[i].gameObject.SetActive(true);
        }
        controler.setNPCCounter(humans.Length);
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
    
    public void riseTheCorrupted()
    {
        for (int i = 0; i < humans.Length; i++) {
            if (humans[i].CorruptionState < 200) {
                humans[i].changeIntoAZombie();
            }
        }
    }
    
    public void RiseZombies()
    {
        var castTime = 10;
        StartCoroutine(castSpell(castTime));
        RpcRiseZombie(castTime);
    }

    [ClientRpc]
    void RpcRiseZombie(int castTime) {
        StartCoroutine(castSpell(castTime));
    }

    private IEnumerator castSpell(int castTime) {
        var light = GameObject.FindGameObjectWithTag("light").GetComponent<Light>();
        light.color = Color.green;
        yield return new WaitForSeconds(castTime);
        riseTheCorrupted();
        light.color = Color.white;
    }
}
