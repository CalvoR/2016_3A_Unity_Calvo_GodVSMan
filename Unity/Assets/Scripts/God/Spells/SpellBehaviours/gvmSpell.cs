using UnityEngine;
using System;

public class gvmSpell : MonoBehaviour {

    public gvmSpellData spellData;

    public GameObject spellContainer;
    public int floorMask;
    public float camRayLength = 100f;
    [SerializeField]
    public TextAsset xmlSpellDataFile;
    
    public gvmSpell(gvmSpellData data) {
        spellData = data;
        floorMask = LayerMask.GetMask("Floor");

        spellContainer = Instantiate(Resources.Load("Prefabs/God/Spells/" + spellData.behaviour, typeof(GameObject))) as GameObject;
        spellContainer.GetComponent<gvmUIDataContainer>().init(spellData);
        
        spellContainer.SetActive(true);
    }
}
