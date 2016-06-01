using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using InventoryManagement;
using System;

/// <summary>
/// Classe de gestion d'un slot d'inventaire côté GUI
/// </summary>
public class gvmUI_SlotManager : MonoBehaviour {

    #region Attributs

    // Id du slot dans l'inventaire
    [SerializeField]
    public int slotId;

    // Image du contenu du slot
    [SerializeField]
    public Image image;

    // UI des informations sur l'item
    [SerializeField]
    public GameObject itemInfosBackground;

    // UI text des informations sur l'item
    [SerializeField]
    public Text itemInfosText;   


    // Slot en mémoire associé à ce slot UI
    public InventorySlot DataSlot  { get; set; }

    // Oobjet du slot UI qui est drag&drop (mis à null, si aucun)
    static gvmUI_SlotManager draggedUI_Slot { get; set; }

    // Type de slot dans l'inventaire (defaut, raccourci ou main droite/gauche)
    public SlotType Type { get; set; }

    #endregion

    #region Méthodes

    void Start()
    {
        gameObject.SetActive(false);
        draggedUI_Slot = null;
        linkDataSlotsToUI();     
    }

    /// <summary>
    /// Affichage de la fenêtre d'info au passage d'un slot rempli d'un objet
    /// </summary>
	public void OnMouseHover ()
    {
        if (DataSlot.IsEmpty)
            return;
        if (itemInfosText == null || DataSlot.Item == null)
        {
            Debug.Log("Reference to item details UI or the item data slot is null.");
            return;
        }

        itemInfosBackground.SetActive(true);
        itemInfosText.enabled = true;      
       
        itemInfosBackground.transform.position = new Vector2(                   // positionnement de la fenêtre, puis remplissage du texte
            image.transform.position.x + image.rectTransform.rect.width/2, 
            image.transform.position.y + image.rectTransform.rect.height/2
                );
        itemInfosText.transform.position = new Vector2(
            itemInfosBackground.transform.position.x, 
            itemInfosBackground.transform.position.y
            );
        itemInfosText.text = DataSlot.Item.Name+"\n Type: "+ DataSlot.Item.Type + "\n Quantity: "+DataSlot.Amount.ToString();

        foreach (string field in DataSlot.Item.Bonus.Keys)
            itemInfosText.text += "\n" + field + ": " + DataSlot.Item.Bonus[field];             
    }

    /// <summary>
    /// A la sortie de la souris du slot, on ferme la fenêtre d'informations
    /// </summary>
    public void OnMouseExit ()
    {
        itemInfosText.enabled = false;
        itemInfosBackground.SetActive(false);
    }

    /// <summary>
    /// Déplacement d'un objet vers un autre slot
    /// </summary>
    public void OnMouseDrag ()
    {
        if (DataSlot.IsEmpty)
            return;
        else
        {
            Debug.Log("Drag started");
            draggedUI_Slot = this;
        }
            
    }

    /// <summary>
    /// Lorsqu'un item est relaché sur un slot
    /// </summary>
    public void OnMouseDrop ()
    {
        if (!DataSlot.IsEmpty) {
            Debug.Log("Slot is not empty");
            return;
        }

        if (draggedUI_Slot == null)       
            return;

        // remplissage du nouveau slot et mise à vide de l'ancien
        Debug.Log("Trying to fill a slot.");

        int dragSlotId = draggedUI_Slot.slotId;
        InventorySlot[] slotSrcTable = draggedUI_Slot.Type.Equals(SlotType.shortcut) ? Inventory.shorcutSlots : Inventory.slots;
        InventorySlot[] slotDestTable = Type.Equals(SlotType.shortcut) ? Inventory.shorcutSlots : Inventory.slots;

        slotDestTable[slotId] = slotSrcTable[dragSlotId];
        slotSrcTable[dragSlotId] = new InventorySlot();   
        
        // Mise à jour de l'affichage de chacun des deux slots
        linkDataSlotsToUI();
        LoadImage();
        draggedUI_Slot.linkDataSlotsToUI();
        draggedUI_Slot.image.sprite = Resources.Load<Sprite> (String.Empty); ;
        draggedUI_Slot = null;        
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

    /// <summary>
    /// Assigne le slot mémoire avec le slot UI, et définit le type du slot
    /// </summary>
    private void linkDataSlotsToUI ()
    {
        if (slotId < 0)                                 // cas des mains
        {
            if (slotId == -1)
            {
                DataSlot = Inventory.leftHand;
                Type = SlotType.leftHand;
            }
            else if (slotId == -2)
            {
                DataSlot = Inventory.rightHand;
                Type = SlotType.rightHand;
            }
        }
        else {                                          // cas des raccourcis
            if (tag.Equals("ItemShortcutSlot"))
            {
                DataSlot = Inventory.shorcutSlots[slotId];
                Type = SlotType.shortcut;
            }
            else {                                      // cas d'un slot classique
                DataSlot = Inventory.slots[slotId];
                Type = SlotType.defaultSlot;
            }
        }
    }

    #endregion
}



[Flags]
public enum SlotType
{
    defaultSlot = 0,

    shortcut = 1,

    leftHand = 2,

    rightHand = 3
}