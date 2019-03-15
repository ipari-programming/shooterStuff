using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    Notifier notifier;

    public List<Item> items;

    bool ready = false;

    void Start()
    {
        ready = false;

        items = new List<Item>(Load());

        ready = true;
    }

    public void Give(Item item)
    {
        if (items == null) items = new List<Item>();

        items.Add(item);
    }

    public bool Contains(string itemName)
    {
        foreach (Item item in items)
        {
            if (item.Name.ToLower().Contains(itemName)) return true;
        }

        return false;
    }

    public void Save()
    {
        if (!ready || FindObjectOfType<PlayerSpawner>().debugMode) return;

        string data = "";

        foreach (Item item in items)
        {
            data += item.Name + "|";
        }

        PlayerPrefs.SetString("inventory", data);
        PlayerPrefs.Save();

        FindObjectOfType<Notifier>().Notify("Checkpoint and items saved");
    }

    Item[] Load()
    {
        string data = PlayerPrefs.GetString("inventory", "");

        List<Item> onLevel = new List<Item>(FindObjectsOfType<Item>());

        List<Item> saved = new List<Item>();

        foreach (Item item in onLevel)
        {
            if (data.Contains(item.Name))
            {
                saved.Add(item);
                item.gameObject.SetActive(false);
            }
        }

        return saved.ToArray();
    }
}
