using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine.UI;

public class gvmLoadSpellDataFromXMLFile : MonoBehaviour {

    [SerializeField]
    private TextAsset xmlSpellDataFile;
                     //Key == spellName / Value == list of every spell data 
    public Dictionary<string, List<int>> spellDataContainer = new Dictionary<string, List<int>>();
    private string spellName;

    void Awake() {
        gvmMonoBehaviourReference.xmlRessources = this;
    }

    //get specific spell (spellName) data from SpellData.xml and put it in spellDataContainer
    public void loadSpellDataFromXmlFile(string spellName) {
        List<int> listData = new List<int>();
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlSpellDataFile.text);

        XmlNodeList spellList = xmlDoc.GetElementsByTagName("spell");
        foreach (XmlNode spellNames in spellList) {
            XmlNodeList spellData = spellNames.ChildNodes;
            if (spellName == spellData.Item(0).InnerText) {
                foreach (XmlNode value in spellData) {
                    if (value.InnerText != spellName) {
                        listData.Add(int.Parse(value.InnerText));
                    }
                }
                if(!spellDataContainer.ContainsKey(spellName)) {
                    spellDataContainer.Add(spellName, listData);
                } 
            }
        }
    }

    public void useRessourcesForCastedSpell(string spellName) {
        gvmMonoBehaviourReference.Ressources.fear -= spellDataContainer[spellName][0];
        gvmMonoBehaviourReference.Ressources.faith -= spellDataContainer[spellName][1];
    }
}
