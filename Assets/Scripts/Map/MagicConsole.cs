using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicConsole : MonoBehaviour
{
    public Button buttonEdit;
    public Button buttonRun;

    public InputField input;

    public GameObject editUI;

    public string command = "";

    [Space]

    public GameObject[] enemiesPrefab;

    public Effect[] effects;

    Notifier notifier;

    void Start()
    {
        notifier = FindObjectOfType<Notifier>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.GetComponent<Player>()) return;

        buttonEdit.gameObject.SetActive(true);
        buttonRun.gameObject.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.GetComponent<Player>()) return;

        buttonEdit.gameObject.SetActive(false);
        buttonRun.gameObject.SetActive(false);
    }

    public void InputValueChanged()
    {
        command = input.text;
    }

    public void SetCommand(string cmd)
    {
        command = cmd;
    }

    public void EditCommand()
    {
        editUI.SetActive(!editUI.gameObject.activeSelf);
    }

    public void RunCommand()
    {
        string[] args = command.Split(' ');

        args[0] = args[0].Replace("/", "");

        // effect <clear|effect name>
        if (args[0] == "effect")
        {
            if (args[1] == "clear") FindObjectOfType<EffectManager>().ClearAll();

            foreach (Effect eff in effects)
            {
                if (eff.name.ToLower() == args[1])
                {
                    FindObjectOfType<EffectManager>().ApplyEffect(eff);
                    return;
                }
            }
        }
        // notify <message>
        else if (args[0] == "notify" && args.Length > 1)
        {
            notifier.Notify(command.Substring(7));
        }
        // spawn enemy
        else if (args[0] == "spawn" && args[1] == "enemy")
        {
            Instantiate(enemiesPrefab[0], transform.position, Quaternion.identity);
        }
        // spawn boss
        else if (args[0] == "spawn" && args[1] == "boss")
        {
            Instantiate(enemiesPrefab[1], transform.position, Quaternion.identity);
        }
        // kill all
        else if (args[0] == "kill" && args[1] == "all")
        {
            Enemy[] enemies = FindObjectsOfType<Enemy>();

            foreach (Enemy enemy in enemies) enemy.Die();

            notifier.Notify("CONSOLE: killed " + enemies.Length + " enemies");
        }
        // kill nearest
        else if (args[0] == "kill" && args[1] == "nearest")
        {
            Enemy nearestEnemy = FindObjectsOfType<Enemy>()[0];

            foreach (Enemy enemy in FindObjectsOfType<Enemy>())
            {
                float nearest = Vector2.Distance(transform.position, nearestEnemy.transform.position);
                float current = Vector2.Distance(transform.position, enemy.transform.position);

                if (current < nearest) nearestEnemy = enemy;
            }

            nearestEnemy.Die();

            notifier.Notify("CONSOLE: killed enemy " + Vector2.Distance(transform.position, nearestEnemy.transform.position) + " units away");
        }
        else if (args[0] == "gigau")
        {
            notifier.Notify("au");
            PlayerPrefs.DeleteAll();
        }
        else notifier.Notify("CONSOLE: unknown command");
    }
}
