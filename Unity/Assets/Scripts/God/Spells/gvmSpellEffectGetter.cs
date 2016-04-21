using UnityEngine;
using System.Collections.Generic;

public class gvmSpellEffectGetter : MonoBehaviour {


    public string affectedBy;
    private int XMLdata;

    void Awake() {
        affectedBy = "";
    }

    void Update() {
        if (affectedBy != "") {
            List<gvmSpellProperty> Data = gvmPropertiesManager.GetInstance().propertiesContainer; //get spell data when affected by
            for (int i = 0; i < Data.Count; i++) {
                Debug.Log(Data[i]); //display affectedBy (spell) data from xml file 
            }
            affectedBy = "";
        }
    }
}