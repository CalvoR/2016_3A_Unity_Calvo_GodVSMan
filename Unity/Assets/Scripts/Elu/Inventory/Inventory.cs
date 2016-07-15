using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine.Networking;

namespace InventoryManagement {

    /// <summary>
    /// Inventaire comportant plusieurs slots, associé à un objet ou non
    /// </summary>
    public static class Inventory
    {
        #region Attributs

        const int SLOTS_NBR = 30;
        const int SHORTCUTSLOTS_NBR = 6;

        public static InventorySlot leftHand = new InventorySlot();
        public static InventorySlot rightHand = new InventorySlot();

        public static InventorySlot[] slots = new InventorySlot[SLOTS_NBR];
        public static InventorySlot[] shorcutSlots = new InventorySlot[SHORTCUTSLOTS_NBR];

        #endregion

        #region Méthodes

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
        /// Attribue un item à un slot déjà existant
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="spritePath"></param>
        public static void AddItem(Item itemToAdd, GameObject go)
        {
            itemToAdd.go = go;
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
                else if (currentSlot.Item.Name.Equals(itemToAdd.Name) && currentSlot.Amount < 100)
                {
                    currentSlot.Amount += 1;       // Si un ojet du même type a été trouvé, on incrémente juste la quantité
                    return;
                }
                slotId++;
            }

            slots[slotId].FillSlot(itemToAdd);
        }

        /// <summary>
        /// Equipe ou déséquipe une arme selon le booléen envoyé en paramètres
        /// </summary>
        /// <param name="weapon"></param>
        /// <param name="ToDisarm"></param>
        public static void EquipWeapon(Item weapon, bool ToDisarm)
        {
            Debug.LogError("Equip: " + ToDisarm);
            if (weapon == null)
                return;

            int toDisarmCoef = (ToDisarm) ? -1 : 1;

            foreach (string key in weapon.Bonus.Keys)
            {
                switch (key)
                {
                    case "life":            HeroStats.Life += weapon.Bonus[key] * toDisarmCoef;
                        break;
                    case "attack":          HeroStats.Attack += weapon.Bonus[key] * toDisarmCoef;
                        break;
                    case "defence":         HeroStats.Defense += weapon.Bonus[key] * toDisarmCoef;
                        break;
                    case "speed":           HeroStats.Speed += (weapon.Bonus[key]* HeroStats.Speed) / 100.0f * toDisarmCoef;
                        break;
                    case "endurance":       HeroStats.Endurance += weapon.Bonus[key] * toDisarmCoef;
                        break;
                }
            }
        }
        
        /// <summary>
        /// Applique les bonus d'un objet
        /// </summary>
        /// <param name="item"></param>
        /// <param name="timer"></param>
        /// <param name="toRebootStats"></param>
        public static void UseConsumable (Item item, ref float timer, bool toRebootStats)
        {
            if (item == null || (!item.Type.Equals(ItemType.consumable)) )
                return;
                
            int toRebootStatsCoef = (toRebootStats) ? -1 : 1;
                
            foreach (string key in item.Bonus.Keys)
            {
                switch (key)
                {
                    case "life":            HeroStats.Life += item.Bonus[key] * toRebootStatsCoef;
                        break;
                    case "attack":          HeroStats.Attack += item.Bonus[key] * toRebootStatsCoef;
                        break;
                    case "defence":         HeroStats.Defense += item.Bonus[key] * toRebootStatsCoef;
                        break;
                    case "speed":           HeroStats.Speed += (item.Bonus[key]* HeroStats.Speed) / 100.0f * toRebootStatsCoef;
                        break;
                    case "endurance":       HeroStats.Endurance += item.Bonus[key] * toRebootStatsCoef;
                        break;
                }
            }
            
            if (!toRebootStats)     // Timer mis à la date courante pour annoncer le début de la période de boost
                timer = Time.time;
        }
        


        /// <summary>
        /// Créée un nouvel item associé au slot destinataire et répartie la quantité totale sur le slot source et celui-ci (par réflexion)
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dest"></param>
        public static void SeparateItemStack(InventorySlot src, InventorySlot dest)
        {
            if (src.Amount < 2 || src == null || dest == null)
                return;

            BindingFlags reflectionFlags = BindingFlags.Instance | BindingFlags.Public;

            PropertyInfo pi = dest.GetType().GetProperty("IsEmpty", reflectionFlags);
            pi.SetValue(dest, src.IsEmpty, null);

            pi = dest.GetType().GetProperty("Item", reflectionFlags);
            pi.SetValue(dest, src.Item, null);

            pi = dest.GetType().GetProperty("Amount", reflectionFlags);
            pi.SetValue(dest, src.Amount/2, null);

            pi = dest.GetType().GetProperty("Type", reflectionFlags);
            pi.SetValue(dest, src.Type, null);


            src.GetType().GetProperty("Amount", reflectionFlags).SetValue(src, src.Amount / 2 + ((src.Amount % 2 == 0) ? 0 : 1), null);
        }

