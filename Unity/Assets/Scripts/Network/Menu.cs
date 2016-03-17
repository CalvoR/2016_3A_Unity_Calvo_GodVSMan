using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour
{
    [SerializeField]
    public string IP = "127.0.0.1";

    [SerializeField]
    public int Port = 7777;

    /// <summary>
    /// Test pour creer mon propre server aux vues des echec d'utilisation du Manager Networking
    /// </summary>
    void OnGUI()
    {
        if (Network.peerType == NetworkPeerType.Disconnected)
        {
            if (GUI.Button(new Rect(100, 100, 100, 25), "Start Client"))
                Network.Connect(IP, Port);

            if (GUI.Button(new Rect(100, 125, 100, 25), "Start Server"))
                Network.InitializeServer(10, Port, true);
        }
        else
        {
            if (Network.peerType == NetworkPeerType.Client)
            {
                GUI.Label(new Rect(100, 100, 100, 25), "Client");

                if (GUI.Button(new Rect(100, 125, 100, 25), "Logout"))
                    Network.Disconnect(250);
            }

            if (Network.peerType == NetworkPeerType.Server)
            {
                GUI.Label(new Rect(100, 100, 100, 25), "Server");
                GUI.Label(new Rect(100, 125, 100, 25), "Connection : " + Network.connections.Length);
                if (GUI.Button(new Rect(100, 150, 100, 25), "Logout"))
                    Network.Disconnect(250);
            }
        }
    }
	
}
