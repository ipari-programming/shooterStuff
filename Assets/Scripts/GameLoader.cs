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
        yield return new WaitForSeconds(.2f);

        while (progressBar.value < 1)
        {
            progressBar.value += Random.Range(1, 5) / 300f;

            yield return null;
        }

        AsyncOperation operation = SceneManager.LoadSceneAsync(index);
    }
}
