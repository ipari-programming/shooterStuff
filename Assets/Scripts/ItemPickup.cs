using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{

    public string thisItemName;

    string[] allItemName = {"masterball", "chaosemerald", "upmushroom"};


    void Start()
    {
        if (PlayerPrefs.GetInt("first-item") == 1 && thisItemName.ToLower() == allItemName[0])
        {
            Destroy(gameObject);
        }
        if (PlayerPrefs.GetInt("second-item") == 1 && thisItemName.ToLower() == allItemName[1])
        {
            Destroy(gameObject);
        }
        if (PlayerPrefs.GetInt("third-item") == 1 && thisItemName.ToLower() == allItemName[2])
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            if (thisItemName.ToLower() == allItemName[0])
            {
                PlayerPrefs.SetInt("first-item", 1);
                PlayerPrefs.Save();
                Destroy(gameObject);
            }
            else if (thisItemName.ToLower() == allItemName[1])
            {
                PlayerPrefs.SetInt("second-item", 1);
                PlayerPrefs.Save();
                Destroy(gameObject);
            }
            else if (thisItemName.ToLower() == allItemName[2])
            {
                PlayerPrefs.SetInt("third-item", 1);
                PlayerPrefs.Save();
                Destroy(gameObject);
            }
        }
    }
}
