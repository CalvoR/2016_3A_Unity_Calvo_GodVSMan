using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;

/// <summary>
/// Objet ou ressource à identifiant unique
/// </summary>
public class Item {

    #region Attributs

    public GameObject go;

    private string name;
    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    private string spritePath;
    public string SpritePath
    {
        get { return spritePath; }
        set { spritePath = value; }
    }

    private ItemType type;
    public ItemType Type
    {
        get { return type; }
        set { type = value; }
    }

    public bool Equipped;
    public gvmAnimations animations;
    private Dictionary<string, int> bonus;
    public Dictionary<string, int> Bonus
    {
        get { return bonus; }
        set { bonus = value; }
    }


    #endregion

    #region Constructeurs

    public Item() {
        Equipped = false;
    }

    public Item(string name, ItemType type, Dictionary<string, int> bonus, string spritePath)
    {
        Name = name;
        Type = type;
        Bonus = bonus;
        SpritePath = spritePath;
        Equipped = false;
    }
        // Constructeur utilisant un objet de référence chargé dans le XML
    public Item (Item itemModel)
    {
        Name = itemModel.Name;
        Type = itemModel.Type;
        Bonus = itemModel.Bonus;
        SpritePath = itemModel.SpritePath;
        Equipped = false;
    }
   
    #endregion

    #region Méthodes

    /// <summary>
    /// Renvoie le nom du type de l'item envoyé en paramètre afin de l'ajouter dans l'inventaire
    /// </summary>
    /// <param name="targetObject"></param>
    /// <returns></returns>
    public static string[] GetItemInfosFromGameObject (GameObject targetObject)
    {
        if (targetObject != null)
        {
            if (targetObject.CompareTag("wood_resource"))
                return new string[] { "Wood", ((int)ItemType.resource).ToString() };

            else if (targetObject.CompareTag("steel_resource"))
                return new string[] { "Steel", ((int)ItemType.resource).ToString() };

            else if (targetObject.CompareTag("sword_weapon"))
                return new string[] { "SteelSword", ((int)ItemType.weapon).ToString() };

            else if (targetObject.CompareTag("defencePotion_consumable"))
                return new string[] { "DefencePotion", ((int)ItemType.consumable).ToString() };

            else if (targetObject.CompareTag("Relic"))
            {
                gvmRelicManager.hasToUpdate = true;
                return new string[] { "Relic", null };
            }
        }

        return null;
    }

    #endregion

    public void unequip() {
        Debug.LogError("Unequip");
        go.SetActive(false);
        Equipped = false;
        animations = null;
    }

    public void equip(Transform position) {
        go.transform.SetParent(position);
        go.transform.position = Vector3.zero;
        go.transform.localPosition = Vector3.zero;
        go.transform.rotation = Quaternion.identity;
        animations = go.GetComponent<gvmAnimations>();
        Equipped = true;
        go.SetActive(true);
    }
}



[Flags]
public enum ItemType
{
    weapon = 0,

    field = 1,

    consumable = 2,

    resource = 3,

    other = 4
}