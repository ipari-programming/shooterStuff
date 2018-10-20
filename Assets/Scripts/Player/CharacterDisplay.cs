using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDisplay : MonoBehaviour {

    public Character character;

    Player p;

    PlayerController pc;

    Animator a;

	void Awake ()
    {
        p = GetComponent<Player>();
        pc = GetComponent<PlayerController>();
        a = GetComponent<Animator>();

        ChangeCharacter();
    }
	
    public void ChangeCharacter()
    {
        p.name = character.name;

        p.maxHealth = character.maxHealth;
        p.health = character.health;
        
        p.mainColor = character.mainColor;

        pc.speed = character.speed;
        pc.damage = character.damage;
        pc.bulletSpeed = character.bulletSpeed;
        pc.weaponRange = character.weaponRange;

        pc.bullet = character.bullet;

        pc.isWeaponRay = character.isWeaponRay;

        pc.shootingOffset = character.shootingOffset;

        a.runtimeAnimatorController = character.RTAnimatorController;
    }

}
