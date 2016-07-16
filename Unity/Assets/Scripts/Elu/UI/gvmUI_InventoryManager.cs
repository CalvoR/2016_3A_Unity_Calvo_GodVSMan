using UnityEngine;
using System.Collections;
using InventoryManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Networking;

/// <summary>
/// Classe de gestion de l'inventaire côté GUI
/// </summary>
public class gvmUI_InventoryManager : NetworkBehaviour {
    
    [SerializeField]
    Image image;

    // Liste des objets UI de chaque slot
    private List<gvmUI_SlotManager> UI_SlotsList;

    // UI des informations sur l'item
    [SerializeField]
    public GameObject itemInfosBackground;

    [SerializeField]
    public GameObject playing_ShortcutSlots;
    [SerializeField]
    private GameObject DefaultSlots;
    [SerializeField]
    private GameObject ShortcutSlots;
    [SerializeField]
    private GameObject HandSlots;

    private bool isVisible;
    
    void Awake()
    {
        Inventory.InitSlotsTable();
        isVisible = false;
        UI_SlotsList = new List<gvmUI_SlotManager>();
        for (int i = 0; i < DefaultSlots.transform.childCount; i++) {
            UI_SlotsList.Add(DefaultSlots.transform.GetChild(i).GetComponent<gvmUI_SlotManager>());
        }
        for (int i = 0; i < ShortcutSlots.transform.childCount; i++) {
            UI_SlotsList.Add(ShortcutSlots.transform.GetChild(i).GetComponent<gvmUI_SlotManager>());
        }
        for (int i = 0; i < HandSlots.transform.childCount; i++) {
            UI_SlotsList.Add(HandSlots.transform.GetChild(i).GetComponent<gvmUI_SlotManager>());
        }
    }
    
    void Update () {
        if (Inventory.leftHand.Item.Equipped && Input.GetMouseButtonDown(0))
        {
            Inventory.leftHand.Item.animations.animation1.Play();
        }
        
        if (Input.GetKeyDown("e"))          // Ouverture / cache de l'inventaire
        {
            image.enabled = (isVisible = !isVisible);
            playing_ShortcutSlots.SetActive(!isVisible);
            for (int i = 0; i < UI_SlotsList.Count; i++) {
                UI_SlotsList[i].SetActive(isVisible);
                if (isVisible) {
                    UI_SlotsList[i].LoadImage();
                }
            }

            if (!isVisible)         // La fenêtre d'info d'un item est cachée si l'inventaire est fermé, et les raccourcis en jeu cachés si l'inventaire est ouvert
                itemInfosBackground.SetActive(false);
        }
	}


    //private void InitSlots()
    //{
    //    if (slotPrefab == null) return;

    //    int slotNb = 0;
    //    Vector2 currentSlotPosition = new Vector2(firstSlotPosition.x, firstSlotPosition.y);

    //    for (int i = 0; i < rowNb; i++)
    //    {
    //        for (int j = 0; j < colNb; j++, slotNb++)
    //        {
    //            // Génère le slot actuel
    //            GameObject currentSlot = Instantiate(slotPrefab, Vector3.zero, Quaternion.identity) as GameObject;
    //            currentSlot.transform.SetParent(transform);
    //            currentSlot.SetActive(false);
    //            currentSlot.name = "Slot" + slotNb;

    //            // association du slot" visuel" côté UI avec le slot "data" côté classe
    //            Inventory.slots[slotNb].UI_SlotName = currentSlot.name;
    //            if (currentSlot.GetComponent<gvmUI_SlotManager>() != null)
    //            {
    //                currentSlot.GetComponent<gvmUI_SlotManager>().DataSlot = Inventory.slots[slotNb];
    //                UI_Slots.Add(currentSlot, currentSlot.GetComponent<gvmUI_SlotManager>());
    //            }
                
    //            // Définit sa position
    //            RectTransform slotRT = currentSlot.GetComponent<RectTransform>();
    //            slotRT.localPosition = new Vector3(currentSlotPosition.x, currentSlotPosition.y, 0);
    //            currentSlotPosition.x += slotOffset;

    //        }
    //        // repositionnement au début de la ligne suivante avant de générer de nouveaux slots
    //        currentSlotPosition.x = firstSlotPosition.x;
    //        currentSlotPosition.y -= slotOffset;    
    //    }
        
    //}
}
