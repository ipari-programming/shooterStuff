﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string Name { get => gameObject.name; }

    [TextArea]
    public string pickupText;
    [Min(2)]
    public float pickupTextDelay = -1;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.GetComponent<Player>()) return;

        FindObjectOfType<Inventory>().Give(this);

        if (pickupText.Length >= 0) FindObjectOfType<Notifier>().Notify(pickupText, pickupTextDelay);
        else FindObjectOfType<Notifier>().Notify(pickupText);

        gameObject.SetActive(false);
    }
}
