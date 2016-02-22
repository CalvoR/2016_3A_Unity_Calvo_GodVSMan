using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;


public class gvmSpellPrefabManager : MonoBehaviour {

    private GameObject prefab;
    public Text spellName;

    void Start()
    {
        prefab = Instantiate(Resources.Load("Prefabs/God/Spells/" + gameObject.tag , typeof(GameObject))) as GameObject;
        spellName.text = gameObject.tag;
    }

    public void OnClickBehaviour()
    {
        prefab.SetActive(true);
    }

    void Update ()
    {        
        if (Input.GetButtonDown(gameObject.name))
            prefab.SetActive(true);
    }
}
