using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using InventoryManagement;

public class gvmPlayerControler : NetworkBehaviour {

    #region Attributs

    [SerializeField]
    Transform characterTransform;

    [SerializeField]
    public Text heroStatsDisplay;

    [SerializeField]
    Camera playerCamera;

    [SerializeField]
    private GameObject ChosenUI;

    private float forwardVar;               // distance de déplacement sur les axes X et Z
    private float SidewayVar;

    private float lastTapTime;              // gestion de la course
    private float doubleTapDelay;
    private float startRunningTime;

    private float currentSpeed;             // Vitesse actuelle
    private float runSpeed;                 // Vitesse attribué à la course

    const float RUN_COEF = 1.75f;

    RaycastHit rayHit;

    #endregion


    #region Méthodes

    public override void OnStartLocalPlayer() {
        base.OnStartLocalPlayer();
        if (Camera.main && Camera.main.gameObject) {
            Camera.main.gameObject.SetActive(false);
        }
        ChosenUI.SetActive(true);
        playerCamera.gameObject.SetActive(true);
        UpdateStatsDisplay();
        doubleTapDelay = 0.5f;
        lastTapTime = 0;
        currentSpeed = HeroStats.Speed;
        runSpeed = HeroStats.Speed * RUN_COEF;
    }
    
    void Update()
    {
        if (isLocalPlayer) {
            ManageRun();

            forwardVar = Input.GetAxis("Forward") * currentSpeed;
            SidewayVar = Input.GetAxis("Sideway") * currentSpeed;

            if (Input.GetMouseButtonUp(0))       // Récupération d'un objet au clic gauche
                GetItem();

            UpdateStatsDisplay();
        }
    }
    
    void FixedUpdate()
    {
        if (isLocalPlayer)
        {
            CmdMoveCharacter(forwardVar, SidewayVar);
        }
    }


    /// <summary>
    /// Met à jour l'affichage des statistiques
    /// </summary>
    public void UpdateStatsDisplay()
    {
        if (heroStatsDisplay != null)
            heroStatsDisplay.text =  "Player\n Attaque:" + HeroStats.Attack + "\n Defense:" + HeroStats.Defense + "\n Vitesse:" + currentSpeed;
    }


    [Command]
    public void CmdMoveCharacter(float f, float s) {
        characterTransform.Translate(
           Vector3.forward * f * Time.deltaTime +
           Vector3.right * s * Time.deltaTime
           );
    }


    /// <summary>
    /// Disparition et ajout de la ressource lorsque celle-ci est récupérée au sol
    /// </summary>
    public void GetItem()
    {
        var ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out rayHit, 5.0f))
            return;

        GameObject targetResource = rayHit.collider.gameObject;       // Calcul de la collision et de l'objet touché
        string[] ItemInfos = Item.GetItemInfosFromGameObject(targetResource);

        if (ItemInfos == null)
            return;

        CmdDisableResource(targetResource.GetComponent<NetworkIdentity>().netId);                        // l'objet est "détruit" dans la scène et ajouté dans l'inventaire
        if (ItemInfos[0].Equals("Relic"))
            return;

        Inventory.AddItem(
            DefaultItemsList.ItemList[(ItemType)int.Parse(ItemInfos[1])].Where(x => x.Name.Equals(ItemInfos[0])).SingleOrDefault()
            );
    }



    /// <summary>
    /// Met à jour la vitesse de déplacement si la course commence ou doit s'arrêter
    /// </summary>
    public void ManageRun()
    {
        if (Input.GetKeyDown("up") || Input.GetKeyDown("z"))
        {
            if (Time.time - lastTapTime < doubleTapDelay)       // activationde la course
            {
                runSpeed = HeroStats.Speed * RUN_COEF;
                currentSpeed = runSpeed;
                startRunningTime = Time.time;
            }
            lastTapTime = Time.time;
        }
        if (Input.GetKeyUp("up") || Input.GetKeyUp("z") || HeroStats.isEnduranceFinished(startRunningTime))       // test sur la jauge d'endurance 
             currentSpeed = HeroStats.Speed;                                                                 // vitesse remise à sa valeur par défaut     
    }

    [Command]
    private void CmdDisableResource(NetworkInstanceId netId) {
        Debug.LogError("Remove On Server: " + netId);
        NetworkServer.FindLocalObject(netId).SetActive(false);
        RpcDisableResource(netId);
    }

    [ClientRpc]
    private void RpcDisableResource(NetworkInstanceId netId) {
        Debug.LogError("Remove On Client: "+netId);
        ClientScene.FindLocalObject(netId).SetActive(false);
    }

    #endregion

}


