using UnityEngine;
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

    float currentSpeed;

    private float forwardVar;               // distance de déplacement sur les axes X et Z
    private float SidewayVar;

    private float lastTapTime;              // gestion de la course
    private float doubleTapDelay;
    private float startRunningTime;

    RaycastHit rayHit;


    void Start()
    {
        UpdateStatsDisplay();
        doubleTapDelay = 0.5f;
        lastTapTime = 0;
        currentSpeed = HeroStats.Speed;
        runSpeed = (runSpeed <= HeroStats.Speed) ? HeroStats.Speed + 2 : runSpeed;
    }

    void Update() {

        ManageRun();   

        forwardVar = Input.GetAxis("Forward") * currentSpeed;
        SidewayVar = Input.GetAxis("Sideway") * currentSpeed;
        
        if(Input.GetMouseButtonUp(0))       // Récupération d'un objet au clic gauche
            GetResource();

        UpdateStatsDisplay();

        // AGIR sur le composant RigidBody plutot que Tranform
    }

    void FixedUpdate()
    {
        mainTransform.Translate(
            Vector3.forward * forwardVar * Time.deltaTime +
            Vector3.right * SidewayVar * Time.deltaTime
            );
    }


    public void UpdateStatsDisplay()
    {
        if (heroStatsDisplay != null)
            heroStatsDisplay.text =  "Player\n Attaque:" + HeroStats.Attack + "\n Defence:" + HeroStats.Defense + "\n Speed:" + HeroStats.Speed;
    }

    /// <summary>
    /// Met à jour la vitesse de déplacement si la course commence ou doit s'arrêter
    /// </summary>
    public void ManageRun()
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
        string objectName = rayHit.collider.gameObject.name;

        if (GameObject.Find(objectName).CompareTag("Resource"))   // Calcul de la collision et vérifiaction s'il s'agit d'une ressource
        {
            Destroy(GameObject.Find(objectName));
            InventoryManagement.Inventory.AddItem(
                DefaultItemsList.ItemList[ItemType.resource].Where(x => x.Name.Equals("Wood")).SingleOrDefault()
            );
        }
    }

}
