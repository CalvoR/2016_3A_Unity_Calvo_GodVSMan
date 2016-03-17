using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class gvmGodAbilitySelectionView : MonoBehaviour {

    [SerializeField]
    private GameObject[] abilitiesButtons;
    [SerializeField]
    private Text[] abilitiesTextArea;

    void OnEnable() {
        int i = 0;
        foreach(KeyValuePair<string, List<int>> spellName in gvmMonoBehaviourReference.xmlRessources.spellDataContainer) {
            Debug.Log(spellName.Key.ToString());
            i++;
            abilitiesTextArea[i].text = spellName.Key.ToString();
        }
    }


}
