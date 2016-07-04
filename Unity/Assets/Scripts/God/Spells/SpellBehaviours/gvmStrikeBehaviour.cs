using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class gvmStrikeBehaviour : NetworkBehaviour {

    private Camera GodCamera;
    private int floorMask;
    private float camRayLength = 100f;
    [SerializeField]
    private gvmUIDataContainer dataContainer;
    private bool spellCasted = false;
    public Collider spellCollider;

    void Start() {
        if (hasAuthority) {
            GodCamera = GameObject.FindGameObjectWithTag("GodCamera").GetComponent<Camera>();
            floorMask = LayerMask.GetMask("Floor");
        }
        spellCollider.GetComponent<gvmSpellCollider>().Init(dataContainer);
        spellCollider.enabled = false;
        spellCollider.transform.parent = null;
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
        spellCollider.transform.position = pos;
        spellCollider.enabled = true;
        RpcCastSpell(pos);
        disableSpell();
        //gvmMonoBehaviourReference.Ressources.useRessourcesForCastedSpell(gameObject.tag);
    }
    
    //reset spell prefab to default
    public void disableSpell() {
        gameObject.transform.position = Vector3.up * -1000;
        spellCasted = false;
        gameObject.SetActive(false);
    }

    [ClientRpc]
    void RpcCastSpell(Vector3 pos) {
        spellCollider.transform.position = pos;
        spellCollider.enabled = true;
        disableSpell();
        //gvmMonoBehaviourReference.Ressources.useRessourcesForCastedSpell(gameObject.tag);
    }
}
