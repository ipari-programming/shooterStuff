using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Item : MonoBehaviour
{
    public string Name { get => gameObject.name; }

    [TextArea]
    public string pickupText;
    [Min(2)]
    public float pickupTextDelay = -1;
    public Effect effect;
    [Space]
    public bool save;
    public int sceneIndex = -1;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.GetComponent<Player>()) return;

        if (effect != null) FindObjectOfType<EffectManager>().ApplyEffect(effect);

        FindObjectOfType<Inventory>().Give(this);

        if (pickupText.Length >= 0) FindObjectOfType<Notifier>().Notify(pickupText, pickupTextDelay);
        else FindObjectOfType<Notifier>().Notify(pickupText);

        gameObject.SetActive(false);

        // Save

        if (!save) return;

        PlayerPrefs.SetFloat("checkpoint-x", transform.position.x);
        PlayerPrefs.SetFloat("checkpoint-y", transform.position.y + 1);

        FindObjectOfType<Inventory>().Save();

        if (sceneIndex < 0) return;

        SceneManager.LoadScene(sceneIndex);
    }
}
