using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchControlsKit;

public class Player : MonoBehaviour {
    
    public Sprite skinIdle;
    public Sprite skinRun;
    public Sprite skinAttack;

    public TCKJoystick joystickMove;
    public TCKJoystick joystickShoot;

    public float speed = 10f;

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

        // Rotation
        if (move != Vector2.zero || isAiming) transform.rotation = Quaternion.LookRotation(isAiming ? aim : move, Vector3.forward);

        transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, 0, 0);
    }
}
