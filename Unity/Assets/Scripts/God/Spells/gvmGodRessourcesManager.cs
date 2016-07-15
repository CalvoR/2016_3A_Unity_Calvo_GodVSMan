using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class gvmGodRessourcesManager : NetworkBehaviour {
    
    private int faith = 100;
    private int fear = 100;
    [SerializeField]
    private int faithPerSeconds = 0;
    [SerializeField]
    private int fearPerSeconds = 0;
    [SerializeField]
    public Text FearText;
    [SerializeField]
    public Text FaithText;
    
    private gvmSpellContainer resourcesContainer;
    public int NPCCounter = 0;
    public int FaithfulNPCCounter = 0;
    public int FearfulNPCCounter = 0;

    void Start() {
        gvmMonoBehaviourReference.Ressources = this;
        resourcesContainer = gvmSpellContainer.Load("SpellData");
        if (isServer) {
            InvokeRepeating("updateRessources", 2, 1f);
        }
    }
    
    void updateRessources() {
        faith += faithPerSeconds;
        fear += fearPerSeconds;
        RpcSetGodResources(fear, faith);
    }

    void FixedUpdate()
    {
        if (FaithfulNPCCounter * 100 / NPCCounter >= 75)
        {
            Debug.LogError("GOD WIN BY FAITH");
        }
        if (FearfulNPCCounter * 100 / NPCCounter >= 75) {
            Debug.LogError("GOD WIN BY FEAR");
        }
    }

    public void setResourcesPerSeconds(int _fear, int _faith) {
        faithPerSeconds += _faith;
        fearPerSeconds += _fear;
    }

    public void useRessourcesForCastedSpell(string spellName) {
        foreach (gvmSpellData spellData in resourcesContainer.spells) {
            if (spellData.behaviour+"(Clone)" == spellName) {
                fear -= spellData.fearCost;
                faith -= spellData.faithCost;
            }
        }
        Rpctest();
        RpcSetGodResources(fear, faith);
        Debug.LogError("use resources");
    }

    [ClientRpc]
    public void Rpctest()
    {
        Debug.LogError("rpc");
    }

    [ClientRpc]
    public void RpcSetGodResources(int _fear, int _faith)
    {
        fear = _fear;
        faith = _faith;
        FearText.text = fear.ToString();
        FaithText.text = faith.ToString();
    }
}
