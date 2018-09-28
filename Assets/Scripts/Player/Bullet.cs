using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public bool isRay;
    public int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isRay)
        {
            Enemy enemy = collision.transform.GetComponent<Enemy>();
            if (enemy)
            {
                enemy.DealDamage(damage);
            }
            Destroy(transform.gameObject);
        }
    }



}
