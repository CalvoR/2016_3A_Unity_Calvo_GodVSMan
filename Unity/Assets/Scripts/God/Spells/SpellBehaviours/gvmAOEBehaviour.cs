using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class gvmAOEBehaviour : NetworkBehaviour {
    
    private int floorMask;
    [SyncVar]
    private int areaCounter;
    private bool spellCasted = false;
    private float camRayLength = 100f;
    private Camera GodCamera;
    [SerializeField]
    private GameObject[] AOEContainer;
    [SerializeField]
    private gvmUIDataContainer SpellData;
    private gvmGodRessourcesManager resourcesUI;
    
    void Start() {
        if (hasAuthority) {
            GodCamera = GameObject.FindGameObjectWithTag("GodCamera").GetComponent<Camera>();
            floorMask = LayerMask.GetMask("Floor");
        }
        if (isServer) {
            resourcesUI = GameObject.FindGameObjectWithTag("AvatarPoolManager").GetComponent<gvmGodRessourcesManager>();
        }
        for (int i = 0; i < AOEContainer.Length; i++) {
            AOEContainer[i].transform.parent = null;
            Debug.Log(SpellData.behaviour);
            AOEContainer[i].GetComponent<gvmSpellCollider>().Init(SpellData);
        }
        gameObject.SetActive(false);
        Debug.LogError("Start");
    }
    
    void Update() {
        if (hasAuthority) {
            if (Input.GetMouseButtonDown(1)) {
                disableSpell();
            }
            if (spellCasted == false) {
                updateSpellPosition();
                if (Input.GetMouseButtonDown(0)) {
                    spellCasted = true;
                    CmdCastSpell(transform.position);
                    //spellEffect(transform.position);
                }
            }
        }
    }
    
    public void updateSpellPosition() {
        Ray camRay = GodCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask)) {
            Vector3 spellPosition = floorHit.point;
            spellPosition.y = 0f;
            transform.position = spellPosition;
        }
    }
    
    [Command]
    public void CmdCastSpell(Vector3 pos) {
        resourcesUI.useRessourcesForCastedSpell(gameObject.name);
        AOEContainer[areaCounter].transform.position = pos;
        AOEContainer[areaCounter].SetActive(true);
        RpcCastSpell(pos);
        areaCounter++;
        if (areaCounter == AOEContainer.Length) {
            areaCounter = 0;
        }
        disableSpell();
    }
    
    [ClientRpc]
    void RpcCastSpell(Vector3 pos) {
        AOEContainer[areaCounter].transform.position = pos;
        AOEContainer[areaCounter].SetActive(true);
        disableSpell();
        //gvmMonoBehaviourReference.Ressources.useRessourcesForCastedSpell(gameObject.tag);
    }
    
    public void disableSpell() {
        spellCasted = false;
        gameObject.transform.position = Vector3.up * -1000;
        gameObject.SetActive(false);
    }
    
    
}
