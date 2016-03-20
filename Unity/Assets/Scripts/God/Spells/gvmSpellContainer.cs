using UnityEngine;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("spellCollection")]
public class gvmSpellContainer {

    [XmlArray("spells")]
    [XmlArrayItem("spell")]
    public List<gvmSpell> spells = new List<gvmSpell>();

    public static gvmSpellContainer Load(string path) {
        TextAsset _xml = Resources.Load<TextAsset>(path);

        XmlSerializer serializer = new XmlSerializer(typeof(gvmSpellContainer));

        StringReader reader = new StringReader(_xml.text);

        gvmSpellContainer spells = serializer.Deserialize(reader) as gvmSpellContainer;

        reader.Close();

        return spells;
    }

    public static void Save(string path, gvmSpellContainer spellContainer) {
        path = "Assets/Resources/" + path +".xml";
        foreach(gvmSpell spell in spellContainer.spells) {
            Debug.Log(spell.spellName);
        }
        XmlSerializer serializer = new XmlSerializer(typeof(gvmSpellContainer));

        FileStream stream = new FileStream(path, FileMode.Truncate);

        serializer.Serialize(stream, spellContainer);

        stream.Close();
    }
}
