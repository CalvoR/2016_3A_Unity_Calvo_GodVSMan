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


        /// <summary>
        /// Remplit les tableau de slots d'emplacements vides
        /// </summary>
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
            int slotId = 0;
            InventorySlot currentSlot;

            // Initialisation du numéro de slot à remplir
            while (slotId < slots.Count())
            {
                if (slotId >= SLOTS_NBR)
                {
                    Debug.Log("No empty slot can be found in Inventory");
                    return;
                }

                currentSlot = slots[slotId];

                if (currentSlot.IsEmpty)
                    break;
                else if(currentSlot.Item.Name.Equals(itemToAdd.Name) && currentSlot.Amount < 100) {
                    currentSlot.Amount += 1;       // Si un ojet du même type a été trouvé, on incrémente juste la quantité
                    return;
                }
                slotId++;
            }

            slots[slotId].FillSlot(itemToAdd);    
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

        public SlotType Type{
            get;
            set;
        }


        // Constructeur
        public InventorySlot()
            {
                IsEmpty = true;
                Amount = 0;
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
