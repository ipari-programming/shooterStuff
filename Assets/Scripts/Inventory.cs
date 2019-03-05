using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    Notifier notifier;

    public List<string> items;

    void Start()
    {
        items = new List<string>(Load());
    }

    public void Give(Item item)
    {
        if (items == null) items = new List<string>();

        items.Add(item.name);

        Save();
    }

    public bool Contains(string itemName)
    {
        foreach (string item in items)
        {
            if (item.ToLower().Contains(itemName)) return true;
        }

        return false;
    }

    void Save()
    {
        string data = "";

        foreach (string item in items)
        {
            data += item + "|";
        }

        PlayerPrefs.SetString("inventory", data);
        PlayerPrefs.Save();
    }

    string[] Load()
    {
        string data = PlayerPrefs.GetString("inventory", "");
        return data.Split('|');
    }
}
