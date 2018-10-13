using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public Player player;

    public Character[] characters;

    void Start()
    {
        player.GetComponent<CharacterDisplay>().character = characters[PlayerPrefs.GetInt("last-player", 0)];
        player.GetComponent<CharacterDisplay>().ChangeCharacter();
    }
}
