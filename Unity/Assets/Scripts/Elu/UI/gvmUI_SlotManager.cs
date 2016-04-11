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

    // Slot en mémoire associé à ce slot UI
    private InventorySlot dataSlot;

    public InventorySlot DataSlot
    {
        get  { return dataSlot; } 
        set { dataSlot = value; }
    }


    void Start()
    {
        image = GetComponent<Image>();
        dataSlot.UI_SlotName = name;

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

        image.sprite = Resources.Load<Sprite>("Picture/wood_resource.png");   //DataSlot.Item.SpritePath);
    }
}
