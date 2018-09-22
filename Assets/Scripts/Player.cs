using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    
    public Sprite skinIdle;
    public Sprite skinRun;
    public Sprite skinAttack;

    public float speed = 10f;

    Rigidbody2D rb;

    SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update () {
        float moveH = Input.GetAxis("Horizontal");
        float moveV = Input.GetAxis("Vertical");

        rb.AddForce(new Vector2(moveH * speed * 10, moveV * speed * 10));

        sr.sprite = moveH + moveV == 0 ? skinIdle : skinRun;
    }
}
