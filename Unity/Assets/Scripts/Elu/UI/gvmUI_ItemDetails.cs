using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class gvmUI_ItemDetails : MonoBehaviour {

    // UI image des informations sur l'item
    [SerializeField]
    Text itemInfosText;

    [SerializeField]
    GameObject itemInfosBackground;

    public void Start ()
    {
        itemInfosText.enabled = false;
        itemInfosBackground.SetActive(false);
    }


    public void OnMouseExit()
    {       
        itemInfosText.enabled = false;
        itemInfosBackground.SetActive(false);
    }
}
