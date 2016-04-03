using UnityEngine;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("collection")]
public class gvmPropertiesManager {
    
    [XmlArray("properties")]
    [XmlArrayItem("property")]
    public static List<gvmSpellProperty> spellCompatibility;
    public string path = "SpellCompatibility";
    private static gvmPropertiesManager _instance;

    private gvmPropertiesManager() {
        TextAsset _xml = Resources.Load<TextAsset>(path);

        XmlSerializer serializer = new XmlSerializer(typeof(gvmPropertiesManager));

        StringReader reader = new StringReader(_xml.text);

        spellCompatibility = serializer.Deserialize(reader) as List<gvmSpellProperty>;

        reader.Close();
    }

    public static gvmPropertiesManager GetInstance() {
        if(_instance == null) {
            _instance = new gvmPropertiesManager();
        }
        return _instance;
    }

    public void Load(string path) {
        TextAsset _xml = Resources.Load<TextAsset>(path);

        XmlSerializer serializer = new XmlSerializer(typeof(gvmPropertiesManager));

        StringReader reader = new StringReader(_xml.text);

        spellCompatibility = serializer.Deserialize(reader) as List<gvmSpellProperty>;

        reader.Close();
    }

    public void Save(string path) {
        path = "Assets/Resources/" + path + ".xml";
        foreach (gvmSpellProperty property in spellCompatibility) {
            Debug.Log(property.id);
        }
        XmlSerializer serializer = new XmlSerializer(typeof(gvmPropertiesManager));

        FileStream stream = new FileStream(path, FileMode.Truncate);

        serializer.Serialize(stream, spellCompatibility);

        stream.Close();
    }

    public List<int> GetCompatibility(List<int> currentSpellProperties, List<int> encounteredSpellProperties) {
        List<int> tmp = currentSpellProperties;
        for(int i = 0; i < encounteredSpellProperties.Count; i++) {
            tmp.Add(encounteredSpellProperties[i]);
        }

        int rmC = 0;
        int rmN = 0;
        for (int x = 0; x < currentSpellProperties.Count;) {
            for (int y = 0; y < encounteredSpellProperties.Count; y++) {
                switch (spellCompatibility[currentSpellProperties[x]].compatibility[encounteredSpellProperties[y]]) {
                    case -1:
                        tmp.Remove(currentSpellProperties[x + rmC]);
                        rmC++;
                        break;
                    case 0:
                        tmp.Remove(currentSpellProperties[x + rmC]);
                        tmp.Remove(encounteredSpellProperties[y + rmC + rmN]);
                        rmC++;
                        rmN++;
                        break;
                    case 1:
                        tmp.Remove(encounteredSpellProperties[y + rmC + rmN]);
                        rmN++;
                        break;
                    case 2:
                        break;
                }
            }
        }
        return tmp;
    }
}

public class gvmSpellProperty {

    [XmlElement("name")]
    public string name;
    [XmlElement("id")]
    public int id;
    [XmlArray("compatibility")]
    public List<int> compatibility;

    gvmSpellProperty() {

    }
}

