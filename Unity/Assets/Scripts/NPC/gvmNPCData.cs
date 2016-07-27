using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class gvmNPCData : NetworkBehaviour {
    // currently useless
    public float HP = 1000;
    public int CorruptionState = 1000;
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
    [SerializeField]
    private gvmGodRessourcesManager resources;
    private int resourcesPerSeconds = 0;

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "sword_weapon" && isServer) {
            Debug.LogError(col.name);
            if (gameObject.tag == "HumanNPC") {
                UpdateState(10, 0);
            }
        }
    }

    public bool UpdateState(float hp, int s) {
        var _hp = HP - hp;
        var _corruption = CorruptionState - s;
        if (_corruption > 0) {
            if (_corruption < 200 && CorruptionState >= 200) {
                mesh.material.color = Color.red;
                controler.addNPCHasCorrupted();
            } else if (_corruption < 500 && CorruptionState >= 500) {
                mesh.material.color = Color.yellow;
            } else {
                mesh.material.color = Color.green;
            }
            CorruptionState = _corruption;
            /*
            var newRes = Mathf.FloorToInt((1000 - CorruptionState) / 100);
            resources.setResourcesPerSeconds(newRes - resourcesPerSeconds);
            resourcesPerSeconds = newRes;*/
        } else {
            CorruptionState = 0;
            mesh.material.color = Color.black;
        }

        if (_hp <= 0 && HP > 0) {
            controler.addNPCHasDead();
            HP = 0;
        } else {
            HP = _hp;
        }

        RpcUpdateState(hp, s);
        if (HP <= 0 && CorruptionState <= 0) {
            changeIntoAZombie();
            return false;
        }
        return true;
    }

    [ClientRpc]
    public void RpcUpdateState(float hp, int s) {
        HP = hp;
        CorruptionState = s;
        mesh.material.color = CorruptionState < 200 ? Color.red : CorruptionState < 500 ? Color.yellow : Color.green;
    }

    public void changeIntoAZombie() {
        controler.addNPCHasTransformed();
        col.enabled = false;
        zombie.transform.parent = NPCContainer;
        zombie.SetActive(true);
        //RpcChangeIntoAZombie();
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
