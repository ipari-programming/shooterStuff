using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stair : MonoBehaviour {

    public GameObject buttonObject;

    public Transform destination;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            Button button = buttonObject.GetComponent<Button>();

            buttonObject.SetActive(true);

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() =>
            {
                StartCoroutine(collision.GetComponent<Player>().Teleport(destination));
            });
        }
    }
    
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
            buttonObject.SetActive(false);
    }

}
