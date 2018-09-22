﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDisplay : MonoBehaviour {

    public Character character;

    Player p;

	void Start ()
    {
        p = GetComponent<Player>();
	}
	
    public void Update()
    {
        p.name = character.name;
        p.skinIdle = character.skinIdle;
        p.skinRun = character.skinRun;
        p.skinAttack = character.skinAttack;
        p.speed = character.speed;
    }

}
