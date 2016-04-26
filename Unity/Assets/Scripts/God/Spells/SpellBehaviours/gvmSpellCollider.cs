using UnityEngine;
using System.Collections;

public class gvmSpellCollider : MonoBehaviour {

    public int duration;
    private gvmUIDataContainer dataContainer;

    void Awake() {
        dataContainer = gameObject.GetComponent<gvmUIDataContainer>();
        duration = dataContainer.duration;
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
