using UnityEngine;
using System.Xml;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;

public class gvmGodSceneManager : NetworkBehaviour {

    [SerializeField]
    private TextAsset xmlPlayerPreferencesFile;
    [SerializeField]
    private GameObject[] spellButtons = new GameObject[5];
    [SerializeField]
    private GameObject GodUI;
    
    private gvmPropertiesManager spellProperties;
    private gvmSpellContainer spellDataContainer;
    //private gvmUnitsManager unitManager;

    //public Vector3 vector3InvertYAndZAxes;
    
    [SerializeField]
    GameObject playerCamera;


    public override void OnStartLocalPlayer() {
        base.OnStartLocalPlayer();
        if (Camera.main && Camera.main.gameObject) {
            Camera.main.gameObject.SetActive(false);
        }
        //GodUI.SetActive(true);
        playerCamera.gameObject.SetActive(true);
        awake();
    }

    public void awake() {
        spellProperties = gvmPropertiesManager.GetInstance();
        spellDataContainer = gvmSpellContainer.Load("SpellData");

        //unitManager = gvmUnitsManager.GetInstance();
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlPlayerPreferencesFile.text);
        RpcInitialiseSpellButtons(xmlDoc);

        GodUI.SetActive(true);
    }
    
    
    public void RpcInitialiseSpellButtons(XmlDocument xmlDoc) {
        int btnIndex = 0;
        XmlNodeList spellList = xmlDoc.GetElementsByTagName("spells")[0].ChildNodes;

        foreach (GameObject button in spellButtons) {
            button.GetComponent<gvmSpellButton>().initialise(spellDataContainer.getDataByName(spellList[btnIndex].FirstChild.InnerText));
            btnIndex++;
        }
    }

}