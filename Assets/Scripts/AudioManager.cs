using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {
    
    #region Singleton
    public static AudioManager instance = null;

	void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
    #endregion

    public AudioSource sourceMusic;
    public AudioSource sourceEffect;

    [Space]

    public AudioClip[] clipsMusic;
    public AudioClip[] clipsEffect;

    #region Music
    public void StartMusic(AudioClip clip)
    {
        sourceMusic.clip = clip;
        sourceMusic.Play();
    }

    public void StartMusic(string clipName)
    {
        foreach (AudioClip item in clipsMusic)
        {
            if (item.name.Contains(clipName.ToLower()))
            {
                sourceMusic.clip = item;
                break;
            }
        }

        if (sourceMusic.clip != null) sourceMusic.Play();
    }

    public void StopMusic()
    {
        sourceMusic.Stop();
    }
    #endregion
}
