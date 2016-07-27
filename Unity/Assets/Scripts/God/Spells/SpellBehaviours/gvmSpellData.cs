using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

public class gvmSpellData {

    [XmlElement("name")]
    public string name;

    [XmlElement("cost")]
    public int cost;
    
    [XmlElement("instantCorruption")]
    public int instantCorruption;

    [XmlElement("instantDamage")]
    public int instantDamage;

    [XmlElement("areaCPS")]
    public int areaCPS;

    [XmlElement("areaDPS")]
    public int areaDPS;

    [XmlElement("areaDuration")]
    public int areaDuration;
    
    [XmlElement("castTime")]
    public int castTime;

    [XmlElement("cooldown")]
    public int cooldown;

    [XmlElement("prefab")]
    public string prefab;

    [XmlElement("properties")]
    public List<int> propertiesId;
    
    public gvmSpellData() {
        propertiesId = new List<int>();
    }
}
