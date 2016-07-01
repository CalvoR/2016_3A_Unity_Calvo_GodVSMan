using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class XavierNetworkInstantiateTest : NetworkBehaviour {

    public GameObject prefab;
    private GameObject cube;
    public MeshRenderer cubeRenderer;

    void Awake() {
        if (isLocalPlayer) {
            CmdSpawn();
            cubeRenderer.material.color = Color.green;
            Debug.Log("CUBE SPAWNED");
        }
    }

    void Update() {
        if (Input.GetButtonDown("Fire1")) {
            Debug.Log("CUBE ENABLE/DISABLE");
            CmdEnable();
        }
    }
    
    [Command]
    public void CmdSpawn() {
        cube = (GameObject)Instantiate(
            Resources.Load("/Prefabs/Cube"),
            prefab.transform.position,
            prefab.transform.rotation);

        //spellContainer =  (GameObject)Network.Instantiate(Resources.Load("Prefabs/God/Spells/" + spellData.behaviour, typeof(GameObject)), transform.position, Quaternion.identity, 0);
        

        //NetworkServer.Spawn(spellGO);

        NetworkServer.SpawnWithClientAuthority(cube, connectionToClient);
        cube.SetActive(true);
        Debug.Log("SPAWNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNN");
    }

    [Command]
    void CmdEnable() {
        cube.SetActive(true);
        prefab.SetActive(true);
    }

}
