using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class gvmGodRessourcesManager : NetworkBehaviour {
    
    private int fear = 400;
    [SerializeField]
    private int fearPerSeconds = 0;
    [SerializeField]
    public Text FearText;

    
    private gvmSpellContainer resourcesContainer;
    public int NPCCounter;
    public int FearfulNPCCounter = 0;

    void Start() {
        gvmMonoBehaviourReference.Ressources = this;
        resourcesContainer = gvmSpellContainer.Load("SpellData");
        if (isServer) {
            InvokeRepeating("updateRessources", 2, 1f);
        }
    }
    
    void updateRessources() {
        fear += fearPerSeconds;
        RpcSetGodResources(fear);
    }
    
    public void setResourcesPerSeconds(int res)
    {
        fearPerSeconds += res;
    }

    public void useRessourcesForCastedSpell(string spellName) {
        for (var i = 0; i < resourcesContainer.spells.Count; i++) {
            if (resourcesContainer.spells[i].prefab+"(Clone)" == spellName) {
                fear -= resourcesContainer.spells[i].cost;
            }
        }
        RpcSetGodResources(fear);
    }
    
    [ClientRpc]
    public void RpcSetGodResources(int _fear)
    {
        fear = _fear;
        FearText.text = fear.ToString();
    }
}
