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
    public Character selectedCharacter;

    string characterName;
    int characterID = 0;

	void Start ()
    {
        characterName = PlayerPrefs.GetString("last-player", "Mario");
        SelectCharacter(characterName);

        buttonRight.onClick.AddListener(() =>
        {
            SelectNextCharacter();
        });
        buttonLeft.onClick.AddListener(() =>
        {
            SelectPrevCharacter();
        });
    }

    void SelectCharacter(string name)
    {
        int id = 0;
        foreach (Character item in characters)
        {
            if (name == item.name)
            {
                header.color = item.mainColor;
                title.text = item.name;
                characterID = id;
                characterDisplay.sprite = item.skinIdle;
                selectedCharacter = characters[characterID];
                GetComponent<MenuAudioManager>().StartMusic();
            }
            else
                id++;
        }
    }

    void SelectPrevCharacter()
    {
        if (characterID > 0)
        {
            characterID--;
            header.color = characters[characterID].mainColor;
            title.text = characters[characterID].name;
            characterDisplay.sprite = characters[characterID].skinIdle;
            characterName = characters[characterID].name;
            selectedCharacter = characters[characterID];
            GetComponent<MenuAudioManager>().StartMusic();
        }
    }

    void SelectNextCharacter()
    {
        if (characterID < characters.Length - 1)
        {
            characterID++;
            header.color = characters[characterID].mainColor;
            title.text = characters[characterID].name;
            characterDisplay.sprite = characters[characterID].skinIdle;
            characterName = characters[characterID].name;
            selectedCharacter = characters[characterID];
            GetComponent<MenuAudioManager>().StartMusic();
        }
    }

    public void StartGame()
    {
        PlayerPrefs.SetString("last-player", characterName);
        GetComponent<MenuAudioManager>().StopMusic();
        SceneManager.LoadScene(1);
    }
}
