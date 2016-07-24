using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class gvmSpellCollider : NetworkBehaviour {
    
    
    public gvmUIDataContainer dataContainer;
    private List<int> basicProperty = new List<int>();
    readonly Vector3 _areaDefaultPosition = new Vector3(0, -200, 0);

    public void Init(gvmUIDataContainer data) {
        dataContainer = data;
        basicProperty.Add(dataContainer.propertiesId[0]);
    }

    void OnEnable() {
        if (dataContainer != null) {
            dataContainer.propertiesId = basicProperty;
        }
        StartCoroutine(StartCountdown());
    }
    
    private IEnumerator StartCountdown()
    {
        yield return new WaitForSeconds(dataContainer.areaDuration);
        gameObject.SetActive(false);
        gameObject.transform.position = _areaDefaultPosition;
    }
    
    //Event gameobject which can trigger the spell effect want it enter the collider and set to the gameobject the name of the spell
    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "HumanNPC") {
            col.gameObject.GetComponent<gvmSpellEffectGetter>().getNewEffect(dataContainer);
        }
    }

    void OnTriggerExit(Collider col) {
        if (col.gameObject.tag == "HumanNPC") {
            col.gameObject.GetComponent<gvmSpellEffectGetter>().removeAreaEffect(dataContainer);
        }
    }
}
