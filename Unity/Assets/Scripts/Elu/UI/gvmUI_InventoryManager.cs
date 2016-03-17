using UnityEngine;
using System.Collections;
using InventoryManagement;
using UnityEngine.UI;

/// <summary>
/// Classe de gestion de l'inventaire côté GUI
/// </summary>
public class gvmUI_InventoryManager : MonoBehaviour {

    [SerializeField]
    GameObject slotPrefab;

    [SerializeField]
    Vector2 firstSlotPosition;

    [SerializeField]
    Image image;

    // Nécessité: rowCount*colsCount <= Inventory.SLOTS_NBR
    private int rowCount = 5;
    private int colsCount = 5;

    // écart entre les positions x et entre les positions y de deux slots successifs
    private int slotOffset = 60;

    private bool isVisible;

    void Start()
    {
        Inventory.InitSlotsTable();
        InitSlots();
        isVisible = false;

    }
	void Update () {

        if (Input.GetKeyDown("e"))
        {
            isVisible = !isVisible;
            image.enabled = isVisible;
            for (var i = 0; i < transform.childCount; ++i)
                transform.GetChild(i).gameObject.SetActive(isVisible);
        }
	}


    private void InitSlots()
    {
        if (slotPrefab == null) return;

        int slotNb = 0;
        Vector2 currentSlotPosition = new Vector2(firstSlotPosition.x, firstSlotPosition.y);

        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < colsCount; j++, slotNb++)
            {
                // Génère le slot actuel
                GameObject currentSlot = Instantiate(slotPrefab, Vector3.zero, Quaternion.identity) as GameObject;
                currentSlot.transform.SetParent(transform);
                currentSlot.SetActive(false);
                currentSlot.name = "Slot" + slotNb;

                // association du slot" visuel" côté UI avec le slot "data" côté classe
                Inventory.slots[slotNb].UI_SlotName = currentSlot.name;
                if (currentSlot.GetComponent<gvmUI_SlotManager>() != null)
                    currentSlot.GetComponent<gvmUI_SlotManager>().DataSlot = Inventory.slots[slotNb];

                // Définit sa position
                RectTransform slotRT = currentSlot.GetComponent<RectTransform>();
                slotRT.localPosition = new Vector3(currentSlotPosition.x, currentSlotPosition.y, 0);
                currentSlotPosition.x += slotOffset;
            }
            // repositionnement au début de la ligne suivante avant de générer de nouveaux slots
            currentSlotPosition.x = firstSlotPosition.x;
            currentSlotPosition.y -= slotOffset;    
        }
        
    }
}
