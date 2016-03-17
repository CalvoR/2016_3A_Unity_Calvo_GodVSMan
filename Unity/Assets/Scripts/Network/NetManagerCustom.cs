using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetManagerCustom : NetworkManager
{
    [SerializeField]
    GameObject[] PlayerPrefab;


    /// <summary>
    /// Network Managger custom pour pouvoir instancier plusieurs prefab different
    /// </summary>
    /// <param name="conn"></param>
    /// <param name="playerControllerId"></param>
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        if (GameObject.Find("UI(Clone)"))
        {
            GameObject player = (GameObject)Instantiate(PlayerPrefab[1], Vector3.zero, Quaternion.identity);
            NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        }
        else
        {
            GameObject player2 = (GameObject)Instantiate(PlayerPrefab[0], Vector3.zero, Quaternion.identity);
            NetworkServer.AddPlayerForConnection(conn, player2, playerControllerId);
        }
    }

    public override void OnStartClient(NetworkClient client)
    {
        base.OnStartClient(client);
    }
}