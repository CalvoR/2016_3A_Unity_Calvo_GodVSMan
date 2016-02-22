using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine.UI;

public class gvmLoadSpellDataFromXMLFile : MonoBehaviour {

    [SerializeField]
    private TextAsset xmlPlayerPreferencesFile;
    [SerializeField]
    private TextAsset xmlSpellDataFile;
    [SerializeField]
    private GameObject spell_button1;
    [SerializeField]
    private GameObject spell_button2;
    [SerializeField]
    private GameObject spell_button3;
    [SerializeField]
    private GameObject spell_button4;
    [SerializeField]
    private GameObject spell_button5;
    private List<GameObject> GOlist = new List<GameObject>();
                     //Key == spellName / Value == list of every spell data 
    public Dictionary<string, List<int>> spellDataContainer = new Dictionary<string, List<int>>();
    private string spellName;

    void Awake() {
        gvmMonoBehaviourReference.xmlRessources = this;
        GOlist.Add(spell_button1);
        GOlist.Add(spell_button2);
        GOlist.Add(spell_button3);
        GOlist.Add(spell_button4);
        GOlist.Add(spell_button5);
        for (int i = 0; i < 5; i++) {
            loadSpellsSelectedByThePlayerFromXMLFile(i, GOlist[i]);
            loadSpellDataFromXmlFile(GOlist[i].tag);
        }
    }
    
    GameObject loadSpellsSelectedByThePlayerFromXMLFile(int btnNumber, GameObject GOSpell) {               
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlPlayerPreferencesFile.text);
        XmlNodeList spellList = xmlDoc.GetElementsByTagName("spells")[0].ChildNodes;
        int counter = 0;
        while (btnNumber != counter) {
            counter++;
        }
        GOSpell.tag = spellList[counter].InnerText;
        return GOSpell;
    }

    //get specific spell (spellName) data from SpellData.xml and put it in spellDataContainer
    void loadSpellDataFromXmlFile(string spellName) {
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
