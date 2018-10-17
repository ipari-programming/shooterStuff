using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDisplay : MonoBehaviour {

    public Character character;

    Player p;

	void Awake ()
    {
        p = GetComponent<Player>();

        ChangeCharacter();
    }
	
    public void ChangeCharacter()
    {
        p.name = character.name;

        p.skinIdle = character.skinIdle;
        p.skinRun = character.skinRun;
        p.skinAttack = character.skinAttack;
        p.bullet = character.bullet;

        p.speed = character.speed;
        p.weaponRange = character.weaponRange;
        p.bulletSpeed = character.bulletSpeed;
        p.maxHealth = character.maxHealth;
        p.health = character.health;
        p.damage = character.damage;

        p.isWeaponRay = character.isWeaponRay;

        p.shootingOffset = character.shootingOffset;

        p.mainColor = character.mainColor;
    }

}
