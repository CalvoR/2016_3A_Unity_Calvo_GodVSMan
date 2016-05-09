using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagement { 
    
    /// <summary>
    /// Inventaire comportant plusieurs slots, associé à un objet ou non
    /// </summary>
    public static class Inventory
    {
        const int SLOTS_NBR = 30;
        const int SHORTCUTSLOTS_NBR = 6;

        public static InventorySlot leftHand = new InventorySlot();
        public static InventorySlot rightHand = new InventorySlot();

        public static InventorySlot[] slots = new InventorySlot[SLOTS_NBR];
        public static InventorySlot[] shorcutSlots = new InventorySlot[SHORTCUTSLOTS_NBR];


        // Remplit les tableau de slots d'emplacements vides
        public static void InitSlotsTable()
        {
            for (int i = 0; i < SLOTS_NBR; i++)
            {
                slots[i] = new InventorySlot();
            }

            for (int i = 0; i < SHORTCUTSLOTS_NBR; i++)
                shorcutSlots[i] = new InventorySlot();

        }

        /// <summary>
        /// Ajoute un item à un slot déjà existant
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="spritePath"></param>
        public static void AddItem(Item itemToAdd)
        {
            int slotId = 0;                     // Initialisation du numéro de slot à remplir
            while (!slots[slotId].IsEmpty && slotId <= slots.Count() + 1)  {   
                if (slotId >= SLOTS_NBR)    {
                    Debug.Log("No empty slot can be found in Inventory");
                    return;
                }
                slotId++;
            }

            slots[slotId].FillSlot(itemToAdd);

            if (slots[slotId].UI_SlotName == null)  {
                Debug.Log("DataSlot linked to UISlot can't be found");
                return;
            }           
        }
	   
    }



    /// <summary>
    /// Slot de placement d'un objet ou d'une ressource dans l'inventaire
    /// </summary>
    public class InventorySlot
    {
        bool isEmpty;
        Item item;
        int amount;
        string UI_slotName;

        // Accesseurs et modificateurs

        public bool IsEmpty
        {
            get { return isEmpty; }
            set { isEmpty = value; }
        }

        public Item Item
        {
            get { return item; }
            set { item = value; }
        }

        public int Amount
        {
            get { return amount; }
            set { amount = value;}
        }

        public string UI_SlotName
        {
            get { return UI_slotName; }
            set { UI_slotName = value; }
        }

        // Constructeur
        public InventorySlot()
        {
            IsEmpty = true;
            Amount = 0;
            UI_SlotName = string.Empty;
        }

        // Remplit le slot avec l'objet souhaité
        public void FillSlot (Item itemToAdd)
        {
            if (!IsEmpty && !Item.Name.Equals(itemToAdd.Name))
            {
                Debug.Log("Trying to add a different item to an already filled slot");
                return;
            }

            isEmpty = false;
            Amount++;
            Item = itemToAdd;     
        }

    }

}
