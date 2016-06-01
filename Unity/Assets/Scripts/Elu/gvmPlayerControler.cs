﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class gvmPlayerControler : MonoBehaviour {

    [SerializeField]
    Transform mainTransform;

    [SerializeField]
    public Text heroStatsDisplay;

    [SerializeField]
    [Range(5.0f, 35.0f)]
    float runSpeed;

    [SerializeField]
    Camera player_camera;

    [SerializeField]
    MeshRenderer playerHead;

    [SerializeField]
    MeshRenderer playerBody;

    [SerializeField]
    GameObject prefab;
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
    }*/

    float currentSpeed;

    private float forwardVar;               // distance de déplacement sur les axes X et Z
    private float SidewayVar;

    private float lastTapTime;              // gestion de la course
    private float doubleTapDelay;
    private float startRunningTime;

    RaycastHit rayHit;


    void Start()
    {
        player_camera.enabled = true;
        UpdateStatsDisplay();
        doubleTapDelay = 0.5f;
        lastTapTime = 0;
        currentSpeed = HeroStats.Speed;
        runSpeed = (runSpeed <= HeroStats.Speed) ? HeroStats.Speed + 2 : runSpeed;
    }

    void Update() {
            CmdManageRun();
            
            if (Input.GetMouseButtonUp(0))       // Récupération d'un objet au clic gauche
                GetResource();

            UpdateStatsDisplay();
            // AGIR sur le composant RigidBody plutot que Tranform
        

    }

    void FixedUpdate() {
        forwardVar = Input.GetAxis("Forward") * currentSpeed;
        SidewayVar = Input.GetAxis("Sideway") * currentSpeed;

        mainTransform.Translate(
            Vector3.forward * forwardVar * Time.deltaTime +
            Vector3.right * SidewayVar * Time.deltaTime
            );
    }


    public void UpdateStatsDisplay()
    {
        if (heroStatsDisplay != null)
            heroStatsDisplay.text =  "Player\n Attaque:" + HeroStats.Attack + "\n Defense:" + HeroStats.Defense + "\n Vitesse:" + HeroStats.Speed;
    }

    /// <summary>
    /// Met à jour la vitesse de déplacement si la course commence ou doit s'arrêter
    /// </summary>
    /// 
    public void CmdManageRun()
    {
        if (Input.GetKeyDown("up") || Input.GetKeyDown("z"))
        {
            if (Time.time - lastTapTime < doubleTapDelay)
            {                                                           // activation ou non de la course
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
    public void GetResource()
    {
        var ray = player_camera.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out rayHit, 5.0f))
            return;

        GameObject targetResource = GameObject.Find(rayHit.collider.gameObject.name);       // Calcul de la collision et de l'objet touché
        string resourceType = string.Empty;

        if (targetResource != null) 
        {
            if (targetResource.CompareTag("wood_resource"))
                resourceType = "Wood";
            else if (targetResource.CompareTag("steel_resource"))
                resourceType = "Steel";
            else
                return;

            Destroy(targetResource);                        // l'objet est "détruit" dans la scène et ajouté dans l'inventaire
            InventoryManagement.Inventory.AddItem(
                DefaultItemsList.ItemList[ItemType.resource].Where(x => x.Name.Equals(resourceType)).SingleOrDefault()
            );
        }
    }

}
