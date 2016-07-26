using UnityEngine;
using System.Xml;
using UnityEngine.Networking;

public class gvmGodSceneManager : NetworkBehaviour {

    [SerializeField]
    TextAsset xmlPlayerPreferencesFile;

    [SerializeField]
    gvmSpellButton[] spellButtons = new gvmSpellButton[5];

    [SerializeField]
    private GameObject GodUI;

    [SerializeField]
    private GameObject EndGameVictoryUI;

    [SerializeField]
    private GameObject EndGameDefeatUI;

    [SerializeField]
    GameObject playerCamera;
    [SerializeField]
    private GameObject NoAuthorityScripts;

    gvmSpellContainer spellDataContainer;

    public override void OnStartLocalPlayer() {
        base.OnStartLocalPlayer();
        GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkManagerHUD>().enabled = false;
        if (Camera.main && Camera.main.gameObject) {
            Camera.main.gameObject.SetActive(false);
        }
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlPlayerPreferencesFile.text);

        GodUI.SetActive(true);
        initialiseSpellButtons(xmlDoc);
        playerCamera.gameObject.SetActive(true);
    }

    void Awake() {
        gvmPropertiesManager.GetInstance();
        spellDataContainer = gvmSpellContainer.Load("SpellData");
    }
    
    public void initialiseSpellButtons(XmlDocument xmlDoc) {
        XmlNodeList spellList = xmlDoc.GetElementsByTagName("spells")[0].ChildNodes;
        for(int i = 0; i < spellButtons.Length; i++) {
            CmdSpawn(i, spellList[i].LastChild.InnerText);
        }
    }

    [Command]
    public void CmdSpawn(int btnId, string spellName) {
        var go = (GameObject)Instantiate((GameObject)Resources.Load("Prefabs/God/Spells/"+spellName), transform.position, Quaternion.identity);
        go.GetComponent<gvmUIDataContainer>().init(spellDataContainer.getDataByBehaviour(spellName));

        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);

        spellButtons[btnId].spellGO = go;
        spellButtons[btnId].spellName.text = spellDataContainer.getDataByBehaviour(spellName).name;
        spellButtons[btnId].spellData = spellDataContainer.getDataByBehaviour(spellName);

        RpcSetSpellNetId(go.GetComponent<NetworkIdentity>().netId, btnId, spellName);
        spellButtons[btnId].spellGO.SetActive(true);
    }
    
    [ClientRpc]
    public void RpcSetSpellNetId(NetworkInstanceId netId, int btnId, string name) {//gvmSpellData spellData) {
        spellButtons[btnId].spellGO = ClientScene.FindLocalObject(netId);
        spellButtons[btnId].spellGO.GetComponent<gvmUIDataContainer>().init(spellDataContainer.getDataByBehaviour(name));
        spellButtons[btnId].spellName.text = spellDataContainer.getDataByBehaviour(name).name;
        spellButtons[btnId].spellData = spellDataContainer.getDataByBehaviour(name);

        spellButtons[btnId].spellGO.SetActive(true);
    }
    
    public void OnClickBehaviour(int i) {
        spellButtons[i].spellGO.SetActive(true);
        CmdSetSpellActive(i);
    }
    
    [Command]
    public void CmdSetSpellActive(int i) {
        spellButtons[i].spellGO.SetActive(true);
    }

    [ClientRpc]
    public void RpcEndTheGame(bool win) {
        if (hasAuthority) {
            GodUI.SetActive(false);
            if (win) {
                EndGameVictoryUI.SetActive(true);
                NoAuthorityScripts.SetActive(false);
            } else {
                EndGameDefeatUI.SetActive(true);
                NoAuthorityScripts.SetActive(false);
            }
        }
    }
}