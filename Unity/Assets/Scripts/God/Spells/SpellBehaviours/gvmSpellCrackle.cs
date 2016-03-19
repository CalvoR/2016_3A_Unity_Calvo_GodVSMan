using UnityEngine;
using System.Xml;

public class gvmSpellCrackle : MonoBehaviour {
    
    private int floorMask;
    private float camRayLenght = 100f;
    [SerializeField]
    private TextAsset xmlSpellDataFile;
    [SerializeField]
    private bool spellCasted = false;
    [SerializeField]
    private GameObject spellRender;
    [SerializeField]
    private Collider spellCollider;

    void Awake() {
        floorMask = LayerMask.GetMask("Floor"); //Floor layer = every object it is possible to cast spell on it position
        spellCollider.isTrigger = false;
    }

    //activate the spell trigger area : disable click for the spell : call the xml loader to remove spell specific cost
    void OnMouseDown() { 
        if (spellCasted == false) {
            spellCollider.isTrigger = true;
            spellCasted = true;
            gvmMonoBehaviourReference.Ressources.useRessourcesForCastedSpell(gameObject.tag);
            //animation
        }
    }

    void OnMouseUp() {
        gameObject.transform.position = Vector3.up * -1000;
        disableSpell();
    }

    void Update() {/*
        if (Input.GetMouseButtonDown(0)) {
            gvmMonoBehaviourReference.Ressources.crackle = true;
        } else {
            gvmMonoBehaviourReference.Ressources.crackle = false;
        }*/
        if (Input.GetMouseButtonDown(1)) {
            disableSpell();
        }
        if (spellCasted == false) {
            setSpellPosition();
        }
    }
    
    //Update the spell position according to the mouse position on screen
    void setSpellPosition() {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast(camRay, out floorHit, camRayLenght, floorMask)) {
            Vector3 spellPosition = floorHit.point;
            spellPosition.y = 0f;
            transform.position = spellPosition;            
        }
    }

    //reset spell prefab to default
    void disableSpell() {
        spellCollider.isTrigger = false;
        spellCasted = false;
        gameObject.SetActive(false);
    }

    //Event gameobject which can trigger the spell effect want it enter the collider and set to the gameobject the name of the spell
    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "TriggerSpells") {
            col.gameObject.GetComponent<gvmSpellEffectGetter>().affectedBy = "Crackle";
        }
    }
}
