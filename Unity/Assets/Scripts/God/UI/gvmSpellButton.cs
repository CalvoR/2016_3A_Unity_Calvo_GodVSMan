using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


public class gvmSpellButton : NetworkBehaviour {

    private GameObject prefab;
    [SerializeField]
    private Text spellName;
    private gvmSpell spellAttached;

    public void initialise(gvmSpellData data) {
        spellName.text = data.name;
        spellAttached = new gvmSpell(data, this);
    }

    public void OnClickBehaviour()
    {
        spellAttached.spellContainer.SetActive(true);
    }

    void Update ()
    {        
        if (Input.GetButtonDown(gameObject.name))
            spellAttached.spellContainer.SetActive(true);
    }

    [Command]
    public void CmdSpawn(GameObject toSpawn) {

        NetworkServer.Spawn(toSpawn);

        //NetworkServer.SpawnWithClientAuthority(spellContainer, connectionToClient);
    }
}
