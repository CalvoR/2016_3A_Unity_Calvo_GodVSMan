using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using InventoryManagement;
using System;
using UnityEngine.Networking;

/// <summary>
/// Classe de gestion d'un slot d'inventaire côté GUI
/// </summary>
public class gvmUI_SlotManager : NetworkBehaviour {

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

    [SerializeField]
    private gvmPlayerControler playerControler;

    // Slot en mémoire associé à ce slot UI
    public InventorySlot DataSlot  { get; set; }

    // Oobjet du slot UI qui est drag&drop (mis à null, si aucun)
    static gvmUI_SlotManager draggedUI_Slot { get; set; }

    // Type de slot dans l'inventaire (defaut, raccourci ou main droite/gauche)
    public SlotType Type { get; set; }
    
    int dragSlotId;
    InventorySlot slotSrc, slotDest;

    #endregion

    #region Méthodes

    void Awake() { 
        gameObject.SetActive(false);
        draggedUI_Slot = null;
        linkDataSlotsToUI();     
    }

    void OnEnable() {
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
            image.transform.position.x + image.rectTransform.rect.width, 
            image.transform.position.y + image.rectTransform.rect.height 
                );
        itemInfosText.text = DataSlot.Item.Name+"\n  Type: "+ DataSlot.Item.Type + "\n  Quantity: "+DataSlot.Amount.ToString();

        foreach (string field in DataSlot.Item.Bonus.Keys)
            itemInfosText.text += "  \n" + field + ": " + DataSlot.Item.Bonus[field];             
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
            draggedUI_Slot = this;     
    }

    public void OnDrag()
    {
        draggedUI_Slot.image.transform.position = Input.mousePosition;
    }

    /// <summary>
    /// Lorsqu'un item est relaché sur un slot
    /// </summary>
    public void OnMouseDrop()
    {
        if (draggedUI_Slot == null)
            return;

        dragSlotId = draggedUI_Slot.slotId;

        UpdateSlots(out slotSrc, slotId, out slotDest, dragSlotId);

        // Mise à jour de l'affichage de chacun des deux slots
        linkDataSlotsToUI();
        LoadImage();
        draggedUI_Slot.linkDataSlotsToUI();
        draggedUI_Slot.LoadImage();
        draggedUI_Slot = null;
            // Déséquippe si le slot d'origine était une arme et était placée sur un slot de main
        if (dragSlotId < 0 && slotSrc.Item.Type.Equals(ItemType.weapon))
        {
            Inventory.EquipWeapon(slotDest.Item, false);
            playerControler.CmdUnequipItem(slotDest.Item.go);
            slotDest.Item.Equipped = false;
            slotDest.Item.animations = null;
        }
        
        // Equipe si l'objet est une arme et que le nouveau slot est une des mains
        if (slotId < 0 && slotDest.Item.Type.Equals(ItemType.weapon)) {
            Inventory.EquipWeapon(slotDest.Item, true);
            playerControler.CmdEquipItem(slotDest.Item.go, dragSlotId);
            slotDest.Item.animations = ClientScene.FindLocalObject(slotDest.Item.go).GetComponent<gvmAnimations>();
            slotDest.Item.Equipped = true;
        }
    }
    

    /// <summary>
    /// Met à jour le contenu en mémoire des slots destinataire et source envoyés en paramètres (avec leurs IDs respectifs)
    /// </summary>
    private void UpdateSlots(out InventorySlot slotSrc, int slotIdSrc, out InventorySlot slotDest, int slotIdDest)
    {
        // Associe le slot "data" correct par rapport ux IDs et types que l'on a
        if (slotIdDest < 0) {
            slotSrc = (slotIdDest == -1) ? Inventory.leftHand : Inventory.rightHand;
        }
        else
            slotSrc = draggedUI_Slot.Type.Equals(SlotType.shortcut) ? Inventory.shorcutSlots[slotIdDest] : Inventory.slots[slotIdDest];

        if (slotIdSrc < 0) {
            slotDest = (slotIdSrc == -1) ? Inventory.leftHand : Inventory.rightHand;
        }
        else
            slotDest = Type.Equals(SlotType.shortcut) ? Inventory.shorcutSlots[slotIdSrc] : Inventory.slots[slotIdSrc];


        if (Input.GetMouseButtonUp(1))          // Si clic droit, l'objet est séparé
        {
            Inventory.SeparateItemStack(slotSrc, slotDest);
        }
        else if (!slotDest.IsEmpty)                         // Si slot occupé par un objet du même type, fusion dans la même pile
        {
            if (slotDest.Item.Name.Equals(slotSrc.Item.Name))
            {
                Inventory.MoveOrMergeItem(slotSrc, slotDest, true);
                itemInfosBackground.SetActive(false);
            }
            else                                            // Sinon, les deux positions d'item sont inversés
                Inventory.ExchangeItems(slotSrc, slotDest);
        }
        else
            Inventory.MoveOrMergeItem(slotSrc, slotDest, false);
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
            Debug.LogError(gameObject.name);
            if (tag.Equals("ItemShortcutSlot"))
            {
                DataSlot = Inventory.shorcutSlots[slotId];
                Type = SlotType.shortcut;
            }
            else {                      // cas d'un slot classique
                DataSlot = Inventory.slots[slotId];
                Type = SlotType.defaultSlot;
            }
        }
    }

    public void SetActive(bool isVisible) {
        gameObject.SetActive(isVisible);
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