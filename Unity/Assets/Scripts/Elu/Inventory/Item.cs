using System.Collections.Generic;
using System;

/// <summary>
/// Objet ou ressource à identifiant unique
/// </summary>
public class Item {

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

    // Constructeurs

    public Item()
    {    }

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