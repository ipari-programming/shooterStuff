using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public Player player;

    public Character[] characters;

    void Start()
    {
        foreach (Character character in characters)
        {
            if (PlayerPrefs.GetString("last-player", "Mario") == character.name)
            {
                player.GetComponent<CharacterDisplay>().character = character;
                player.GetComponent<CharacterDisplay>().ChangeCharacter();
                break;
            }
        }
    }
}
