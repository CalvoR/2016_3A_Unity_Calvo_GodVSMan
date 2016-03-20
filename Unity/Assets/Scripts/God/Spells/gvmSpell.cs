using System.Xml;
using System.Xml.Serialization;

public class gvmSpell {

    [XmlElement("name")]
    public string spellName;

    [XmlElement("fearCost")]
    public int fearCost;

    [XmlElement("faithCost")]
    public int faithCost;

    [XmlElement("damage")]
    public int damage;

    [XmlElement("damageOverTime")]
    public int damageOverTime;

    [XmlElement("stateEffect")]
    public int stateEffect;
}
