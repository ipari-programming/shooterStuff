using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    public GameObject pauseMenuUI;

    public bool paused = false;

    public Effect deafness;

    [Space]

    public GameObject[] itemDisplay;

    public Sprite[] itemSprite;

    [Space]

    public Button buttonConsole;

    void Start()
    {
        StartCoroutine(CheckMute());
    }

    IEnumerator CheckMute()
    {
        yield return new WaitForEndOfFrame();

        if (FindObjectOfType<AudioManager>().sourceMusic.mute) FindObjectOfType<EffectManager>().ApplyEffect(deafness);
        else FindObjectOfType<EffectManager>().ClearEffect(deafness);

        gameObject.SetActive(false);
    }

    void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
        FindObjectOfType<AudioManager>().ResumeMusic();
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
        FindObjectOfType<AudioManager>().PauseMusic();

        CheckItems();
    }

    void CheckItems()
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

        buttonConsole.gameObject.SetActive(PlayerPrefs.GetInt("fourth-item") == 1);
    }

    public void TogglePause()
    {
        if (paused) Resume();
        else Pause();
    }

    public void BackToMenu()
    {
        Resume();
        FindObjectOfType<AudioManager>().StopMusic();
        FindObjectOfType<AudioManager>().pausedMusic = true;
        SceneManager.LoadScene(1);
    }

    public void ToggleSound()
    {
        FindObjectOfType<AudioManager>().ToggleMute();
        if (FindObjectOfType<AudioManager>().sourceMusic.mute) FindObjectOfType<EffectManager>().ApplyEffect(deafness);
        else FindObjectOfType<EffectManager>().ClearEffect(deafness);
    }

    public void SongOnYoutube()
    {
        int i = 0;
        while(FindObjectOfType<AudioManager>().clipsMusic[i] != FindObjectOfType<AudioManager>().sourceMusic.clip)
        {
            i++;
        }
        Application.OpenURL(FindObjectOfType<AudioManager>().musicClipLinks[i]);
    }

    public void Console()
    {
        FindObjectOfType<MagicConsole>().transform.position = FindObjectOfType<Player>().transform.position;
        TogglePause();
    }
}
