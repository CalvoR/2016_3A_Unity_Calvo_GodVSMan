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
    [SerializeField]
    private List<gvmUI_SlotManager> UI_SlotsList;

    // UI des informations sur l'item
    [SerializeField]
    public GameObject itemInfosBackground;

    [SerializeField]
    public GameObject playing_ShortcutSlots;

    private bool isVisible;
    static public bool isInGamePause;
    
    void Awake()
    {
        Inventory.InitSlotsTable();
        isVisible = false;
        isInGamePause = false;
    }
    
    void Update ()
    {
        if (Inventory.leftHand.Item.Equipped && Input.GetMouseButtonDown(0))
        {
            Inventory.leftHand.Item.animations.animation1.Play();
        }
        
        if (Input.GetKeyDown("e") && !isInGamePause)          // Ouverture / cache de l'inventaire
        {
            image.enabled = (isVisible = !isVisible);
            playing_ShortcutSlots.SetActive(!isVisible);
            Cursor.visible = isVisible;
            Cursor.lockState = isVisible ? CursorLockMode.None : CursorLockMode.Locked;
            for (int i = 0; i < UI_SlotsList.Count; i++)
            {
                if (UI_SlotsList[i] == null)
                    continue;
                UI_SlotsList[i].SetActive(isVisible);
                if (isVisible) 
                    UI_SlotsList[i].LoadImage();             
            }

            if (!isVisible)         // La fenêtre d'info d'un item est cachée si l'inventaire est fermé, et les raccourcis en jeu cachés si l'inventaire est ouvert
                itemInfosBackground.SetActive(false);
        }
	}

}
