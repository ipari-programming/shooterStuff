using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public Camera cam;
    [Space]
    public Image header;
    public Image footer;
    public Text title;
    [Space]
    public SpriteRenderer characterDisplay;

    public Button buttonRight;
    public Button buttonLeft;
    public Button buttonStartNew;
    [Space]
    public Character[] characters;
    public Character selectedCharacter;
    [Space]
    public GameObject infoPanel;
    [Space]
    public Text randomText;
    public float randomTextChangeTime = 5000;
    public string[] randomTexts;

    float randomTextChange;

    string characterName;
    int characterID = 0;

    AudioManager audioManager;

    void Start ()
    {
        randomTextChange = randomTextChangeTime;

        characterName = PlayerPrefs.GetString("last-player", "Mario");
        FindCharacterByName(characterName);

        buttonRight.onClick.AddListener(FindNextCharacter);
        buttonLeft.onClick.AddListener(FindPrevCharacter);
    }

    void Update()
    {
        randomTextChange -= Time.deltaTime;

        if (randomTextChange <= 0)
        {
            randomText.text = randomTexts[Random.Range(0, randomTexts.Length)];

            randomTextChange = randomTextChangeTime;
        }
    }

    public void SetText(string text)
    {
        randomText.text = text;
        randomTextChange = randomTextChangeTime;
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
                StartCoroutine(SelectCharacter(characterID));
            }
            else id++;
        }
    }

    void FindPrevCharacter()
    {
        if (characterID > 0)
        {
            characterID--;
            StartCoroutine(SelectCharacter(characterID));
        }
    }

    void FindNextCharacter()
    {
        if (characterID < characters.Length - 1)
        {
            characterID++;
            StartCoroutine(SelectCharacter(characterID));
        }
    }

    IEnumerator SelectCharacter(int id)
    {
        Character character = characters[id];

        Color darker = new Color(character.mainColor.r / 3, character.mainColor.g / 3, character.mainColor.b / 3);
        cam.backgroundColor = darker;
        
        header.color = character.mainColor;
        footer.color = character.mainColor;

        title.text = character.name;
        characterDisplay.sprite = character.menuIcon;
        characterName = character.name;
        selectedCharacter = character;

        buttonStartNew.gameObject.SetActive(PlayerPrefs.GetString("last-player", "") != "");

        buttonLeft.gameObject.SetActive(id != 0);
        buttonRight.gameObject.SetActive(id != characters.Length - 1);

        yield return null;

        audioManager = FindObjectOfType<AudioManager>();
        audioManager.pausedMusic = true;
        audioManager.Loop(true);
        audioManager.StartTheme(selectedCharacter.name.ToLower());
    }
    #endregion

    public void StartGame(bool isNew)
    {
        if (isNew) PlayerPrefs.DeleteAll();
        // checkpoint-x, checkpoint-y, inventory
        PlayerPrefs.SetString("last-player", characterName);
        PlayerPrefs.Save();

        audioManager.StopMusic();
        audioManager.Loop(false);
        audioManager.pausedMusic = false;

        SceneManager.LoadScene(2);
    }

    public void ToggleInfo()
    {
        infoPanel.SetActive(!infoPanel.activeSelf);

        characterDisplay.transform.localPosition = infoPanel.activeSelf ? new Vector3(500, 300, 0) : new Vector3(0, 100, 0);
        characterDisplay.transform.localScale = infoPanel.activeSelf ? new Vector3(20, 20, 1) : new Vector3(80, 80, 1);
    }

    public void OpenYoutubeLink()
    {
        Application.OpenURL(selectedCharacter.themeLink);
    }
}
