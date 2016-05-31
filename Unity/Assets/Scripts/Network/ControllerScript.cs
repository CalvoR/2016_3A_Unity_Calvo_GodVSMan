using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ControllerScript : NetworkBehaviour {

    [SerializeField]
    GameObject playerCamera;

    [SerializeField]
    GameObject GodUI;
    
    public override void OnStartLocalPlayer() {
        base.OnStartLocalPlayer();
        if (Camera.main && Camera.main.gameObject) {
            Camera.main.gameObject.SetActive(false);
        }
        playerCamera.SetActive(true);
    }
}