using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.GetComponent<Player>())
        {
            PlayerPrefs.SetFloat("checkpoint-x", transform.position.x);
            PlayerPrefs.SetFloat("checkpoint-y", transform.position.y);
        }
    }
}
