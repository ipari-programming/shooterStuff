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

    public AudioClip[] clipsTheme;
    public AudioClip[] clipsMusic;
    public AudioClip[] clipsEffect;

    [Space]

    public string[] musicClipLinks;

    #region Music
    private void Update()
    {
        if (FindObjectOfType<Player>() && !sourceMusic.isPlaying)
        {
            int selected = Random.Range(0, clipsMusic.Length);
            StartMusic(clipsMusic[selected]);
        }
    }

    public void StartMusic(AudioClip clip)
    {
        sourceMusic.clip = clip;
        sourceMusic.Play();
    }

    public void StartTheme(string clipName)
    {
        foreach (AudioClip item in clipsTheme)
        {
            if (item.name.Contains(clipName.ToLower()))
            {
                sourceMusic.clip = item;
                break;
            }
        }

        if (sourceMusic.clip != null) sourceMusic.Play();
    }

    public void StopTheme()
    {
        sourceMusic.Stop();
    }

    public void Loop(bool isOn)
    {
        sourceMusic.loop = isOn;
    }

    #endregion
}
