using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gvmSpellCollider : MonoBehaviour {

    private gvmUIDataContainer dataContainer;
    private gvmPropertiesManager properties;
    private int basicProperty;

    void Awake() {
        dataContainer = gameObject.GetComponent<gvmUIDataContainer>();
        properties = gvmPropertiesManager.GetInstance();
        basicProperty = dataContainer.propertiesId[0];
    }

    void OnEnable() {
        dataContainer.propertiesId = new List<int>() { basicProperty };
        if (dataContainer.areaDuration > 0) {
            StartCoroutine(areaCountdown());
        }
    }

    //Timer before the effect area of the spell disappear
    IEnumerator areaCountdown() {
        yield return new WaitForSeconds(dataContainer.areaDuration);
        gameObject.SetActive(false);
    }

    //Event gameobject which can trigger the spell effect want it enter the collider and set to the gameobject the name of the spell
    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "TriggerSpells") {
            Debug.Log(dataContainer.name);
            col.gameObject.GetComponent<gvmSpellEffectGetter>().getNewEffect(dataContainer.propertiesId);
        } else if (col.gameObject.tag == "GodSpell") {
            dataContainer.propertiesId = properties.GetCompatibility(dataContainer.propertiesId, col.GetComponent<gvmUIDataContainer>().propertiesId);
        }
    }
}
