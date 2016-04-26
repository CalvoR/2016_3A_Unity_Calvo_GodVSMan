﻿using UnityEngine;
using System.Collections;

public class gvmStrikeBehaviour : MonoBehaviour {
        
    private bool spellCasted = false;
    [SerializeField]
    private Collider spellCollider;
    private int floorMask;
    public float camRayLength = 100f;
    private gvmUIDataContainer dataContainer;

    void Awake() {
        dataContainer = gameObject.GetComponent<gvmUIDataContainer>();
        spellCollider.GetComponent<gvmUIDataContainer>().init(dataContainer.getData());

        floorMask = LayerMask.GetMask("Floor");
        spellCollider.isTrigger = false;
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

    //reset spell prefab to default
    public void disableSpell() {
        spellCollider.isTrigger = false;
        spellCasted = false;
        gameObject.SetActive(false);
    }
    /*
    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "TriggerSpells") {
            col.gameObject.GetComponent<gvmSpellEffectGetter>().affectedBy = dataContainer.name;
        } else if (col.gameObject.tag == "GodSpell") {
            Debug.Log(dataContainer.name + " : " + col.gameObject.name);
            Debug.Log(gvmPropertiesManager.GetInstance().GetCompatibility(dataContainer.propertiesId, col.GetComponent<gvmUIDataContainer>().propertiesId));
        }
    }*/
}
