using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stair : MonoBehaviour {

    public GameObject buttonObject;

    public Transform destination;

    [Space]

    public string requireItems;

    Notifier notifier;

    void Start()
    {
        notifier = FindObjectOfType<Notifier>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.GetComponent<Player>()) return;

        if (requireItems.Length > 1)
        {
            Inventory inventory = FindObjectOfType<Inventory>();

            string[] items = requireItems.Split(',');

            foreach (string itemName in items)
            {
                if (!inventory.Contains(itemName))
                {
                    notifier.Notify("Stair locked! You need some items to go here.");
                    return;
                }
            }
        }

        Button button = buttonObject.GetComponent<Button>();

        buttonObject.SetActive(true);

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            StartCoroutine(collision.GetComponent<Player>().Teleport(destination));
        });
    }
    
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
            buttonObject.SetActive(false);
    }

}
