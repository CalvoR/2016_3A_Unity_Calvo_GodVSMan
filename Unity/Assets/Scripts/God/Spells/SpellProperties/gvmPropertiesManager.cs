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

        int rmC = 0; //removedCurrent
        int rmN = 0; //removedNew
        for (int x = 0; x < currentSpellProperties.Count; x++) {
            for (int y = 0; y < encounteredSpellProperties.Count; y++) {
                switch (propertiesContainer[currentSpellProperties[x]].compatibility[encounteredSpellProperties[y]]) {
                    case -1:
                        Debug.Log(-1);
                        tmp.Remove(currentSpellProperties[x + rmC]);
                        rmC++;
                        break;
                    case 0:
                        Debug.Log(0);
                        tmp.Remove(currentSpellProperties[x + rmC]);
                        tmp.Remove(encounteredSpellProperties[y + rmC + rmN]);
                        rmC++;
                        rmN++;
                        break;
                    case 1:
                        Debug.Log(1);
                        tmp.Remove(encounteredSpellProperties[y + rmC + rmN]);
                        rmN++;
                        break;
                    case 2:
                        Debug.Log(2);
                        break;
                }
            }
        }
        return tmp;
    }
}

[System.Serializable]
public class gvmSpellProperty {

    [XmlElement("name")]
    public string name;
    [XmlElement("id")]
    public int id;
    [XmlArray("compatibility")]
    public List<int> compatibility;

    public gvmSpellProperty() {
    }

    public gvmSpellProperty(int length) {
        compatibility = new List<int>();
        for(int i = 0; i < length; i++) {
            compatibility.Add(0);
        }
    }
}

