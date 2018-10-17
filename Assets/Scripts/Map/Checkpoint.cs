using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.GetComponent<Player>())
        {
            PlayerPrefs.SetFloat("x-pos", collision.transform.position.x);
            PlayerPrefs.SetFloat("y-pos", collision.transform.position.y);
            /*
            float x = PlayerPrefs.GetFloat("x-pos");
            float y = PlayerPrefs.GetFloat("y-pos");
            Debug.Log(x + " " + y);
            */
        }
    }
}
