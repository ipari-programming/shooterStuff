using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public Camera cam;

    public Image header;
    public Text title;

    public SpriteRenderer characterDisplay;

    public Button buttonRight;
    public Button buttonLeft;
    public Button buttonStartNew;

    public Character[] characters;
    public Character selectedCharacter;

    string characterName;
    int characterID = 0;

    AudioManager audioManager;

    void Start ()
    {
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

    #region Find character
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
    #endregion

    IEnumerator SelectCharacter(Character character)
    {
        Color darker = new Color(character.mainColor.r / 3, character.mainColor.g / 3, character.mainColor.b / 3);
        cam.backgroundColor = darker;
        
        header.color = character.mainColor;
        buttonLeft.image.color = character.mainColor;
        buttonRight.image.color = character.mainColor;

        title.text = character.name;
        characterDisplay.sprite = character.menuIcon;
        characterName = character.name;
        selectedCharacter = character;

        buttonStartNew.gameObject.SetActive(PlayerPrefs.GetString("last-player", "Mario") == characterName);

        yield return null;

        audioManager = FindObjectOfType<AudioManager>();
        audioManager.pausedMusic = true;
        audioManager.Loop(true);
        audioManager.StartTheme(selectedCharacter.name);
    }

    public void StartGame(bool isNew)
    {
        if (PlayerPrefs.GetString("last-player", "Mario") != characterName || isNew)
        {
            PlayerPrefs.DeleteKey("checkpoint-x");
            PlayerPrefs.DeleteKey("checkpoint-y");
            PlayerPrefs.SetString("last-player", characterName);
            PlayerPrefs.Save();
        }

        audioManager.StopTheme();
        audioManager.Loop(false);
        audioManager.pausedMusic = false;

        SceneManager.LoadScene(2);
    }

    public void OpenYoutubeLink()
    {
        Application.OpenURL(selectedCharacter.themeLink);
    }
}
