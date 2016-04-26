using UnityEngine;
using System.Collections.Generic;

public class gvmUIDataContainer : MonoBehaviour {
    
    public string Name;
    
    public int stateEffect;
    
    public List<int> propertiesId;
    
    public int areaMax = 15;
    
    public int duration;
    
    public string behaviour;

    public void init(gvmSpellData data) {
        Name = data.name;
        stateEffect = data.stateEffect;
        propertiesId = data.propertiesId;
        areaMax = data.areaMax;
        duration = data.duration;
        behaviour = data.behaviour;
    }
    
    public gvmSpellData getData() {
        gvmSpellData data = new gvmSpellData();
        data.name = Name;
        data.stateEffect = stateEffect;
        data.propertiesId = propertiesId;
        data.areaMax = areaMax;
        data.duration = duration;
        data.behaviour = behaviour;
        return data;
    }
}
