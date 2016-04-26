using UnityEngine;
using System;

public class gvmSpell : MonoBehaviour {

    public gvmSpellData spellData;
    private gvmUIDataContainer dataContainer;

    public GameObject spellContainer;
    public int floorMask;
    public float camRayLength = 100f;
    [SerializeField]
    public TextAsset xmlSpellDataFile;
    [SerializeField]
    
    public gvmSpell(gvmSpellData data) {
        
        spellData = data;

        floorMask = LayerMask.GetMask("Floor");

        spellContainer = Instantiate(Resources.Load("Prefabs/God/Spells/" + spellData.behaviour, typeof(GameObject))) as GameObject;
        spellContainer.GetComponent<gvmUIDataContainer>().init(spellData);
        
        spellContainer.SetActive(true);
        spellContainer.SetActive(false);
    }
    
    //put this in the behaviour because monobehaviour don't run after creation with constructor
    void OnTriggerEnter(Collider col) {
        Debug.Log(col.gameObject.name);
        if (col.gameObject.tag == "TriggerSpells") {
            col.gameObject.GetComponent<gvmSpellEffectGetter>().affectedBy = spellData.name;
            Debug.Log(spellData.name);
        } else if (col.gameObject.tag == "spell") {
            Debug.Log(gvmPropertiesManager.GetInstance().GetCompatibility(spellData.propertiesId, col.GetComponent<gvmSpell>().spellData.propertiesId));
        }
    }
}
