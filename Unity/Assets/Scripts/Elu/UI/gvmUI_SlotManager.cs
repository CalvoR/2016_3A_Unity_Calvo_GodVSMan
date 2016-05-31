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

    //// UI des informations sur l'item
    //[SerializeField]
    //public GameObject itemInfosObject;

    // UI text des informations sur l'item
    [SerializeField]
    public Text itemInfosText;   

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
        if (DataSlot.IsEmpty)
            return;
        if (itemInfosText == null || DataSlot.Item == null)
        {
            Debug.Log("Reference to item details UI or the item data slot is null.");
            return;
        }

        itemInfosText.enabled = false;

        //itemInfosObject.SetActive(true);

        itemInfosText.transform.position = new Vector2(image.transform.position.x - 15 , image.transform.position.y + 15);
        itemInfosText.text = DataSlot.Item.Name+"\n Type: "+ DataSlot.Item.Type;
        foreach (string field in DataSlot.Item.Bonus.Keys)
            itemInfosText.text += "\n" + field + ": " + DataSlot.Item.Bonus[field];       
        
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
