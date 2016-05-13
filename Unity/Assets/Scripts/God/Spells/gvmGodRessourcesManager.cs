using UnityEngine;
using System.Collections;

public class gvmGodRessourcesManager : MonoBehaviour {

    [SerializeField]
    public int faith = 0;
    [SerializeField]
    public int fear = 0;
    [SerializeField]
    public int faithPerSeconds = 0;
    [SerializeField]
    public int fearPerSeconds = 0;

    void updateRessources() {
        fear += fearPerSeconds;
        faith += faithPerSeconds;
    }

    void Awake() {
        gvmMonoBehaviourReference.Ressources = this;
        InvokeRepeating("updateRessources", 2, 1f);
    }

    public void useRessourcesForCastedSpell(string spellName) {
        var spellContainer = gvmSpellContainer.Load("SpellData");
        foreach (gvmSpellData spellData in spellContainer.spells) {
            if (spellData.name == spellName) {
                fear -= spellData.fearCost;
                faith -= spellData.faithCost;
            }
        }
    }
}
