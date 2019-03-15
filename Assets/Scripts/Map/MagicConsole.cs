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

    PlayerSpawner playerSpawner;

    Notifier notifier;

    void Start()
    {
        playerSpawner = playerSpawner;
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

    public void PickUp()
    {
        Destroy(gameObject);
    }

    public void EditCommand()
    {
        editUI.SetActive(!editUI.gameObject.activeSelf);
        input.text = command;
    }

    public void RunCommand()
    {
        string[] args = command.Split(' ');

        args[0] = args[0].Replace("/", "");

        // debug <on|off|toggle>
        if (args[0] == "debug")
        {
            switch (args[1])
            {
                case "on":
                    playerSpawner.debugMode = true;
                    break;
                case "off":
                    playerSpawner.debugMode = false;
                    break;
                case "toggle":
                    playerSpawner.debugMode = !playerSpawner.debugMode;
                    break;
            }
            
            if (playerSpawner.debugMode) notifier.Notify("CONSOLE> DEBUG MODE ON! SAVING DISABLED!");
            else notifier.Notify("CONSOLE> DEBUG MODE OFF! SAVING ENABLED!");
        }
        // effect <clear|effect name>
        else if (args[0] == "effect")
        {
            if (args[1] == "clear") FindObjectOfType<EffectManager>().ClearAll();
            else if (args[1] == "give")
            {
                foreach (Effect eff in effects)
                {
                    if (eff.name.ToLower() == args[2])
                    {
                        FindObjectOfType<EffectManager>().ApplyEffect(eff);
                        return;
                    }
                }
            }
        }
        // notify <time> <message>
        else if (args[0] == "notify" && args.Length > 1)
        {
            string message = args[2];

            if (args.Length > 3) for (int i = 3; i < args.Length; i++) message += " " + args[i];

            notifier.Notify(message, float.Parse(args[1]));
        }
        // spawn <enemy|boss|player>
        else if (args[0] == "spawn")
        {
            switch (args[1])
            {
                case "beetle":
                    Instantiate(enemiesPrefab[0], transform.position, Quaternion.identity);
                    notifier.Notify("CONSOLE> Spawned a beetle.");
                    break;
                case "boss":
                    Instantiate(enemiesPrefab[1], transform.position, Quaternion.identity);
                    notifier.Notify("CONSOLE> Spawned boss.");
                    break;
                case "player":
                    playerSpawner.Spawn();
                    notifier.Notify("CONSOLE> WARNING!!! THIS CAN LEAD TO HUGE ERRORS! SAVING WILL BE DISABLED!", 10);
                    playerSpawner.debugMode = true;
                    break;
            }
        }
        // kill all
        else if (args[0] == "kill" && args[1] == "all")
        {
            Enemy[] enemies = FindObjectsOfType<Enemy>();

            foreach (Enemy enemy in enemies) enemy.Die();

            notifier.Notify("CONSOLE> Killed " + enemies.Length + " enemies.");
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

            notifier.Notify("CONSOLE> Killed enemy " + Vector2.Distance(transform.position, nearestEnemy.transform.position) + " units away.");
        }
        // kill player
        else if (args[0] == "kill" && args[1] == "player")
        {
            notifier.Notify("CONSOLE> Suicide is not a solution!");
        }
        // Easter egg
        else if (args[0] == "gigau")
        {
            notifier.Notify("au");
            PlayerPrefs.DeleteAll();
        }
        else notifier.Notify("CONSOLE> Unknown command ¯\\_(ツ)_/¯");
    }
}
