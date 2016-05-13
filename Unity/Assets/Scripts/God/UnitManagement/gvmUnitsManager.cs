using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gvmUnitsManager : MonoBehaviour {
    
    private static gvmUnitsManager _instance;
    private Dictionary<int, gvmUnit> unitBundle;

    private gvmUnitsManager() {
        unitBundle = new Dictionary<int, gvmUnit>();
    }

    public static gvmUnitsManager GetInstance() {
        if(_instance == null) {
            _instance = new gvmUnitsManager();
            _instance.initBundle();
        }
        return _instance;
    }

    private void initBundle() {
        GameObject tmp;
        for (int i = 0; i< 1000; i++) {
            tmp = Instantiate(Resources.Load("Prefabs/God/WildLife/" + "unit" , typeof(GameObject))) as GameObject;
            unitBundle.Add(tmp.GetInstanceID(), tmp.GetComponent<gvmUnit>());
        }
    }
}
