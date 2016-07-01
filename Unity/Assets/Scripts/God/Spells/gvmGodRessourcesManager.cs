using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class gvmGodRessourcesManager : NetworkBehaviour {
    
    [SyncVar]
    private int faith = 100;
    [SyncVar]
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

    void Start() {
        Debug.LogError(isServer + ":" + isClient);
        Debug.LogError("Resource: ");
        gvmMonoBehaviourReference.Ressources = this;
        resourcesContainer = gvmSpellContainer.Load("SpellData");
        InvokeRepeating("updateRessources", 2, 1f);
    }

    void Update() {
        if (isServer)
        {
            faith += faithPerSeconds;
            fear += fearPerSeconds;
            Debug.LogError(fear + " : " + faith);
        }
        FearText.text = fear.ToString();
        FaithText.text = faith.ToString();
    }

    public void useRessourcesForCastedSpell(string spellName) {
        Debug.LogError(resourcesContainer.spells.Count);
        foreach (gvmSpellData spellData in resourcesContainer.spells) {
            if (spellData.behaviour+"(Clone)" == spellName) {
                Debug.LogError(spellData.behaviour + " : " + spellName);
                fear -= spellData.fearCost;
                faith -= spellData.faithCost;
            }
        }
    }
}
