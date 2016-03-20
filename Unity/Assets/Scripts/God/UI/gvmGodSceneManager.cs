using UnityEngine;
using System.Xml;
using UnityEngine.UI;
using System.Collections;

public class gvmGodSceneManager : MonoBehaviour {

    [SerializeField]
    private TextAsset xmlPlayerPreferencesFile;
    [SerializeField]
    private GameObject[] spellButtons = new GameObject[5];
    [SerializeField]
    private Text[] buttonsText = new Text[5];


    void Awake() {
        //GodDataLoader godDataContainer();
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlPlayerPreferencesFile.text);
        initialiseSpellButtons(xmlDoc, spellButtons, buttonsText);
        //GodSpellManager = new gvmSpellDataManager();
    }

    public void initialiseSpellButtons(XmlDocument xmlDoc, GameObject[] spellButtons, Text[] buttonsText) {
        int btnIndex = 0;
        XmlNodeList spellList = xmlDoc.GetElementsByTagName("spells")[0].ChildNodes;

        foreach (GameObject button in spellButtons) {
            button.tag = spellList[btnIndex].FirstChild.InnerText;
            buttonsText[btnIndex].text = spellList[btnIndex].FirstChild.InnerText;
            btnIndex++;
        }
    }
}
