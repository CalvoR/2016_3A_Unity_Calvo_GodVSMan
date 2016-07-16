using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class gvmNPCData : NetworkBehaviour {
    // currently useless
    public float HP = 100;
    public int state = 0;
    
    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "sword_weapon") {
            HP -= 10;
            if (isServer) {
                Debug.LogError(col.name);
                RpcUpdateState(HP, state);
            }
        }
    }

    [ClientRpc]
    public void RpcUpdateState(float hp, int s) {
        HP = hp;
        state = s;
        Debug.LogError("UpdateState");
    }
}
