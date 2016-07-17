using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class gvmGameMenu : NetworkBehaviour
{
    [SerializeField]
    private GameObject SettingsMenu;
    [SerializeField]
    private GvmGameControler gameControler;
    [SerializeField]
    private gvmControlCameraScript godControl;
    [SerializeField]
    private gvmControlCameraScript serverControl;
    
    void Update () {
	    if (Input.GetKeyDown(KeyCode.Escape)) {
	        SettingsMenu.SetActive(!SettingsMenu.activeInHierarchy);
        }
    }
    
    public void PauseUnpauseTheGame() {
        if (isServer) {
            Time.timeScale = Time.timeScale == 0 ? 1.0f : 0.0f;
            serverControl.setPause();
            RpcPauseUnpauseGame();
        } else {
            CmdPauseUnpauseGame();
        }
    }

    [Command]
    public void CmdPauseUnpauseGame() {
        Time.timeScale = Time.timeScale == 0 ? 1.0f : 0.0f;
        RpcPauseUnpauseGame();
    }

    [ClientRpc]
    public void RpcPauseUnpauseGame() {
        Time.timeScale = Time.timeScale == 0 ? 1.0f : 0.0f;
        godControl.setPause();
    }

    public void Surrender() {
        if (!isServer) {
        }
    }

    [Command]
    void CmdSurrender() {

    }
}
