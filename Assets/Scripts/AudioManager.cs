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

    public int musicRandomMaxIndex = 6;

    [Space]

    public string[] musicClipLinks;

    [HideInInspector]
    public bool pausedMusic = false;

    // In game music
    private void Update()
    {
        if (FindObjectOfType<Player>() && !sourceMusic.isPlaying && !pausedMusic)
        {
            int selected = Random.Range(0, musicRandomMaxIndex);
            StartMusic(clipsMusic[selected]);
        }
    }

    #region Music
    public float MusicTime { get => sourceMusic.time; set => sourceMusic.time = value; }

    public void StartMusic(AudioClip clip)
    {
        sourceMusic.clip = clip;
        sourceMusic.time = 0;
        sourceMusic.Play();
    }

    public void StartMusic(string clipName)
    {
        foreach (AudioClip clip in clipsMusic)
        {
            if (clip.name.Contains(clipName.ToLower()))
            {
                StartMusic(clip);
                break;
            }
        }
    }

    public void StartTheme(string clipName)
    {
        foreach (AudioClip clip in clipsTheme)
        {
            if (clip.name.Contains(clipName.ToLower()))
            {
                StartMusic(clip);
                break;
            }
        }
    }

    public void StopMusic()
    {
        sourceMusic.Stop();
    }

    public void Loop(bool isOn)
    {
        sourceMusic.loop = isOn;
    }

    public void PauseMusic()
    {
        if (sourceMusic.isPlaying)
        {
            pausedMusic = true;
            sourceMusic.Pause();
        }  
    }

    public void ResumeMusic()
    {
        if(!sourceMusic.isPlaying)
        {
            pausedMusic = false;
            sourceMusic.Play();
        }
    }

    #endregion

    #region Effect
    public void StartEffect(string effectName)
    {
        foreach (AudioClip clip in clipsEffect)
        {
            if (clip.name.ToLower().Contains(effectName))
            {
                sourceEffect.clip = clip;
                break;
            }
        }

        if (sourceEffect.clip != null) sourceEffect.Play();
    }

    public void StartEffect(string effectName, float delay)
    {
        foreach (AudioClip clip in clipsEffect)
        {
            if (clip.name.ToLower().Contains(effectName))
            {
                sourceEffect.clip = clip;
                break;
            }
        }

        if (sourceEffect.clip != null) sourceEffect.PlayDelayed(delay);
    }
    #endregion

    public void ToggleMute()
    {
        sourceMusic.mute = !sourceMusic.mute;
        sourceEffect.mute = !sourceEffect.mute;
    }
}
