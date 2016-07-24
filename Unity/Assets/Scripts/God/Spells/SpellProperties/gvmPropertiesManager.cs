using UnityEngine;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("collection")]
[System.Serializable]
public class gvmPropertiesManager {
    
    [XmlArray("properties")]
    [XmlArrayItem("property")]
    public List<gvmSpellProperty> propertiesContainer;
    public string _path = "PropertiesCompatibility";
    private static gvmPropertiesManager _instance;

    private gvmPropertiesManager() {
        propertiesContainer = new List<gvmSpellProperty>();
    }

    public static gvmPropertiesManager GetInstance() {
        if(_instance == null) {
            _instance = new gvmPropertiesManager();
            _instance.Load();
        }
        return _instance;
    }

    public void Load() {
        TextAsset _xml = Resources.Load<TextAsset>(_path);
        XmlSerializer serializer = new XmlSerializer(typeof(gvmPropertiesManager));
        StringReader reader = new StringReader(_xml.text);

        _instance = serializer.Deserialize(reader) as gvmPropertiesManager;

        reader.Close();
    }

    public void Save(List<gvmSpellProperty> Data) {
        propertiesContainer = Data;
        var _fullPath = "Assets/Resources/" + _path + ".xml";
        XmlSerializer serializer = new XmlSerializer(typeof(gvmPropertiesManager));

        FileStream stream = new FileStream(_fullPath, FileMode.Truncate);

        serializer.Serialize(stream,  _instance);

        stream.Close();
    }

    public List<int> GetCompatibility(List<int> currentSpellProperties, List<int> encounteredSpellProperties) {
        List<int> tmp = currentSpellProperties;
        for(int i = 0; i < encounteredSpellProperties.Count; i++) {
            tmp.Add(encounteredSpellProperties[i]);
        }

        for (int x = 0; x < tmp.Count-1; x++) {
            for (int y = x+1; y < tmp.Count; y++) {
                switch (propertiesContainer[tmp[x]].compatibilities[tmp[y]]) {
                    case -1:
                        tmp.RemoveAt(y);
                        break;
                    case 0:
                        tmp.RemoveAt(x);
                        tmp.RemoveAt(y);
                        break;
                    case 1:
                        tmp.RemoveAt(x);
                        break;
                    case 2:
                        break;
                }
            }
        }
        return tmp;
    }

    public gvmSpellProperty getPropertyById(int id) {
        return propertiesContainer[id];
    }
}

[System.Serializable]
public class gvmSpellProperty {

    [XmlElement("name")]
    public string name;
    [XmlElement("id")]
    public int id;
    [XmlElement("duration")]
    public int duration = 1;
    [XmlElement("damageOverTime")]
    public int dot;
    [XmlElement("corruptionOverTime")]
    public int cot;
    [XmlArray("compatibilities")]
    public List<int> compatibilities;

    public gvmSpellProperty() {
    }

    public gvmSpellProperty(int length) {
        compatibilities = new List<int>();
        for(int i = 0; i < length; i++) {
            compatibilities.Add(0);
        }
    }
}

