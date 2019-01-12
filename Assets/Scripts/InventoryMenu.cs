using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenu : MonoBehaviour
{
    public GameObject inventoryMenuUI;
    public GameObject pauseButton;

    public bool inInventory = false;

    [Space]

    public GameObject[] itemDisplay;

    [Space]

    public Sprite[] itemSprite;

    void CheckItem()
    {
        if (PlayerPrefs.GetInt("first-item") == 1)
        {
            itemDisplay[0].GetComponent<Image>().sprite = itemSprite[0];
            itemDisplay[0].GetComponent<Image>().color = new Color(255, 255, 255, 255);
        }
        if (PlayerPrefs.GetInt("second-item") == 1)
        {
            itemDisplay[1].GetComponent<Image>().sprite = itemSprite[1];
            itemDisplay[1].GetComponent<Image>().color = new Color(255, 255, 255, 255);
        }
        if (PlayerPrefs.GetInt("third-item") == 1)
        {
            itemDisplay[2].GetComponent<Image>().sprite = itemSprite[2];
            itemDisplay[2].GetComponent<Image>().color = new Color(255, 255, 255, 255);
        }
    }

    void Close()
    {
        inventoryMenuUI.SetActive(false);
        Time.timeScale = 1f;
        inInventory = false;
        pauseButton.SetActive(true);
    }

    void Open()
    {
        inventoryMenuUI.SetActive(true);
        Time.timeScale = 0f;
        inInventory = true;
        pauseButton.SetActive(false);
        CheckItem();
    }

    public void ToggleInventory()
    {
        if (inInventory) Close();
        else Open();
    }
}
