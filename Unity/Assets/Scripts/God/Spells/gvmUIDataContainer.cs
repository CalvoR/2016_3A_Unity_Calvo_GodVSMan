using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;

public class gvmUIDataContainer : NetworkBehaviour {
    
    public string Name;
    
    public int stateEffect;
    
    public List<int> propertiesId;
    
    public int areaMax;
    
    public int areaDuration;
    
    public string behaviour;

    public void init(gvmSpellData data) {
        Name = data.name;
        stateEffect = data.stateEffect;
        propertiesId = data.propertiesId;
        areaMax = data.areaMax;
        areaDuration = data.areaDuration;
        behaviour = data.behaviour;
    }
    

    public void init(gvmUIDataContainer data) {
        Name = data.name;
        stateEffect = data.stateEffect;
        propertiesId = data.propertiesId;
        areaMax = data.areaMax;
        areaDuration = data.areaDuration;
        behaviour = data.behaviour;
    }
}
