using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class gvmGodAbilitySelectionView : MonoBehaviour {
    
    [SerializeField]
    private Text[] abilitiesFaithTextArea;
    [SerializeField]
    private Text[] abilitiesFearTextArea;

    void Start() {
        int faithCounter = 0;
        int fearCounter = 0;
        foreach (KeyValuePair<string, List<int>> spell in gvmMonoBehaviourReference.spellContainer.spellDataContainer) {
            Debug.Log(spell.Key.ToString()+" : "+spell.Value[]);
            if(spell.Value[1] == 0) {
                abilitiesFaithTextArea[faithCounter].text = spell.Key.ToString();
                faithCounter++;
            } else {
                abilitiesFearTextArea[fearCounter].text = spell.Key.ToString();
                fearCounter++;
            }
        }
    }
}
