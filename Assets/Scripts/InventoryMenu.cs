using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenu : MonoBehaviour
{
    public GameObject inventoryMenuUI;
    public GameObject pauseButton;

    public bool inInventory = false;

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
    }

    public void ToggleInventory()
    {
        if (inInventory) Close();
        else Open();
    }
}
