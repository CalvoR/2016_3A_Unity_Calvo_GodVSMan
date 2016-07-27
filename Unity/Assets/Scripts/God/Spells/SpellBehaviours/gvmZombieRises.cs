using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class gvmZombieRises : NetworkBehaviour
{
    
    private gvmZombiesManager NPCContainer;
    [SerializeField]
    private gvmUIDataContainer SpellData;
    private gvmGodRessourcesManager resourcesUI;
    //private WaitForSeconds castTime;
    private bool spellCasted = false;
    [SerializeField] private GameObject visual;

    void Start() {
        NPCContainer = GameObject.FindGameObjectWithTag("NPCContainer").GetComponent<gvmZombiesManager>();
        //castTime = new WaitForSeconds(SpellData.castTime);
        spellCasted = false;
        gameObject.SetActive(false);
    }

    void Update() {
        if (hasAuthority) {
            visual.SetActive(true);
            if (Input.GetMouseButtonDown(1)) {
                disableSpell();
            }
            if (spellCasted == false) {
                if (Input.GetMouseButtonDown(0)) {
                    spellCasted = true;
                    CmdCastSpell();
                }
            }
        }
    }

    [Command]
    void CmdCastSpell(){
        NPCContainer.RiseZombies();
    }

    public void disableSpell() {
        if (hasAuthority) {
            CmdDisableSpell();
        }
        spellCasted = false;
        gameObject.SetActive(false);
    }

    [Command]
    public void CmdDisableSpell() {
        spellCasted = false;
        gameObject.SetActive(false);
    }
}
