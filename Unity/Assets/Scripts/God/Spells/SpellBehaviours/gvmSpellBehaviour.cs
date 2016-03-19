using UnityEngine;
using System.Xml;

public class gvmSpellBehaviour : MonoBehaviour {

    private Vector3 tmp;
    int floorMask;
    float camRayLenght = 100f;
    [SerializeField]
    private TextAsset xmlSpellDataFile;
    [SerializeField]
    private bool spellCasted = false;
    [SerializeField]
    private GameObject spellRender;

    void Awake() {
        floorMask = LayerMask.GetMask("Floor");
    }

    void OnMouseDown() {
        if(spellCasted == false) {
            spellCasted = true;
            //gvmMonoBehaviourReference.xmlRessources.useRessourcesForCastedSpell(gameObject.tag);
            //setRessources(gameObject.tag);
            //animation
        }
    }
    void OnMouseUp() {
        spellRender.SetActive(false);
        Invoke("disableSpell", 1);
    }

    void OnTriggerEnter(Collider other) {        
        //check and disabled every object which trigger
        if (other.gameObject.tag == "TriggerSpells") {
            //other.gameObject.SetActive(false); 
        }
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            gvmMonoBehaviourReference.Ressources.crackle = true;
        } else {
            gvmMonoBehaviourReference.Ressources.crackle = false;
        }
    }

    void FixedUpdate() {
        if (spellCasted == false) {
            setSpellPosition();
        }
    }

    public void EnableSpellOnClick () {
        gameObject.SetActive(true);
    }

    void setSpellPosition() {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLenght, floorMask)) {
            Vector3 spellPosition = floorHit.point;
            spellPosition.y = 0f;
            transform.position = spellPosition;            
        }
    }

    void disableSpell() {
        spellCasted = false;
        spellRender.SetActive(true);
        gameObject.SetActive(false);
    }
}
