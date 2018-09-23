using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed;

	void Start () {
        GetComponent<Rigidbody2D>().AddForce(transform.up * speed);
	}
}
