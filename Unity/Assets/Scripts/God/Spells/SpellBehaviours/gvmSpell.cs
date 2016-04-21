using UnityEngine;

public class gvmSpell : MonoBehaviour {

    public gvmSpellData spellData;
    public GameObject spellPrefab;

    public int floorMask;
    public float camRayLength = 100f;
    [SerializeField]
    public TextAsset xmlSpellDataFile;
    [SerializeField]
    public GameObject spellRender;
    
    public gvmSpell(gvmSpellData data) {
        spellData = data;
        floorMask = LayerMask.GetMask("Floor");
        spellPrefab = Instantiate(Resources.Load("Prefabs/God/Spells/" + spellData.behaviour, typeof(GameObject))) as GameObject;
        spellPrefab.SetActive(true);
        spellPrefab.SetActive(false);
    }

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "TriggerSpells") {
            col.gameObject.GetComponent<gvmSpellEffectGetter>().affectedBy = spellData.name;
        } else if (col.gameObject.tag == "spell") {
            gvmPropertiesManager.GetInstance().GetCompatibility(spellData.propertiesId, col.GetComponent<gvmSpell>().spellData.propertiesId);
        }
    }
}
