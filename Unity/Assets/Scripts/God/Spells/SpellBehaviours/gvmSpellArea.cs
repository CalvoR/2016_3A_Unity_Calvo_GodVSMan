﻿using UnityEngine;
using System.Collections;

public class gvmSpellArea : MonoBehaviour{

    public int duration;
    private gvmUIDataContainer dataContainer;

    void Awake() {
        dataContainer = gameObject.GetComponent<gvmUIDataContainer>();
        duration = dataContainer.duration;
    }

    void OnEnable() {
        //StartCoroutine(areaCountdown());
    }

    //Timer before the effect area of the spell disappear
    IEnumerator areaCountdown () {
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);        
    }

    //Event gameobject which can trigger the spell effect want it enter the collider and set to the gameobject the name of the spell
    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "TriggerSpells") {
            col.gameObject.GetComponent<gvmSpellEffectGetter>().affectedBy = dataContainer.name;
        } else if (col.gameObject.tag == "GodSpell") {
            Debug.Log(dataContainer.name + " : " + col.gameObject.name);
            dataContainer.propertiesId = gvmPropertiesManager.GetInstance().GetCompatibility(dataContainer.propertiesId, col.GetComponent<gvmUIDataContainer>().propertiesId);
        }
    }
}
