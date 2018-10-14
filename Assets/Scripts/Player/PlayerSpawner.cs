using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public Player player;

    public Character[] characters;

    void Start()
    {
        int id = 0;
        foreach (Character item in characters)
        {
            if (PlayerPrefs.GetString("last-player", "Mario") != item.name)
                id++;
        }
        player.GetComponent<CharacterDisplay>().character = characters[id];
        player.GetComponent<CharacterDisplay>().ChangeCharacter();
    }
}
