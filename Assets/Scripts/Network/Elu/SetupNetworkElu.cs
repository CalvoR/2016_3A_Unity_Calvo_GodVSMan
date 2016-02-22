using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SetupNetworkElu : NetworkBehaviour
{
    [SerializeField]
    Camera EluCamera;

    [SerializeField]
    AudioListener EluAudioListener;

    private bool player2 = false;

    /// <summary>
    /// Initialisation du Joueur Elu lors de sa connexion
    /// </summary>
    void Start()
    {
        if (!isLocalPlayer)
        {
             GameObject.Find("Camera_Scene").SetActive(false);
             EluCamera.enabled = true;
             EluAudioListener.enabled = true;
            if (!player2)
            {
                GameObject Camplayer2 = GameObject.FindGameObjectWithTag("MainCamera");
                Camplayer2.GetComponent<Camera>().enabled = false;
                Camplayer2.GetComponent<AudioListener>().enabled = false;
                player2 = true;
            }
        }
    }
}
