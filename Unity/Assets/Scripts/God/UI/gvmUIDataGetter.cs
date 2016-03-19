using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class gvmUIDataGetter : MonoBehaviour {

    private Text text;
    
    void Awake () {
        text = GetComponent<Text>();
	}
	
	// Update fear and faith gauges
	void Update () {
        if(name == "Fear") {
            text.text = gvmMonoBehaviourReference.Ressources.fear.ToString();
        } else if (name == "Faith") {
            text.text = gvmMonoBehaviourReference.Ressources.faith.ToString();
        }
    }
}
