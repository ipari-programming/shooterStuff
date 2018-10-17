using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject healthBar;
    public GameObject healthBarFill;
    public GameObject buttonRespawn;

    public Joystick joystickMove;
    public Joystick joystickShoot;

    public Character[] characters;

    public CinemachineVirtualCamera cam;

    GameObject currentPlayer;

    void Start()
    {
        Spawn();
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

    public void Spawn()
    {
        Vector2 pos = new Vector2(PlayerPrefs.GetFloat("x-pos", 0), PlayerPrefs.GetFloat("y-pos", 0));
        currentPlayer = Instantiate(playerPrefab, pos, Quaternion.identity);
        currentPlayer.GetComponent<Player>().buttonRespawn = buttonRespawn;
        buttonRespawn.SetActive(false);
        currentPlayer.GetComponent<Player>().healthBar = healthBar;
        currentPlayer.GetComponent<Player>().healthBarFill = healthBarFill;
        currentPlayer.GetComponent<Player>().joystickMove = joystickMove;
        currentPlayer.GetComponent<Player>().joystickShoot = joystickShoot;
        currentPlayer.GetComponent<Player>().cam = cam;
        cam.Follow = currentPlayer.transform;
    }
}
