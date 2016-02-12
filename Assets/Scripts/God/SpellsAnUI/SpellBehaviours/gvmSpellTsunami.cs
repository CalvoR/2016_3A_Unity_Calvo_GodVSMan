using UnityEngine;
using System.Collections;
using System.Xml;
using System;

public class gvmSpellTsunami : MonoBehaviour {

    private int floorMask;
    private float camRayLength = 1000f;
    private int tsunamiWaveSpeed;
    [SerializeField]
    private TextAsset xmlSpellDataFile;
    [SerializeField]
    private GameObject spellAreaDisplay;
    [SerializeField]
    private GameObject spellContainer;
    [SerializeField]
    private GameObject wave;
    private Vector3 firstClickPosition;
    private Vector3 secondClickPosition;
    private bool clickedOnce;
    private bool clickedTwice;
    private float areaSize;

    void Awake() {
        floorMask = LayerMask.GetMask("Floor");
        firstClickPosition = Vector3.up * -1000;
        secondClickPosition = Vector3.up * -1000;
        tsunamiWaveSpeed = 30;
        clickedOnce = false;
        clickedTwice = false;
        areaSize = 1f;
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

    void OnEnable() {
        clickedOnce = false;
        clickedTwice = false;
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

    //display spell position before first click
    void setSpellPosition() {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask)) {
            Vector3 spellPosition = floorHit.point;
            spellPosition.y = 0f;
            spellContainer.transform.position = spellPosition;
        }
    }

    //display wave area after first click depending on first click position
    void displayCastedSpellPosition(Vector3 spellAreaBeginning, Vector3 spellAreaEnd) {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask)) {
        float xAxis = (spellContainer.transform.position.x - spellAreaEnd.x) / -200;
        float yAxis = (spellContainer.transform.position.z - spellAreaEnd.z) / -200;
            spellAreaDisplay.transform.localScale = new Vector3(0.1f, 0.1f, 0.035f);
            spellAreaDisplay.transform.localPosition = new Vector3(0.1f * 5, 0.1f, 0);
            double angle = Math.Acos(xAxis / (float)Math.Sqrt((xAxis * xAxis) + (yAxis * yAxis))) * 180 / Math.PI;
            if (yAxis > 0) { angle = -angle; }
            if(double.IsNaN(angle) == false) {
                spellContainer.transform.localRotation = Quaternion.Euler(0, (float)angle, 0);
            }
        }
    }
    
    //display final spell position and area end start trigger animation
    void castSpellTsunami(Vector3 spellAreaBeginning, Vector3 spellAreaEnd) {
        gvmMonoBehaviourReference.xmlRessources.useRessourcesForCastedSpell(gameObject.tag);
        spellContainer.transform.position = spellAreaBeginning;
        displayCastedSpellPosition(spellAreaBeginning, spellAreaEnd); 
        StartCoroutine(spellAnimation(spellAreaBeginning, spellAreaEnd));
    }

    //translate trigger block along aera effect
    IEnumerator spellAnimation(Vector3 spellAreaBeginning, Vector3 spellAreaEnd) {
        while (wave.transform.localPosition.x < areaSize) {
            wave.transform.Translate(Vector3.right * tsunamiWaveSpeed * Time.deltaTime);
            yield return new WaitForSeconds(0.1f);
        }
        resetSpellVariables();
    }

    //reset spell prefab to default
    void resetSpellVariables() {
        firstClickPosition = Vector3.up * -1000;
        secondClickPosition = Vector3.up * -1000;
        spellContainer.SetActive(false);
        spellContainer.transform.localRotation = Quaternion.Euler(0, 0, 0);
        spellContainer.transform.position = new Vector3(0, -2000, 0);
        spellAreaDisplay.transform.localScale = new Vector3(0.01f, 0.1f, 0.035f);
        spellAreaDisplay.transform.localPosition = new Vector3(0, 0.1f, 0);
        wave.transform.localPosition = new Vector3(0, 2.5f, 0);
    }

    //behaviour want the wave hit a NPC
    void OnTriggerEnter(Collider col) {
        if(col.gameObject.tag == "TriggerSpells" && firstClickPosition != Vector3.up * -1000 && secondClickPosition != Vector3.up * -1000) {
            col.gameObject.GetComponent<gvmSpellEffectGetter>().affectedBy = "Tsunami";
        }
    }
}
