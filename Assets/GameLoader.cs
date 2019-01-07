using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour
{
    public Slider progressBar;

    void Start()
    {
        LoadLevel(1);
    }

    void LoadLevel(int index)
    {
        StartCoroutine(LoadAsync(index));
    }

    IEnumerator LoadAsync(int index)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);

        while (!operation.isDone)
        {
            float prog = Mathf.Clamp01(operation.progress / .9f);
            progressBar.value = prog;

            yield return null;
        }
    }
}
