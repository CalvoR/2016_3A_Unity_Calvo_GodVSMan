using UnityEngine;
using System;
using UnityEngine.Networking;

public class gvmSpell : NetworkBehaviour {

    public gvmSpellData spellData;

    public GameObject spellContainer;
    public int floorMask;
    public float camRayLength = 100f;
    [SerializeField]
    public TextAsset xmlSpellDataFile;
    
    public gvmSpell(gvmSpellData data, gvmSpellButton test) {
        spellData = data;
        floorMask = LayerMask.GetMask("Floor");



        spellContainer = Instantiate(Resources.Load("Prefabs/God/Spells/" + spellData.behaviour, typeof(GameObject))) as GameObject;

        //spellContainer =  (GameObject)Network.Instantiate(Resources.Load("Prefabs/God/Spells/" + spellData.behaviour, typeof(GameObject)), transform.position, Quaternion.identity, 0);

        spellContainer.GetComponent<gvmUIDataContainer>().init(spellData);

        spellContainer.SetActive(true);

        test.CmdSpawn(spellContainer);
    }


}
