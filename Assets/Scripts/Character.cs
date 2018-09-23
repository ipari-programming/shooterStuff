using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
public class Character : ScriptableObject {

    public new string name;

    public Sprite skinIdle;
    public Sprite skinRun;
    public Sprite skinAttack;
    public Sprite bullet;

    public int health;
    public int speed;
    public int damage;
    public int weaponRange;

    public bool isWeaponRay;

    public Vector2 shootingOffset;

    public AudioClip theme;
}
