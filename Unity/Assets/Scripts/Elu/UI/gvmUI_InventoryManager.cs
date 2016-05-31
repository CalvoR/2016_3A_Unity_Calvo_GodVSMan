using UnityEngine;
using System.Collections;
using InventoryManagement;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// Classe de gestion de l'inventaire côté GUI
/// </summary>
public class gvmUI_InventoryManager : MonoBehaviour {
    
    [SerializeField]
    Image image;

    [SerializeField]
    public List<GameObject> UI_SlotsList;

    // UI des informations sur l'item
    [SerializeField]
    public GameObject itemInfosBackground;    

    private bool isVisible;

    void Awake()
    {
        Inventory.InitSlotsTable();
        isVisible = false;
    }

	void Update () {

        if (Input.GetKeyDown("e"))          // Ouverture / cache de l'inventaire
        {
            image.enabled = (isVisible = !isVisible);
            foreach (GameObject UI_Slot in UI_SlotsList)
            {
                UI_Slot.SetActive(isVisible);
                if (UI_Slot.GetComponent<gvmUI_SlotManager>() != null && isVisible)
                    UI_Slot.GetComponent<gvmUI_SlotManager>().LoadImage();
            }

            if (!isVisible)         // La fenêtre d'info d'un item est cachée si l'inventaire est fermé
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
