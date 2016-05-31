using UnityEngine;
using System.Collections;

public class gvmAOEBehaviour : MonoBehaviour {
    
    private bool spellCasted = false;
    [SerializeField]
    private int floorMask;
    private Camera GodCamera;
    private GameObject[] AOEContainer;
    public GameObject AOECollider;
    public float camRayLength = 100f;
    private int areaCounter;

    gvmUIDataContainer SpellData;

    void Awake() {
        Debug.Log("AOE");
        GodCamera = GameObject.FindGameObjectWithTag("GodCamera").GetComponent<Camera>();
        SpellData = gameObject.GetComponent<gvmUIDataContainer>();
        floorMask = LayerMask.GetMask("Floor");
        AOEContainer = new GameObject[SpellData.areaMax];
        AOECollider.GetComponent<gvmUIDataContainer>().init(SpellData);
        areaCounter = 0;
        for(int i = 0; i < AOEContainer.Length; i++) {
            AOEContainer[i] = Instantiate(AOECollider);
            AOEContainer[i].SetActive(false);
        }
        gameObject.SetActive(false);
    }

    void Update() {
        if (Input.GetMouseButtonDown(1)) {
            disableSpell();
        }
        if (spellCasted == false) {
            GodCamera = GameObject.FindGameObjectWithTag("GodCamera").GetComponent<Camera>();
            setSpellPosition();
        }
    }

    public void setSpellPosition() {
        Ray camRay = GodCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask)) {
            Debug.Log(floorHit.point);
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
        //gvmMonoBehaviourReference.Ressources.useRessourcesForCastedSpell(gameObject.tag);
    }

    //reset spell prefab to default
    public void disableSpell() {
        spellCasted = false;
        gameObject.transform.position = Vector3.up * -1000;
        gameObject.SetActive(false);
    }

    IEnumerator areaCountdown(int index) {
        yield return new WaitForSeconds(SpellData.areaDuration);
        AOEContainer[index].SetActive(false);
    }
}
