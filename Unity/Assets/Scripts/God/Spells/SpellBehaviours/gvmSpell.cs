using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

public class gvmSpell : MonoBehaviour {

    [XmlElement("name")]
    public string spellName { get; set; }

    [XmlElement("fearCost")]
    public int fearCost;

    [XmlElement("faithCost")]
    public int faithCost;

    [XmlElement("stateEffect")]
    public int stateEffect;

    [XmlElement("properties")]
    public List<int> propertiesId;

    public int floorMask = LayerMask.GetMask("Floor");
    public float camRayLength = 100f;
    [SerializeField]
    public TextAsset xmlSpellDataFile;
    [SerializeField]
    public bool spellCasted = false;
    [SerializeField]
    public GameObject spellRender;
    
    public void setSpellPosition() {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask)) {
            Vector3 spellPosition = floorHit.point;
            spellPosition.y = 0f;
            transform.position = spellPosition;
        }
    }

    void Update() {
        if (Input.GetMouseButtonDown(1)) {
            disableSpell();
        }
        if (spellCasted == false) {
            setSpellPosition();
        }
    }

    virtual public void disableSpell() {
    }

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "TriggerSpells") {
            col.gameObject.GetComponent<gvmSpellEffectGetter>().affectedBy = spellName;
        } else if(col.gameObject.tag == "spell") {
            gvmPropertiesManager.GetInstance().GetCompatibility(propertiesId, col.GetComponent<gvmSpell>().propertiesId);
        }
    }
}
