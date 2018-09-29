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

    public float maxHealth;
    public float health;
    public float damage;
    public float speed;
    public float weaponRange;
    public float bulletSpeed;

    public bool isWeaponRay;

    public Vector2 shootingOffset;

    public Color mainColor;

    public AudioClip theme;
}
