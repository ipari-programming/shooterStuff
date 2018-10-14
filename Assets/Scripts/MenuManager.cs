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

    MenuAudioManager menuAudioManager;

	void Start ()
    {
        menuAudioManager = GetComponent<MenuAudioManager>();

        characterName = PlayerPrefs.GetString("last-player", "Mario");
        FindCharacterByName(characterName);

        buttonRight.onClick.AddListener(() =>
        {
            FindNextCharacter();
        });
        buttonLeft.onClick.AddListener(() =>
        {
            FindPrevCharacter();
        });
    }

    void FindCharacterByName(string name)
    {
        int id = 0;
        foreach (Character item in characters)
        {
            if (name == item.name)
            {
                characterID = id;
                StartCoroutine(SelectCharacter(characters[characterID]));
            }
            else
                id++;
        }
    }

    void FindPrevCharacter()
    {
        if (characterID > 0)
        {
            characterID--;
            StartCoroutine(SelectCharacter(characters[characterID]));
        }
    }

    void FindNextCharacter()
    {
        if (characterID < characters.Length - 1)
        {
            characterID++;
            StartCoroutine(SelectCharacter(characters[characterID]));
        }
    }

    IEnumerator SelectCharacter(Character character)
    {
        header.color = character.mainColor;
        title.text = character.name;
        characterDisplay.sprite = character.skinIdle;
        characterName = character.name;
        selectedCharacter = character;

        yield return null;

        menuAudioManager.StartMusic();
    }

    public void StartGame()
    {
        PlayerPrefs.SetString("last-player", characterName);
        GetComponent<MenuAudioManager>().StopMusic();
        SceneManager.LoadScene(1);
    }
}
