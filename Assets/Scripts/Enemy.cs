using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float health;
    public float damage;
    public float speed;

    public bool DealDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
            return true;
        }
        return false;
    }

    public void Die()
    {
        Destroy(gameObject);
    }




}
