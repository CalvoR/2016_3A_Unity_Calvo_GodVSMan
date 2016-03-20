using UnityEngine;
using System.Collections.Generic;
using System.Xml;

public class gvmSpellDataManager : MonoBehaviour {


    private string path = "SpellData";

    [SerializeField]
    private TextAsset xmlSpellDataFile;
    public gvmSpellContainer spellDataContainer;

    void Awake() {
        gvmMonoBehaviourReference.spellContainer = this;
        spellDataContainer = gvmSpellContainer.Load(path);


    }

}
