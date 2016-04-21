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
    private string _path = "SpellData";
    private static gvmSpellContainer _instance;

    private gvmSpellContainer() {
        spells = new List<gvmSpellData>();
    }

    public gvmSpellData getDataByName(string spellName) {
        foreach (gvmSpellData spell in spells) {
            if(spell.name == spellName) {
                return spell;
            }
        }
        return null;
    }

    public static gvmSpellContainer GetInstance() {
        if (_instance == null) {
            _instance = new gvmSpellContainer();
            _instance.Load();
        }
        return _instance;
    }

    public void Load() {
        TextAsset _xml = Resources.Load<TextAsset>(_path);

        XmlSerializer serializer = new XmlSerializer(typeof(gvmSpellContainer));
        StringReader reader = new StringReader(_xml.text);

        _instance = serializer.Deserialize(reader) as gvmSpellContainer;

        reader.Close();
    }

    public void Save(List<gvmSpellData> spellList) {
        var path = "SpellData";
        path = "Assets/Resources/" + path +".xml";
        spells = spellList;
        XmlSerializer serializer = new XmlSerializer(typeof(gvmSpellContainer));

        FileStream stream = new FileStream(path, FileMode.Truncate);

        serializer.Serialize(stream, _instance);

        stream.Close();
    }
}
