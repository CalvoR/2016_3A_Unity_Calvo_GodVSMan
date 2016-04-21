using UnityEngine;
using System.Xml;

public class gvmSpellMassHeal {
    /**
    private GameObject prefab;
    
    //disable click for the spell : call the effect of the spell
    void OnMouseDown() {
        if (spellCasted == false) {
            spellCasted = true;
            //askServer 
            spellEffect(transform.position);
        }
    }

    void OnMouseUp() {
        gameObject.transform.position = Vector3.up * -1000;
        Invoke("disableSpell", 0);
    }
    
    //instantiate the effect area : call the xml loader to remove spell specific cost 
    void spellEffect(Vector3 spellPosition) {
        //animation
        prefab = Instantiate(Resources.Load("Prefabs/God/Spells/" + "healingArea", typeof(GameObject))) as GameObject;
        prefab.transform.position = spellPosition;
        gvmMonoBehaviourReference.Ressources.useRessourcesForCastedSpell(gameObject.tag);
    }

    //reset spell prefab to default
    public override void disableSpell() {
        spellCasted = false;
        gameObject.transform.position = Vector3.up * -1000;
        gameObject.SetActive(false);
    }**/
}
