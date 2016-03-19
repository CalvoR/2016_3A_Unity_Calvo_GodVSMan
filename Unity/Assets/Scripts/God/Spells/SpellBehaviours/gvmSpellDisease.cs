using UnityEngine;
using System.Xml;

public class gvmSpellDisease : MonoBehaviour {
    
    private int floorMask;
    private float camRayLenght = 100f;
    [SerializeField]
    private TextAsset xmlSpellDataFile;
    [SerializeField]
    private bool spellCasted = false;
    [SerializeField]
    private GameObject spellRender;
    [SerializeField]
    private GameObject diseaseArea;
    private GameObject prefab;

    void Awake() {
        floorMask = LayerMask.GetMask("Floor");
    }

    //disable click for the spell : call the effect of the spell
    void OnMouseDown() {
        if (spellCasted == false) {
            spellCasted = true;
            //askServer 
            spellEffect(transform.position);
        }
    }

    void OnMouseUp() {
        gameObject.transform.position = Vector3.up * -1000;
        disableSpell();
    }

    void Update() {
        if (Input.GetMouseButtonDown(1)) {
            disableSpell();
        }
        if (spellCasted == false) {
            setSpellPosition();
        }
    }

    //instantiate the effect area : call the xml loader to remove spell specific cost 
    void spellEffect(Vector3 spellPosition) {
        //animation
        prefab = Instantiate(Resources.Load("Prefabs/God/Spells/" + "diseaseArea", typeof(GameObject))) as GameObject;
        prefab.transform.position = spellPosition;
        gvmMonoBehaviourReference.Ressources.useRessourcesForCastedSpell(gameObject.tag);
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
        spellCasted = false;
        gameObject.transform.position = Vector3.up * -1000;
        gameObject.SetActive(false);
    }
}
