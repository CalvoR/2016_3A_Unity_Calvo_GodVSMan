using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

public class gvmGodAbilitySelectionView : MonoBehaviour {
    /*
    [SerializeField]
    private Text[] abilitiesFaithTextArea;
    [SerializeField]
    private Text[] abilitiesFearTextArea;
    [SerializeField]
    private Toggle[] fearToggleList;
    [SerializeField]
    private Toggle[] faithToggleList;
    [SerializeField]
    private string xmlPlayerPreferencesFile;
    gvmSpellContainer playerBuildContainer;

    void Start() {
        playerBuildContainer = gvmSpellContainer.Load(xmlPlayerPreferencesFile);
        
        var spellContainer = gvmSpellContainer.Load("SpellData");
        int fearCounter = 0;
        foreach (gvmSpellData spell in spellContainer.spells) {
            if (spell.faithCost >= 1) {
                abilitiesFaithTextArea[faithCounter].text = spell.name;
                faithToggleList[faithCounter].isOn = IsSelected(playerBuildContainer.spells, spell.name);
                faithCounter++;
            } else {
                abilitiesFearTextArea[fearCounter].text = spell.name;
                fearToggleList[fearCounter].isOn = IsSelected(playerBuildContainer.spells, spell.name);
                fearCounter++;
            }
        }
    }

    void OnDisable() {
        playerBuildContainer.Save(playerBuildContainer.spells);        
    }

    bool IsSelected(List<gvmSpellData> spellList, string spellName) {
        for(int i = 0; i < spellList.Count; i++) {
            if (spellName == spellList[i].name) {
                return true;
            }
        }
        return false;
    }

    public void changeSpellInPlayerSpellContainer(Text toggleText) {
        int spellIndex = playerBuildContainer.spells.FindIndex(spell => spell.name == toggleText.text);
        if (spellIndex != -1) {
            playerBuildContainer.spells.RemoveAt(spellIndex);
        } else {
            playerBuildContainer.spells.Add(new gvmSpellData());
            playerBuildContainer.spells[playerBuildContainer.spells.Count-1].name = toggleText.text;
        }
    }*/
}
