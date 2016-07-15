using UnityEngine;
using UnityEngine.UI;
using InventoryManagement;
using System.Linq;

public class gvmUI_PlayingShortcuts : MonoBehaviour {

        // Images des objets UI slots affichée pendant le jeu
    [SerializeField]
    Image[] playingShortcutsImages;

    // Ensemble des slot "data" de raccourci, dans l'inventaire
    public InventorySlot[] shortcutSlots;

    float boostTimer;
    bool isBoostActive;
    Item boostItem;

    const float BOOST_DURATION = 5.0f;


    void Start () {
        shortcutSlots = Inventory.shorcutSlots;
        boostTimer = 0.0f;
        isBoostActive = false;

        // A ENLEVER
        Inventory.AddItem(DefaultItemsList.ItemList[ItemType.weapon].Where(x => x.Name.Equals("SteelSword")).SingleOrDefault(), null);
        // A ENLEVER
    }
	
    void Update()
    {
        CheckShortcutsUse();
    }


    /// <summary>
    /// Activé à chaque fois que l'objet est activé
    /// </summary>
	void OnEnable () {

        if (shortcutSlots == null)
            return;

        LoadPlayingShortcutsSlots();
    }

    /// <summary>
    /// Charge chaque image des slots UI à partir des slots-raccourcis de l'Inventaire
    /// </summary>
    private void LoadPlayingShortcutsSlots()
    {
        for (int i = 0; i < shortcutSlots.Length && i < playingShortcutsImages.Length; i++)
        {
            if (shortcutSlots[i].IsEmpty || string.IsNullOrEmpty(shortcutSlots[i].Item.SpritePath))
                playingShortcutsImages[i].sprite = Resources.Load<Sprite>(string.Empty);
            else
                playingShortcutsImages[i].sprite = Resources.Load<Sprite>(shortcutSlots[i].Item.SpritePath);
        }
    }

    /// <summary>
    /// Vérification d'activation de chaque slot de raccourci rapide (1 à 6)
    /// </summary>
    public void CheckShortcutsUse()
    {
        for (var i = 1; i <= 6; i++)
        {
            if (Input.GetAxis("Shortcut_" + i) > 0)                 // parcours des 6 input associés aux slots de raccourci
                UseShortcutSlotItem(Inventory.shorcutSlots[i - 1]);
        }

        
        if (isBoostActive && Time.time > boostTimer + BOOST_DURATION)
        {
            Inventory.UseConsumable(boostItem, ref boostTimer, true);
            isBoostActive = false;
        }
    }

    /// <summary>
    /// Utilise le consommable ou équipe l'arme du slot de raccourci envoyé en paramètres
    /// </summary>
    /// <param name="slot"></param>
    public void UseShortcutSlotItem(InventorySlot slot)
    {
        if (slot.IsEmpty)
            return;

        if (slot.Item.Type.Equals(ItemType.weapon))             // S'il s'agit d'une arme, on l'équipe
        {
            if (Inventory.leftHand.IsEmpty)
                Inventory.MoveOrMergeItem(slot, Inventory.leftHand, false);
            else
                Inventory.ExchangeItems(slot, Inventory.leftHand);

            LoadPlayingShortcutsSlots();
            return;
        }

        if (isBoostActive || !slot.Item.Type.Equals(ItemType.consumable))
            return;

        Inventory.UseConsumable(slot.Item, ref boostTimer, false);
        isBoostActive = true;

        boostItem = slot.Item;      // conservation pour enlever les bonus à la fin du temps de boost
        slot.Amount--;
        if (slot.Amount <= 0)       // On en enlève un en quantité, et supprime s'il s'agissait du dernier
        {
            slot.IsEmpty = true;
            slot.Item = new Item();
        }

        LoadPlayingShortcutsSlots();
    }


}
