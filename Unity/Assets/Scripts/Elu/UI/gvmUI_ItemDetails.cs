using UnityEngine;
using System.Collections;

public class gvmUI_ItemDetails : MonoBehaviour {

    // UI image des informations sur l'item
    [SerializeField]
    public GameObject itemInfosBackground;

    public void OnMouseExit()
    {
        itemInfosBackground.SetActive(false);
    }
}
