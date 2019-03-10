using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.GetComponent<Player>()) return;

        PlayerPrefs.SetFloat("checkpoint-x", transform.position.x);
        PlayerPrefs.SetFloat("checkpoint-y", transform.position.y);

        FindObjectOfType<Inventory>().Save();

        FindObjectOfType<Notifier>().Notify("Checkpoint and items saved");
    }
}
