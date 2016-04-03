using UnityEngine;
using System.Collections;

public class gvmSpellArea: gvmSpell {

    public int duration;

    void Awake() {
        duration = 10;
        StartCoroutine(areaCountdown());
    }

    //Timer before the effect area of the spell disappear
    IEnumerator areaCountdown () {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);        
    }

    //Event gameobject which can trigger the spell effect want it enter the collider and set to the gameobject the name of the spell
    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "TriggerSpells") {
            col.gameObject.GetComponent<gvmSpellEffectGetter>().affectedBy = gameObject.tag;            
        }
    }
}
