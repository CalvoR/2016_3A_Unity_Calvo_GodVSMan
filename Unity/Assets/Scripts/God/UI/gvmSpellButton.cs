using UnityEngine;
using UnityEngine.UI;


public class gvmSpellButton : MonoBehaviour {

    private GameObject prefab;
    [SerializeField]
    private Text spellName;
    private gvmSpell spellAttached;

    public void initialise(gvmSpellData data) {
        spellName.text = data.name;
        spellAttached = new gvmSpell(data);
    }

    public void OnClickBehaviour()
    {
        spellAttached.spellPrefab.SetActive(true);
    }

    void Update ()
    {        
        if (Input.GetButtonDown(gameObject.name))
            spellAttached.spellPrefab.SetActive(true);
    }
}
