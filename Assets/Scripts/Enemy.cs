using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int health;
    public int damage;
    public float speed;

    public void DealDamage(int dealeddamage)
    {
        health -= dealeddamage;
        if (health <= 0)
            Die();
    }

    public void Die()
    {
        Destroy(transform.gameObject);
    }




}
