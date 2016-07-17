using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class gvmNPCData : NetworkBehaviour {
    // currently useless
    public float HP = 100;
    public int CorruptionState = 100;
    [SerializeField]
    private GameObject zombie;
    [SerializeField]
    private Transform NPCContainer;
    [SerializeField]
    private MeshRenderer mesh;
    [SerializeField]
    private Collider col;
    [SerializeField]
    private GvmGameControler controler;
        
    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "sword_weapon" && isServer) {
            Debug.LogError(col.name);
            if (!UpdateState(HP - 10, CorruptionState) && gameObject.tag == "HumanNPC") {
                changeIntoAZombie();
            }
        }
    }
    
    public bool UpdateState(float hp, int s) {
        if (isServer) {
            if (s < 50 && CorruptionState >= 50) {
                controler.addNPCHasCorrupted();
            }
            if (hp <= 0 && CorruptionState > 0) {
                controler.addNPCHasDead();
            }
        }

        HP = hp >= 0 ? hp : 0;
        CorruptionState = s >= 0 ? s : 0;
        mesh.material.color = CorruptionState == 0 ? Color.black : CorruptionState <= 20 ? Color.red : CorruptionState <= 50 ? Color.yellow : Color.green;

        RpcUpdateState(hp, s);
        if (HP <= 0 && CorruptionState <= 0) {
            return false;
        }
        return true;
    }

    [ClientRpc]
    public void RpcUpdateState(float hp, int s) {
        HP = hp;
        CorruptionState = s;
        mesh.material.color = CorruptionState <= 20 ? Color.red : CorruptionState <= 50 ? Color.yellow : Color.green;
    }

    public void changeIntoAZombie() {
        controler.addNPCHasTransformed();
        col.enabled = false;
        zombie.transform.parent = NPCContainer;
        zombie.SetActive(true);
        RpcChangeIntoAZombie();
        gameObject.SetActive(false);
    }

    [ClientRpc]
    private void RpcChangeIntoAZombie() {
        col.enabled = false;
        zombie.transform.parent = NPCContainer;
        zombie.SetActive(true);
        gameObject.SetActive(false);
    }
}
