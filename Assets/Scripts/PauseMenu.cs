using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public GameObject pauseMenuUI;

    public bool paused = false;

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
        SceneManager.LoadScene(0);
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

}
