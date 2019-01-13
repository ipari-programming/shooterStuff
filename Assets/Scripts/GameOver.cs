using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text textGameOver;
    public Text textNo;
    public Text textRandom;
    public Button btn;

    public string[] rdmTexts;

    int state = 0;
    int protestCount = 0;

    AudioManager audioManager;

    void Start()
    {
        StartCoroutine(IStart());
    }

    void Update()
    {
        textNo.color = new Color(Random.Range(.5f, 1), Random.Range(.5f, 1), Random.Range(.5f, 1));
        textNo.fontSize = Random.Range(250, 300);
        textRandom.color = new Color(Random.Range(.5f, 1), Random.Range(.5f, 1), Random.Range(.5f, 1));
        textRandom.fontSize = Random.Range(71, 73);

        switch (state)
        {
            case 1:
                if (audioManager.MusicTime > 11)
                {
                    audioManager.MusicTime = 6;
                }

                if (protestCount > 4)
                {
                    audioManager.StartEffect("glass-break");

                    protestCount = 0;
                    state = 2;

                    audioManager.MusicTime = 41.2f;

                    btn.GetComponent<Image>().color = Color.black;
                    textRandom.text = rdmTexts[Random.Range(0, rdmTexts.Length)];
                    textRandom.gameObject.SetActive(true);

                    StartCoroutine(IRespawn());
                }
                break;
            default:
                return;
        }
    }

    public void Protest()
    {
        if (state == 1)
        {
            protestCount++;
            StartCoroutine(IProtest());
        }
    }

    IEnumerator IStart()
    {
        yield return new WaitForSeconds(.1f);

        audioManager = FindObjectOfType<AudioManager>();
        audioManager.StopMusic();

        yield return new WaitForSeconds(1);

        audioManager.StartMusic("fading-away");

        state = 1;
    }

    IEnumerator IProtest()
    {
        textNo.gameObject.SetActive(true);

        audioManager.sourceMusic.pitch = .7f;

        yield return new WaitForSeconds(.1f);

        textNo.gameObject.SetActive(false);

        while (audioManager.sourceMusic.pitch < 1)
        {
            audioManager.sourceMusic.pitch += .01f;

            yield return null;
        }

        protestCount--;
    }

    IEnumerator IRespawn()
    {
        yield return new WaitForSeconds(3);

        for (float i = 0; i < 1; i += .1f)
        {
            btn.GetComponent<Image>().color = new Color(i, i, i);
            yield return new WaitForSeconds(.1f);
        }

        textRandom.gameObject.SetActive(false);
        audioManager.StopMusic();

        yield return new WaitForSeconds(.1f);

        SceneManager.LoadScene(2);
    }
}
