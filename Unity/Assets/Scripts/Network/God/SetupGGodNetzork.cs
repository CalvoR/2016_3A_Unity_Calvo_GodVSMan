using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SetupGGodNetzork : NetworkBehaviour
{
    [SerializeField]
    Camera GodCamera;

    [SerializeField]
    AudioListener GodAudioListener;

    private GameObject Camplayer1;
    private bool player1 = false;

    /// <summary>
    /// Initialisation du joueur Dieu lors de ça connexion
    /// </summary>
    // Use this for initialization
    void Start()
    {
        if (isLocalPlayer)
        {
            GameObject.Find("Camera_Scene").SetActive(false);
            GodCamera.enabled = true;
            GodAudioListener.enabled = true;
        }
    }

    /// <summary>
    /// Permet de savoir si le deuxieme joueur c'est connecter pour desactiver ça camera
    /// </summary>
    void Update()
    {
        if (!player1)
        {
            Camplayer1 = GameObject.FindGameObjectWithTag("EluCamera");
            if (Camplayer1)
            {
                Camplayer1.GetComponent<Camera>().enabled = false;
                Camplayer1.GetComponent<AudioListener>().enabled = false;
                player1 = true;
            }
        }
    }
}
