using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public bool isRay;
    public float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isRay)
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy)
            {
                enemy.DealDamage(damage);
            }
            if (!collision.isTrigger && !collision.GetComponent<Player>()) Destroy(transform.gameObject);

            MagicConsole console = collision.GetComponent<MagicConsole>();
            if (console) console.RunCommand();
        }
    }



}
