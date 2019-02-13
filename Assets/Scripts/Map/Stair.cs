using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stair : MonoBehaviour {

    public GameObject buttonObject;

    public Transform destination;

    [Space]

    public bool[] requireItem = new bool[3];

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.GetComponent<Player>()) return;

        if ((requireItem[0] && PlayerPrefs.GetInt("first-item") == 0) ||
            (requireItem[1] && PlayerPrefs.GetInt("second-item") == 0) ||
            (requireItem[2] && PlayerPrefs.GetInt("third-item") == 0))
        {
            GetComponent<Fluid>().fluidEffect.name = "Stair locked";
            return;
        }

        GetComponent<Fluid>().fluidEffect.name = "Stair open";

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
