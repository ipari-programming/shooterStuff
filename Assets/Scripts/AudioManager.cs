﻿using System.Collections;
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


    #region Music
    private void Update()
    {
        if (FindObjectOfType<Player>() && !sourceMusic.isPlaying && !pausedMusic)
        {
            int selected = Random.Range(0, musicRandomMaxIndex);
            StartMusic(clipsMusic[selected]);
        }
    }

    public float MusicTime { get => sourceMusic.time; set => sourceMusic.time = value; }

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
}
