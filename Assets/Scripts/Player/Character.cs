using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
public class Character : ScriptableObject {

    public new string name;

    public RuntimeAnimatorController RTAnimatorController;
    public RuntimeAnimatorController bullet;

    public Sprite menuIcon;

    public float maxHealth;
    public float health;
    public float damage;
    public float speed;
    public float weaponRange;
    public float bulletSpeed;
    public float attackSoundDelay;

    public bool isMelee;
    public bool oneJoystick;

    public Vector2 shootingOffset;

    public Color mainColor;

    public AudioClip theme;

    public string themeLink;
}
