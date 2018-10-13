using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public Image header;
    public Text title;

    public SpriteRenderer characterDisplay;

    public Button buttonRight;
    public Button buttonLeft;

    public Character[] characters;

    int characterID = 0;

	void Start ()
    {
        SelectCharacter(PlayerPrefs.GetInt("last-player", 0));

        buttonRight.onClick.AddListener(() =>
        {
            if (characterID < characters.Length - 1) characterID++;
            SelectCharacter(characterID);
        });
        buttonLeft.onClick.AddListener(() =>
        {
            if (characterID > 0) characterID--;
            SelectCharacter(characterID);
        });
    }

    void SelectCharacter(int id)
    {
        header.color = characters[id].mainColor;
        title.text = characters[id].name;

        characterDisplay.sprite = characters[id].skinIdle;
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("last-player", characterID);

        SceneManager.LoadScene(1);
    }
}
