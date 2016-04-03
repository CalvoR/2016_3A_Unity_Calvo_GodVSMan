using UnityEngine;
using System.Xml;

public class gvmSpellCrackle : gvmSpell {
    
    [SerializeField]
    private Collider spellCollider;

    void Awake() {
        spellCollider.isTrigger = false;
    }

    //activate the spell trigger area : disable click for the spell : call the xml loader to remove spell specific cost
    void OnMouseDown() { 
        if (spellCasted == false) {
            spellCollider.isTrigger = true;
            spellCasted = true;
            gvmMonoBehaviourReference.Ressources.useRessourcesForCastedSpell(gameObject.tag);
            //animation
        }
    }

    void OnMouseUp() {
        gameObject.transform.position = Vector3.up * -1000;
        disableSpell();
    }

    //reset spell prefab to default
    override public void disableSpell() {
        spellCollider.isTrigger = false;
        spellCasted = false;
        gameObject.SetActive(false);
    }

    //Event gameobject which can trigger the spell effect want it enter the collider and set to the gameobject the name of the spell
    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "TriggerSpells") {
            col.gameObject.GetComponent<gvmSpellEffectGetter>().affectedBy = "Crackle";
        }
    }
}
