using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine.UI;

public class gvmGodUIManager : MonoBehaviour {

    [SerializeField]
    private TextAsset xmlPlayerPreferencesFile;
    [SerializeField]
    private GameObject[] spellButtons;
    //Key == spellName / Value == list of every spell data 
    private string spellName;

    void Start() {
        for (int i = 0; i < 5; i++) {
            loadSpellsSelectedByThePlayerFromXMLFile(i, spellButtons[i]);
            gvmMonoBehaviourReference.xmlRessources.loadSpellDataFromXmlFile(spellButtons[i].tag);
        }
    }

    public GameObject loadSpellsSelectedByThePlayerFromXMLFile(int btnNumber, GameObject GOSpell) {
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
}
