using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicConsole : MonoBehaviour
{
    public Button buttonEdit;
    public Button buttonRun;

    public GameObject editUI;

    public string command = "";

    [Space]

    public GameObject enemyPrefab;

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
        switch (command)
        {
            case "spawn":
                Instantiate(enemyPrefab, transform.position, Quaternion.identity);
                break;
            case "kill":
                Enemy nearestEnemy = FindObjectsOfType<Enemy>()[0];

                foreach (Enemy enemy in FindObjectsOfType<Enemy>())
                {
                    float nearest = Vector2.Distance(transform.position, nearestEnemy.transform.position);
                    float current = Vector2.Distance(transform.position, enemy.transform.position);

                    if (current < nearest)
                    {
                        nearestEnemy = enemy;
                    }
                }

                nearestEnemy.Die();
                break;
        }
    }
}
