using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

public class gvmSpellData {

    [XmlElement("name")]
    public string name;

    [XmlElement("fearCost")]
    public int fearCost;

    [XmlElement("faithCost")]
    public int faithCost;

    [XmlElement("stateEffect")]
    public int stateEffect;

    [XmlElement("properties")]
    public List<int> propertiesId;

    [XmlElement("behaviour")]
    public string behaviour;
}
