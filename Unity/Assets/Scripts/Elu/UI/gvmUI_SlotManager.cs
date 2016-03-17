using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using InventoryManagement;

/// <summary>
/// Classe de gestion d'un slot d'inventaire côté GUI
/// </summary>
public class gvmUI_SlotManager : MonoBehaviour {

    // Image du contenu du slot
    private Image image;
    private InventorySlot dataSlot;

    public InventorySlot DataSlot
    {
        get  { return dataSlot; } 
        set { dataSlot = value; }
    }


    void Start()
    {
        image = GetComponent<Image>();
        LoadImage();
    }


	public void OnMouseHover ()
    {
        //print("OnMouseHover");
    }

    
    public void LoadImage()
    {
        if (DataSlot.Item == null || image == null)
            return;

        image.sprite = Resources.Load<Sprite>(DataSlot.Item.SpritePath);

    }
}
