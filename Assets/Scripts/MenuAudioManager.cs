using UnityEngine.Audio;
using UnityEngine;

public class MenuAudioManager : MonoBehaviour
{

    AudioSource audioSource;

    MenuManager menuManager;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        menuManager = GetComponent<MenuManager>();
    }

    public void StartMusic()
    {
        audioSource.clip = menuManager.selectedCharacter.theme;
        audioSource.Play();
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }
}