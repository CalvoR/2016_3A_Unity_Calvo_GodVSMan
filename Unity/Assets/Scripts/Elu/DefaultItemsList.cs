using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System;

public class DefaultItemsList : MonoBehaviour {

    [SerializeField]
    public TextAsset xmlItemDataFile;

    public static Dictionary<ItemType, List<Item>> ItemList;


    public void Awake()
    {
        FillListFromXmlFile();
    }


    /// <summary>
    /// Remplit la liste d'objets en lisant le fichier XML
    /// </summary>
    public void FillListFromXmlFile()
    {
        // CHargement du fichier XML
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlItemDataFile.text);
        ItemList = new Dictionary<ItemType, List<Item>>();

        XmlNode itemsTable = xmlDoc.GetElementsByTagName("ItemsTable").Item(0);

        foreach (XmlNode itemTypeNode in itemsTable.ChildNodes)
        {
            List<Item> currentTypeItemList = new List<Item>();
            ItemType currentType = GetTypeFromName(itemTypeNode.Name);

            foreach (XmlNode itemNode in itemTypeNode.ChildNodes)
            {
                Dictionary<string, int> bonusList = new Dictionary<string, int>();

                Item item = new Item();
                item.Name = itemNode.Name;
                item.Type = currentType;
                item.SpritePath = itemNode.Attributes["sprite"].Value ?? string.Empty;
                foreach (XmlNode bonusNode in itemNode.ChildNodes)
                    bonusList.Add(bonusNode.Name, int.Parse(bonusNode.InnerXml));
                item.Bonus = bonusList;
                currentTypeItemList.Add(item);
            }
            ItemList.Add(currentType, currentTypeItemList);
        } 
    }

    /// <summary>
    /// Retourne un type d'item de l'énumération à partir d'une string
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public ItemType GetTypeFromName (string name)
    {
        ItemType resultType;
        switch (name)
        {
            case "weapon":
                resultType = ItemType.weapon;
                break;
            case "field":
                resultType = ItemType.field;
                break;
            case "consumable":
                resultType = ItemType.consumable;
                break;
            case "resource":
                resultType = ItemType.resource;
                break;
            case "other":
                resultType = ItemType.other;
                break;
            default:
                resultType = ItemType.other;
                break;
        }
        return resultType;
    }

}
