using System.Collections.Generic;
using System;
using UnityEngine;

/// <summary>
/// Objet ou ressource à identifiant unique
/// </summary>
public class Item {

    #region Attributs

    private int id;
    private string name;
    private ItemType type;
    private Dictionary<string, int> bonus;
    private string spritePath;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public ItemType Type
    {
        get { return type; }
        set { type = value; }
    }

    public Dictionary<string, int> Bonus
    {
        get { return bonus; }
        set { bonus = value; }
    }

    public string SpritePath
    {
        get { return spritePath; }
        set { spritePath = value; }
    }

    #endregion

    #region Constructeurs

    public Item() {    }

    public Item(string name, ItemType type, Dictionary<string, int> bonus, string spritePath)
    {
        Name = name;
        Type = type;
        Bonus = bonus;
        SpritePath = spritePath;
    }
        // Constructeur utilisant un objet de référence chargé dans le XML
    public Item (Item itemModel)
    {
        Name = itemModel.Name;
        Type = itemModel.Type;
        Bonus = itemModel.Bonus;
        SpritePath = itemModel.SpritePath;
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
            else
                return null;
        }
        return null;
    }
    
    #endregion
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