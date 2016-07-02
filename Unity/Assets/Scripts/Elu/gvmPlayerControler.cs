using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Networking;

public class gvmPlayerControler : NetworkBehaviour {

    #region Attributs

    [SerializeField]
    Transform mainTransform;

    [SerializeField]
    public Text heroStatsDisplay;

    [SerializeField]
    Camera playerCamera;

    [SerializeField]
    MeshRenderer playerHead;

    [SerializeField]
    MeshRenderer playerBody;

    [SerializeField]
    GameObject prefab;

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
        playerCamera.gameObject.SetActive(true);
        UpdateStatsDisplay();
        doubleTapDelay = 0.5f;
        lastTapTime = 0;
        currentSpeed = HeroStats.Speed;
        runSpeed = HeroStats.Speed * RUN_COEF;
    }

    /*
public override void OnStartLocalPlayer() {
    base.OnStartLocalPlayer();
    //CmdSpawn();

    if (Camera.main && Camera.main.gameObject) {
        Camera.main.gameObject.SetActive(false);
    }

    player_camera.enabled = true;
    playerBody.material.color = Color.green;
    playerHead.material.color = Color.green;
}       */

    void Update()
    {
        if (isLocalPlayer) {
            CmdManageRun();

            if (Input.GetMouseButtonUp(0))       // Récupération d'un objet au clic gauche
                GetItem();

            UpdateStatsDisplay();
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

    /// <summary>
    /// Met à jour la vitesse de déplacement si la course commence ou doit s'arrêter
    /// </summary>
    public void CmdManageRun()
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

    /// <summary>
    /// Disparition et ajout de la ressource lorsque celle-ci est récupérée au sol
    /// </summary>
    public void GetItem()
    {
        var ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out rayHit, 5.0f))
            return;

        GameObject targetResource = GameObject.Find(rayHit.collider.gameObject.name);       // Calcul de la collision et de l'objet touché
        string[] ItemInfos = Item.GetItemInfosFromGameObject(targetResource);

        if (ItemInfos == null)
            return;

        Destroy(targetResource);                        // l'objet est "détruit" dans la scène et ajouté dans l'inventaire
        if (ItemInfos[0].Equals("Relic"))
            return;

        InventoryManagement.Inventory.AddItem(
            DefaultItemsList.ItemList[(ItemType) int.Parse(ItemInfos[1])].Where(x => x.Name.Equals(ItemInfos[0])).SingleOrDefault()
            );      
    }
    /*
    public void ActivateConsumableItem()
    {
        
        DateTime boostTimer;
        
        if (DateTime.Now < boostTimer + BOOST_DURATION)
            Inventory.UseConsumable(boostTimer, false);
        else
            Inventory.UseConsumable(boostTimer, true);
    }
    */
    #endregion

}


