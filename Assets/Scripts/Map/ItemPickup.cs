using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{

    public string thisItemName;

    string[] allItemName = { "masterball", "chaosemerald", "upmushroom", "magicconsole" };
    string[] prefsName = { "first-item", "second-item", "third-item", "fourth-item" };

    Notifier notifier;

    void Start()
    {
        notifier = FindObjectOfType<Notifier>();

        for (int i = 0; i < prefsName.Length; i++)
        {
            if (PlayerPrefs.GetInt(prefsName[i]) == 1 && thisItemName.ToLower() == allItemName[i])
                Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            for (int i = 0; i < prefsName.Length; i++)
            {
                if (thisItemName.ToLower() == allItemName[i])
                {
                    notifier.Notify(i == 3 ? "Congratulations! Now you can access the console!" : "New item in inventory!");

                    PlayerPrefs.SetInt(prefsName[i], 1);
                    PlayerPrefs.Save();
                    Destroy(gameObject);
                }
            }
        }
    }
}
