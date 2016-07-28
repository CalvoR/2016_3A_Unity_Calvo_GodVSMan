using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class gvmAOEBehaviour : NetworkBehaviour {
    
    private int floorMask;
    [SyncVar]
    private int areaCounter;
    private bool spellCasted = false;
    private float camRayLength = 200f;
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
            AOEContainer[i].name = gameObject.name + i;
            AOEContainer[i].transform.parent = null;
            AOEContainer[i].GetComponent<gvmSpellCollider>().Init(SpellData);
        }
        gameObject.SetActive(false);
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
                }
            }
        }
    }
    
    public void updateSpellPosition() {
        Ray camRay = GodCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask)) {
            Vector3 spellPosition = floorHit.point;
            spellPosition.y = 200f;
            transform.position = spellPosition;
        }
    }
    
    [Command]
    public void CmdCastSpell(Vector3 pos) {
        resourcesUI.useRessourcesForCastedSpell(gameObject.name);
        RpcCastSpell(pos);
        AOEContainer[areaCounter].transform.position = pos;
        AOEContainer[areaCounter].SetActive(true);
        areaCounter++;
        if (areaCounter == AOEContainer.Length) {
            areaCounter = 0;
        }
    }
    
    [ClientRpc]
    void RpcCastSpell(Vector3 pos) {
        AOEContainer[areaCounter].transform.position = pos;
        AOEContainer[areaCounter].SetActive(true);
        disableSpell();
        //gvmMonoBehaviourReference.Ressources.useRessourcesForCastedSpell(gameObject.tag);
    }
    
    public void disableSpell() {
        if (hasAuthority) {
            CmdDisableSpell();
        }
        gameObject.transform.position = Vector3.up * -1000;
        spellCasted = false;
        gameObject.SetActive(false);
    }

    [Command]
    public void CmdDisableSpell() {
        gameObject.transform.position = Vector3.up * -1000;
        spellCasted = false;
        gameObject.SetActive(false);
    }
    
}
