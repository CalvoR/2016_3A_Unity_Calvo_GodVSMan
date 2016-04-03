using UnityEngine;
using System.Collections.Generic;
using System.Xml;

public class gvmSpellDataManager : MonoBehaviour {

    public gvmPropertiesManager spellProperties;
    private string path = "SpellData";

    [SerializeField]
    private TextAsset xmlSpellDataFile;
    public gvmSpellContainer spellDataContainer;

    void Awake() {
        gvmMonoBehaviourReference.spellContainer = this;
        spellProperties = gvmPropertiesManager.GetInstance();
        spellDataContainer = gvmSpellContainer.Load(path);
    }
}
