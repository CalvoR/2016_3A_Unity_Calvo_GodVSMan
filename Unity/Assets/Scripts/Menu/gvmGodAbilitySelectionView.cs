using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

public class gvmGodAbilitySelectionView : MonoBehaviour {

    [SerializeField]
    private Text[] abilitiesFaithTextArea;
    [SerializeField]
    private Text[] abilitiesFearTextArea;
    [SerializeField]
    private Toggle[] fearToggleList;
    [SerializeField]
    private Toggle[] faithToggleList;
    [SerializeField]
    private TextAsset xmlPlayerPreferencesFile;
    private string path = "PlayerPreferences";
    gvmSpellContainer playerBuildContainer;

    void Start() {
        playerBuildContainer = gvmSpellContainer.Load(path);
        int faithCounter = 0;
        int fearCounter = 0;
        foreach (gvmSpell spell in gvmMonoBehaviourReference.spellContainer.spellDataContainer.spells) {
            if (spell.faithCost >= 1) {
                abilitiesFaithTextArea[faithCounter].text = spell.spellName;
                faithToggleList[faithCounter].isOn = IsSelected(playerBuildContainer.spells, spell.spellName);
                faithCounter++;
            } else {
                abilitiesFearTextArea[fearCounter].text = spell.spellName;
                fearToggleList[fearCounter].isOn = IsSelected(playerBuildContainer.spells, spell.spellName);
                fearCounter++;
            }
        }
    }

    void OnDisable() {
        gvmSpellContainer.Save(path, playerBuildContainer);
        
    }

    bool IsSelected(List<gvmSpell> spellList, string spellName) {
        for(int i = 0; i < spellList.Count; i++) {
            if (spellName == spellList[i].spellName) {
                return true;
            }
        }
        return false;
    }

    public void changeSpellInPLayerSpellContainer(Text toggleText) {
        int spellIndex = playerBuildContainer.spells.FindIndex(spell => spell.spellName == toggleText.text);
        if (spellIndex != -1) {
            playerBuildContainer.spells.RemoveAt(spellIndex);
        } else {
            playerBuildContainer.spells.Add(new gvmSpell());
            playerBuildContainer.spells[playerBuildContainer.spells.Count-1].spellName = toggleText.text;
        }
    }
}
