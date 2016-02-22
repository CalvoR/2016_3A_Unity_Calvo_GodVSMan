using UnityEngine;
using System.Collections.Generic;
using System.Xml;


public class spellDataManager : MonoBehaviour {

    //Key == spellName / Value == list of every spell data 
    private Dictionary<string, List<int>> spellDataContainer;
    private string spellName;
    private List<int> listData;

    [SerializeField]
    private TextAsset xmlSpellDataFile;

    void Awake() {
        gvmMonoBehaviourReference.spellDataContainer = this;
    }

    public void loadRessources(string spellName) {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlSpellDataFile.text);

        XmlNodeList spellList = xmlDoc.GetElementsByTagName("spell");
        foreach (XmlNode spellNames in spellList) {
            XmlNodeList spellData = spellNames.ChildNodes;
            if (spellName == spellData.Item(0).InnerText) {
                foreach (XmlNode value in spellData) {
                    if(value.InnerText != "name") {
                        listData.Add(int.Parse(value.InnerText));
                    }
                    spellDataContainer.Add(spellName, (listData));
                    Debug.Log(spellDataContainer);
                }
            }
        }
    }

    void useRessources(string spellName) {
        gvmMonoBehaviourReference.Ressources.fear -= spellDataContainer[spellName][0];
        gvmMonoBehaviourReference.Ressources.faith -= spellDataContainer[spellName][1];
    }
}