        /// <summary>
        /// A partir de deux références, copie les propriétés d'un slot vers un autre par réflexion
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dest"></param>
        /// <param name="isToMerge"> Si mis à vrai, rassembler deux même objets dans une seule stack</param>
        public static void MoveOrMergeItem(InventorySlot src, InventorySlot dest, bool isToMerge)
        {
            if (src == null || dest == null)
                return;

            BindingFlags reflectionFlags = BindingFlags.Instance | BindingFlags.Public;

            dest.GetType().GetProperty("IsEmpty", reflectionFlags).SetValue(dest, src.IsEmpty, null);
            dest.GetType().GetProperty("Item", reflectionFlags).SetValue(dest, src.Item, null);
            dest.GetType().GetProperty("Amount", reflectionFlags).SetValue(dest, src.Amount + (isToMerge ? dest.Amount : 0) , null);
            dest.GetType().GetProperty("Type", reflectionFlags).SetValue(dest, src.Type, null);

                // le slot source est réinitialisé
            src.GetType().GetProperty("IsEmpty", reflectionFlags).SetValue(src, true, null);
            src.GetType().GetProperty("Item", reflectionFlags).SetValue(src, new Item(), null);
            src.GetType().GetProperty("Amount", reflectionFlags).SetValue(src, 0, null);
            src.GetType().GetProperty("Type", reflectionFlags).SetValue(src, 0, null);
        }

        /// <summary>
        /// Echange la position de deux items par réflexion
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        public static void ExchangeItems(InventorySlot src, InventorySlot dest)
        {
            if (src == null || dest == null)
                return;

            BindingFlags reflectionFlags = BindingFlags.Instance | BindingFlags.Public;
            InventorySlot tempDest = new InventorySlot(dest.IsEmpty, dest.Item, dest.Amount, dest.Type);

                // Slot destinataire rempli avec les données du slot source
            dest.GetType().GetProperty("IsEmpty", reflectionFlags).SetValue(dest, src.IsEmpty, null);
            dest.GetType().GetProperty("Item", reflectionFlags).SetValue(dest, src.Item, null);
            dest.GetType().GetProperty("Amount", reflectionFlags).SetValue(dest, src.Amount, null);
            dest.GetType().GetProperty("Type", reflectionFlags).SetValue(dest, src.Type, null);
       
                // Slot source rempli avec les données du slot destinataire gardées temporairement
            src.GetType().GetProperty("IsEmpty", reflectionFlags).SetValue(src, tempDest.IsEmpty, null);
            src.GetType().GetProperty("Item", reflectionFlags).SetValue(src, tempDest.Item, null);
            src.GetType().GetProperty("Amount", reflectionFlags).SetValue(src, tempDest.Amount, null);
            src.GetType().GetProperty("Type", reflectionFlags).SetValue(src, tempDest.Type, null);
        }
        
        #endregion

    }

    /// <summary>
    /// Slot de placement d'un objet ou d'une ressource dans l'inventaire
    /// </summary>
    public class InventorySlot
    {

        // Propriétés

        public bool IsEmpty {
            get;
            set;
        }

        public Item Item {
            get;
            set;
        }

        public int Amount {
            get;
            set;
        }

        public SlotType Type {
            get;
            set;
        }
        
        // Constructeurs
        public InventorySlot()
            {
                IsEmpty = true;
                Amount = 0;
                Item = new Item();
            }

        public InventorySlot(bool isEmpty, Item item, int amount, SlotType type)
        {
            IsEmpty = isEmpty;
            Item = item;
            Amount = amount;
            Type = type;
        }


        /// <summary>
        /// Remplit le slot avec l'objet souhaité
        /// </summary>
        /// <param name="itemToAdd"></param>
        public void FillSlot (Item itemToAdd)
        {
            if (!IsEmpty && !Item.Name.Equals(itemToAdd.Name))
            {
                Debug.Log("Trying to add a different item to an already filled slot");
                return;
            }

            IsEmpty = false;
            Amount++;
            Item = itemToAdd;     
        }

    }

}
