using UnityEngine.Audio;
using UnityEngine;

public class MenuAudioManager : MonoBehaviour
{

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void StartMusic()
    {
        audioSource.clip = GetComponent<MenuManager>().selectedCharacter.theme;
        audioSource.Play();
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }
}