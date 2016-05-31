using UnityEngine;
using System.Collections;
using System;

public class gvmWaveBehaviour : MonoBehaviour {

    [SerializeField]
    public GameObject spellContainer;
    [SerializeField]
    public GameObject areaOfEffect;
    [SerializeField]
    public Collider waveCollider;
    [SerializeField]
    public TextAsset xmlSpellDataFile;

    private Vector3 firstClickPosition;
    private Vector3 secondClickPosition;
    private bool clickedOnce;
    private bool clickedTwice;
    private int floorMask;
    public float camRayLength = 100f;
    
    private float areaSize;
    private int tsunamiWaveSpeed;

    void Awake() {
        Debug.Log("Wave");
        floorMask = LayerMask.GetMask("Floor");
        firstClickPosition = Vector3.up * -1000;
        secondClickPosition = Vector3.up * -1000;
        tsunamiWaveSpeed = 30;
        areaSize = 1f;
        waveCollider.GetComponent<gvmUIDataContainer>().init(gameObject.GetComponent<gvmUIDataContainer>());
        gameObject.SetActive(false);
    }
    
    void OnEnable() {
        clickedOnce = false;
        clickedTwice = false;
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

    void Update() {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask)) {
            if (Input.GetMouseButtonDown(0)) {
                if (clickedOnce && !clickedTwice) { //second click
                    setSecondClickPosition(floorHit);
                    clickedTwice = true;
                    clickedOnce = false;
                    castSpellTsunami(firstClickPosition, secondClickPosition);
                }
                if (!clickedOnce && !clickedTwice) { //first click
                    setFirstClickPosition(floorHit);
                    clickedOnce = true;
                }
            }
            if (clickedOnce && !clickedTwice) { // waiting for second click
                displayCastedSpellPosition(firstClickPosition, floorHit.point);
            }
            if (!clickedOnce && !clickedTwice) { // waiting for first click
                setSpellPosition();
            }
        }
        if (Input.GetMouseButtonDown(1) && !clickedTwice) {
            resetSpellVariables();
        }
    }


    void setFirstClickPosition(RaycastHit floorHit) {
        spellContainer.transform.position = floorHit.point;
        firstClickPosition = floorHit.point;
        firstClickPosition.y = 0f;
    }

    void setSecondClickPosition(RaycastHit floorHit) {
        secondClickPosition = floorHit.point;
        secondClickPosition.y = 0f;
    }

    //display wave area after first click depending on first click position
    void displayCastedSpellPosition(Vector3 spellAreaBeginning, Vector3 spellAreaEnd) {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask)) {
            float xAxis = (spellContainer.transform.position.x - spellAreaEnd.x) / -200;
            float yAxis = (spellContainer.transform.position.z - spellAreaEnd.z) / -200;
            areaOfEffect.transform.localScale = new Vector3(0.1f, 0.1f, 0.035f);
            areaOfEffect.transform.localPosition = new Vector3(0.1f * 5, 0.1f, 0);
            double angle = Math.Acos(xAxis / (float)Math.Sqrt((xAxis * xAxis) + (yAxis * yAxis))) * 180 / Math.PI;
            if (yAxis > 0) { angle = -angle; }
            if (double.IsNaN(angle) == false) {
                spellContainer.transform.localRotation = Quaternion.Euler(0, (float)angle, 0);
            }
        }
    }

    //display final spell position and area end start trigger animation
    void castSpellTsunami(Vector3 spellAreaBeginning, Vector3 spellAreaEnd) {
        gvmMonoBehaviourReference.Ressources.useRessourcesForCastedSpell(gameObject.tag);
        spellContainer.transform.position = spellAreaBeginning;
        displayCastedSpellPosition(spellAreaBeginning, spellAreaEnd);
        StartCoroutine(spellAnimation(spellAreaBeginning, spellAreaEnd));
    }

    //translate trigger block along aera effect
    IEnumerator spellAnimation(Vector3 spellAreaBeginning, Vector3 spellAreaEnd) {
        waveCollider.enabled = true;
        while (waveCollider.transform.localPosition.x < areaSize) {
            waveCollider.transform.Translate(Vector3.right * tsunamiWaveSpeed * Time.deltaTime);
            yield return new WaitForSeconds(0.1f);
        }
        resetSpellVariables();
    }

    //reset spell prefab to default
    void resetSpellVariables() {
        waveCollider.enabled = false;
        firstClickPosition = Vector3.up * -1000;
        secondClickPosition = Vector3.up * -1000;
        spellContainer.SetActive(false);
        spellContainer.transform.localRotation = Quaternion.Euler(0, 0, 0);
        spellContainer.transform.position = new Vector3(0, -2000, 0);
        areaOfEffect.transform.localScale = new Vector3(0.01f, 0.1f, 0.035f);
        areaOfEffect.transform.localPosition = new Vector3(0, 0.1f, 0);
        waveCollider.transform.localPosition = new Vector3(0, 2.5f, 0);
    }

    //behaviour when the wave hit a NPC
    /*
    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "TriggerSpells") {
            col.gameObject.GetComponent<gvmSpellEffectGetter>().affectedBy = dataContainer.name;
        } else if (col.gameObject.tag == "GodSpell") {
            Debug.Log(dataContainer.name + " : "+ col.gameObject.name);
            Debug.Log(gvmPropertiesManager.GetInstance().GetCompatibility(dataContainer.propertiesId, col.GetComponent<gvmUIDataContainer>().propertiesId));
        }
    }*/
}
