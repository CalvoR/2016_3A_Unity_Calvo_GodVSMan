using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.IO;

public class gvmSpellEffectGetter : MonoBehaviour {


    public string affectedBy = null;
    private int XMLdata;

    void Awake() {
        affectedBy = null;
    }

    void Update() {
        if (affectedBy != null) {
            List<int> Data = gvmMonoBehaviourReference.xmlRessources.spellDataContainer[affectedBy]; //get spell data when affected by
            for (int i = 0; i < Data.Count; i++) {
                Debug.Log(Data[i]); //display affectedBy (spell) data from xml file 
            }
            affectedBy = null;
        }
    }
}