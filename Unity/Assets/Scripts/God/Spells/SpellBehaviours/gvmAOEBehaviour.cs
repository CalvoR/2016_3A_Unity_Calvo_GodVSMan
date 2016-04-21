using UnityEngine;
using System.Collections;

public class gvmAOEBehaviour : MonoBehaviour {

    private GameObject prefab;
    private bool spellCasted = false;
    [SerializeField]
    private int floorMask;
    private GameObject[] AOEContainer;
    public float camRayLength = 100f;
    private int areaCounter;
    private int areaMax = 15;
    private int duration = 15;

    void Awake() {
        floorMask = LayerMask.GetMask("Floor");
        AOEContainer = new GameObject[areaMax];
        areaCounter = 0;
        for(int i = 0; i < AOEContainer.Length; i++) {
            AOEContainer[i] = Instantiate(Resources.Load("Prefabs/God/Spells/" + "areaOfEffect", typeof(GameObject))) as GameObject;
            AOEContainer[i].SetActive(false);
        }
    }

    void Update() {
        if (Input.GetMouseButtonDown(1)) {
            disableSpell();
        }
        if (spellCasted == false) {
            setSpellPosition();
        }
    }

    public void setSpellPosition() {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask)) {
            Vector3 spellPosition = floorHit.point;
            spellPosition.y = 0f;
            transform.position = spellPosition;
        }
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

    //instantiate the effect area : call the xml loader to remove spell specific cost 
    void spellEffect(Vector3 spellPosition) {
        AOEContainer[areaCounter].transform.position = spellPosition;
        AOEContainer[areaCounter].SetActive(true);
        //StartCoroutine(areaCountdown(areaCounter));
        areaCounter++;
        if (areaCounter == AOEContainer.Length) {
            areaCounter = 0;
        }
        Debug.Log(areaCounter);
        gvmMonoBehaviourReference.Ressources.useRessourcesForCastedSpell(gameObject.tag);
    }

    //reset spell prefab to default
    public void disableSpell() {
        spellCasted = false;
        gameObject.transform.position = Vector3.up * -1000;
        gameObject.SetActive(false);
    }

    IEnumerator areaCountdown(int index) {
        yield return new WaitForSeconds(duration);
        Debug.Log("End: "+ index);
        AOEContainer[index].SetActive(false);
    }
}
