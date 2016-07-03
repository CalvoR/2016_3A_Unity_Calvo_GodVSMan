using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class AutomaticallyAssignAvatarToPlayerNetworkManagerScript : NetworkManager {

    Dictionary<NetworkConnection, GameObject> activatedControllers = null;
    
    public Dictionary<NetworkConnection, GameObject> ActivatedControllers
    {
        get
        {
            if (null == activatedControllers) {
                activatedControllers = new Dictionary<NetworkConnection, GameObject>();
            }

            return activatedControllers;
        }
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId) {
        // Do not forget base functionalities
        base.OnServerAddPlayer(conn, playerControllerId);

        // Let's get the next player Controller
        var avatar = ControllerPoolManagerScript.Instance.GetNextAvailableController();

        // Let's save the controller/connection association
        ActivatedControllers.Add(conn, avatar);

        avatar.SetActive(true);

        // give authority to the client on the obtained controller
        NetworkServer.ReplacePlayerForConnection(conn, avatar, playerControllerId);
    }

    public override void OnServerDisconnect(NetworkConnection conn) {
        // Clear everything if the association is known
        if (activatedControllers.ContainsKey(conn)) {
            conn.playerControllers.Clear();
            ControllerPoolManagerScript.Instance.ReleaseController(activatedControllers[conn]);
            ActivatedControllers.Remove(conn);
        }

        // Do not forget base functionalities
        base.OnServerDisconnect(conn);
    }

    public void startClient() {
        StartClient();
    }

    public void startServer() {
        StartServer();
    }
}

