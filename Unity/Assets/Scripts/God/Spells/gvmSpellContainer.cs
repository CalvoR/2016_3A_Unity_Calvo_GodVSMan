using UnityEngine;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("spellCollection")]
[System.Serializable]
public class gvmSpellContainer {

    [XmlArray("spells")]
    [XmlArrayItem("spell")]
    public List<gvmSpellData> spells = new List<gvmSpellData>();

    public gvmSpellData getDataByBehaviour(string spellName) {
        foreach (gvmSpellData spell in spells) {
            if(spell.behaviour == spellName) {
                return spell;
            }
        }
        return null;
    }   

    public static gvmSpellContainer Load(string path) {
        TextAsset _xml = Resources.Load<TextAsset>(path);

        XmlSerializer serializer = new XmlSerializer(typeof(gvmSpellContainer));
        StringReader reader = new StringReader(_xml.text);
        
        var data = serializer.Deserialize(reader) as gvmSpellContainer;

        reader.Close();

        return data;
    }

    public void Save(List<gvmSpellData> spellList) {
        var path = "SpellData";
        path = "Assets/Resources/" + path +".xml";
        spells = spellList;
        XmlSerializer serializer = new XmlSerializer(typeof(gvmSpellContainer));

        FileStream stream = new FileStream(path, FileMode.Truncate);

        serializer.Serialize(stream, this);

        stream.Close();
    }
}
