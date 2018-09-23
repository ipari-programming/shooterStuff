﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchControlsKit;

public class Player : MonoBehaviour {
    
    public TCKJoystick joystickMove;
    public TCKJoystick joystickShoot;

    public GameObject bulletPrefab;

    public Sprite skinIdle;
    public Sprite skinRun;
    public Sprite skinAttack;
    public Sprite bullet;

    public int speed = 10;
    public int weaponRange;

    public Vector3 shootingOffset;

    public bool isWeaponRay;

    Rigidbody2D rb;

    SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate ()
    {
        // Movement
        Vector2 move;
        move.x = joystickMove.axisX.value;
        move.y = joystickMove.axisY.value;
        move *= speed * 10;

        rb.AddForce(move);

        sr.sprite = move.x + move.y == 0 ? skinIdle : skinRun;

        // Aiming and shooting
        Vector2 aim;
        aim.x = joystickShoot.axisX.value;
        aim.y = joystickShoot.axisY.value;
        bool isAiming = aim != Vector2.zero;

        if (isAiming) StartCoroutine(Shoot());

        // Rotation
        if (move != Vector2.zero || isAiming) transform.rotation = Quaternion.LookRotation(isAiming ? aim : move, Vector3.forward);

        transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, 0, 0);
    }

    IEnumerator Shoot()
    {
        sr.sprite = skinAttack;

        Debug.Log(Mathf.Round(transform.rotation.eulerAngles.z) + " SIN: " + Mathf.Sin(transform.rotation.eulerAngles.z) + " COS: " + Mathf.Cos(transform.rotation.eulerAngles.z));

        float x = transform.position.x + shootingOffset.x * Mathf.Sin(transform.rotation.eulerAngles.z * (Mathf.PI / 180));
        float y = transform.position.y + shootingOffset.y * Mathf.Cos(transform.rotation.eulerAngles.z * (Mathf.PI / 180));

        if (isWeaponRay)
        {
            
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(x, y), transform.up, weaponRange);
            
            if (hit)
            {
                
            }

            yield return new WaitForSeconds(.2f);
        }
        else
        {
            GameObject bulletEffect = Instantiate(bulletPrefab, new Vector2(x, y), transform.rotation);

            yield return new WaitForSeconds(weaponRange);

            Destroy(bulletEffect);
        }

        sr.sprite = skinIdle;
    }
}
