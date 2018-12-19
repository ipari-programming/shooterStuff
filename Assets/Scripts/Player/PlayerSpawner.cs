using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using Cinemachine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject healthBar;
    public GameObject healthBarFill;
    public GameObject buttonRespawn;

    public Joystick joystickMove;
    public Joystick joystickShoot;

    public Character[] characters;

    // public CinemachineVirtualCamera cam;

    GameObject currentPlayer;

    void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        Vector2 pos = new Vector2(PlayerPrefs.GetFloat("checkpoint-x", 0), PlayerPrefs.GetFloat("checkpoint-y", 0));
        currentPlayer = Instantiate(playerPrefab, pos, Quaternion.identity);

        currentPlayer.GetComponent<Player>().buttonRespawn = buttonRespawn;
        buttonRespawn.SetActive(false);

        currentPlayer.GetComponent<Player>().healthBar = healthBar;
        currentPlayer.GetComponent<Player>().healthBarFill = healthBarFill;

        // currentPlayer.GetComponent<Player>().cam = cam;
        // cam.Follow = currentPlayer.transform;

        currentPlayer.GetComponent<PlayerController>().joystickMove = joystickMove;
        currentPlayer.GetComponent<PlayerController>().joystickShoot = joystickShoot;

        foreach (Character character in characters)
        {
            if (PlayerPrefs.GetString("last-player", "Mario") == character.name)
            {
                currentPlayer.GetComponent<CharacterDisplay>().character = character;
                currentPlayer.GetComponent<CharacterDisplay>().ChangeCharacter();
                break;
            }
        }
    }
}
