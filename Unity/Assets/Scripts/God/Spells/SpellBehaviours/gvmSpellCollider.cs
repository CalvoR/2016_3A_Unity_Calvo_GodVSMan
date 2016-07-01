using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class gvmSpellCollider : NetworkBehaviour {
    
    
    public gvmUIDataContainer dataContainer;
    private gvmPropertiesManager properties;
    private List<int> basicProperty = new List<int>();
    readonly Vector3 _areaDefaultPosition = new Vector3(0, -200, 0);

    public void Init(gvmUIDataContainer data) {
        dataContainer = data;
        properties = gvmPropertiesManager.GetInstance();
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
        if (isServer)
        {
            Debug.LogError("Server");
        }
        if (col.gameObject.tag == "TriggerSpells") {
            Debug.LogError(col.gameObject.name);
            col.gameObject.GetComponent<gvmSpellEffectGetter>().getNewEffect(dataContainer.propertiesId);
        } else if (col.gameObject.tag == "GodSpell") {
            dataContainer.propertiesId = properties.GetCompatibility(dataContainer.propertiesId, col.GetComponent<gvmUIDataContainer>().propertiesId);
            Debug.LogError(dataContainer.propertiesId.Count);
        }
    }
}
