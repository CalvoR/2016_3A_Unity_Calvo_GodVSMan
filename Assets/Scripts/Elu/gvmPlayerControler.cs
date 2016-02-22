using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class gvmPlayerControler : MonoBehaviour {

    [SerializeField]
    Transform mainTransform;

    [SerializeField]
    public Text heroStatsDisplay;

    [SerializeField]
    [Range(5.0f, 35.0f)]
    float runSpeed;

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

        if (Input.GetKeyDown("up"))                                 
        {               
            if (Time.time - lastTapTime < doubleTapDelay) {                    // activation ou non de la course
                currentSpeed = runSpeed;
                startRunningTime = Time.time;
            }            
            lastTapTime = Time.time;
        }

        if (Input.GetKeyUp("up") || HeroStats.isEnduranceFinished(startRunningTime) )       // test sur la jauge d'endurance 
            currentSpeed = HeroStats.Speed;                                                                 // vitesse remise à sa valeur par défaut        

        forwardVar = Input.GetAxis("Forward") * currentSpeed;
        SidewayVar = Input.GetAxis("Sideway") * currentSpeed;

        if(Input.GetMouseButtonUp(0))       // Récupération d'un objet
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

    public void GetResource()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out rayHit, 5.0f) && GameObject.Find(rayHit.collider.gameObject.name).CompareTag("Resource"))   // Calcul de la collision et vérifiaction s'il s'agit d'une ressource
        {
            Destroy(GameObject.Find(rayHit.collider.gameObject.name));
            // ADD dans l'inventaire
        }
    }

}
