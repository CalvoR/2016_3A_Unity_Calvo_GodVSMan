using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;

public class gvmUIDataContainer : NetworkBehaviour
{

    public string Name;
    public string prefab;
    public int cost;
    public int instantCorruption;
    public int instantDamage;
    public int areaCPS;
    public int areaDPS;
    public int areaDuration;
    public float castTime;
    public int cooldown;
    public List<int> propertiesId;

    public void init(gvmSpellData data) {
        Name = data.name;
        prefab = data.prefab;
        cost = data.cost;
        instantCorruption = data.instantCorruption;
        instantDamage = data.instantDamage;
        areaCPS = data.areaCPS;
        areaDPS = data.areaDPS;
        areaDuration = data.areaDuration;
        castTime = data.castTime;
        cooldown = data.cooldown;
        propertiesId = data.propertiesId;
    }

    public void init(gvmUIDataContainer data) {
        Name = data.name;
        prefab = data.prefab;
        cost = data.cost;
        instantCorruption = data.instantCorruption;
        instantDamage = data.instantDamage;
        areaCPS = data.areaCPS;
        areaDPS = data.areaDPS;
        areaDuration = data.areaDuration;
        castTime = data.castTime;
        cooldown = data.cooldown;
        propertiesId = data.propertiesId;
    }
}
