using UnityEngine;
using UnityEngine.UI;
using InventoryManagement;

public class gvmUI_PlayingShortcuts : MonoBehaviour {

        // Images des objets UI slots affichée pendant le jeu
    [SerializeField]
    Image[] playingShortcutsImages;

    // Ensemble des slot "data" de raccourci, dans l'inventaire
    public InventorySlot[] shortcutSlots;

    
    
	void Start () {
        shortcutSlots = Inventory.shorcutSlots;
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
            if (shortcutSlots[i].Item == null || string.IsNullOrEmpty(shortcutSlots[i].Item.SpritePath))
                continue;

            playingShortcutsImages[i].sprite = Resources.Load<Sprite>(shortcutSlots[i].Item.SpritePath);
        }
    }


}
