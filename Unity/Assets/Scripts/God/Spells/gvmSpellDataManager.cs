using UnityEngine;
using System.Collections.Generic;
using System.Xml;

public class gvmSpellDataManager : MonoBehaviour {

    [SerializeField]
    private TextAsset xmlSpellDataFile;
    //Key == spellName / Value == list of every spell data 
    public Dictionary<string, List<int>> spellDataContainer = new Dictionary<string, List<int>>();

    void Awake() {
        gvmMonoBehaviourReference.spellContainer = this;
        initialiseSpellDataContainer();
    }

    //get specific spell (spellName) data from SpellData.xml and put it in spellDataContainer
    public void initialiseSpellDataContainer() {
        List<int> listData = new List<int>();
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlSpellDataFile.text);

        XmlNodeList spellList = xmlDoc.GetElementsByTagName("spell");
        foreach (XmlNode spell in spellList) {
            XmlNodeList spellData = spell.ChildNodes;
            foreach (XmlNode value in spellData) {
                if (value != spellData.Item(0)) {
                    listData.Add(int.Parse(value.InnerText));
                }
            }
            spellDataContainer.Add(spellData.Item(0).InnerText, listData);
        }
    }
}
