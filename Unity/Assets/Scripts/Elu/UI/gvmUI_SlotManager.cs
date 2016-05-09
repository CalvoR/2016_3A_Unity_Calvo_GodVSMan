using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using InventoryManagement;
using System;

/// <summary>
/// Classe de gestion d'un slot d'inventaire côté GUI
/// </summary>
public class gvmUI_SlotManager : MonoBehaviour {

    // Id du slot dans l'inventaire
    [SerializeField]
    public int slotId;

    // Image du contenu du slot
    [SerializeField]
    public Image image;
    

    // Slot en mémoire associé à ce slot UI
    private InventorySlot dataSlot;

    public InventorySlot DataSlot
    {
        get  { return dataSlot; } 
        set { dataSlot = value; }
    }


    void Start()
    {
        gameObject.SetActive(false);
                            
        // Association de l'objet UI avec son slot mémoire
        if (slotId > -1)
            dataSlot = tag.Equals("ItemShortcutSlot") ? Inventory.shorcutSlots[slotId] : Inventory.slots[slotId];
        else
            dataSlot = (slotId == -1) ? Inventory.leftHand : ((slotId == -2) ? Inventory.rightHand : new InventorySlot());

        if (dataSlot != null)
            dataSlot.UI_SlotName = name; 
    }

	public void OnMouseHover ()
    {
        //print("OnMouseHover");
    }

    
    /// <summary>
    ///  Charge l'image d'un slot selon le path indiqué dans le fichier XML
    /// </summary>
    public void LoadImage()
    {
        try { 
        if (DataSlot.Item == null || image == null)
            return;

            image.sprite = Resources.Load <Sprite> (DataSlot.Item.SpritePath);
        }
        catch (Exception e)
        {
            Debug.Log("Fail in the load of a slot item picture.\n" + e.Message);
            return;
        }
    }
}
