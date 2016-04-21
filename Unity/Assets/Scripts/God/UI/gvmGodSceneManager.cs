using UnityEngine;
using System.Xml;
using UnityEngine.UI;
using System.Collections;

public class gvmGodSceneManager : MonoBehaviour {

    [SerializeField]
    private TextAsset xmlPlayerPreferencesFile;
    [SerializeField]
    private GameObject[] spellButtons = new GameObject[5];
    
    private gvmPropertiesManager spellProperties;
    private gvmSpellContainer spellDataContainer;

    void Awake() {
        //GodDataLoader godDataContainer();
        spellProperties = gvmPropertiesManager.GetInstance();
        spellDataContainer = gvmSpellContainer.GetInstance();
        spellDataContainer.Load();
        //spellDataContainer.Load(path);

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlPlayerPreferencesFile.text);
        initialiseSpellButtons(xmlDoc);
        //GodSpellManager = new gvmSpellDataManager();
    }

    public void initialiseSpellButtons(XmlDocument xmlDoc) {
        int btnIndex = 0;
        XmlNodeList spellList = xmlDoc.GetElementsByTagName("spells")[0].ChildNodes;

        foreach (GameObject button in spellButtons) {
            button.GetComponent<gvmSpellButton>().initialise(spellDataContainer.getDataByName(spellList[btnIndex].FirstChild.InnerText));
            btnIndex++;
        }
    }
}