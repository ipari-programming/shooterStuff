using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float health;
    public float damage;
    public float viewDistance;
    public float stopDistance;
    public float speed;
    public float meleeSpeed;

    Player player;

    float melee = 0;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void FixedUpdate()
    {
        //float angleEnemyPlayer = Mathf.Atan2(transform.position.y - player.transform.position.y, transform.position.x - player.transform.position.x) * 180 / Mathf.PI;

        //float x = transform.position.x + Mathf.Sin((270 - angleEnemyPlayer) * Mathf.PI / 180);
        //float y = transform.position.y + Mathf.Cos((270 - angleEnemyPlayer) * Mathf.PI / 180);

        //RaycastHit2D hit = Physics2D.Raycast(new Vector2(x, y), Quaternion.Euler(0, 0, angleEnemyPlayer + 180) * Vector2.right, viewDistance);

        //if (hit && hit.transform.GetComponent<Player>() && Vector2.Distance(transform.position, player.transform.position) > stopDistance)
        //{
        //    transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed / 100);
        //}

        melee++;

        if (Vector2.Distance(transform.position, player.transform.position) < stopDistance && melee > 60 / meleeSpeed)
        {
            Attack();
            melee = 0;
        }
    }

    void Attack()
    {
        player.DealDamage(damage);
    }

    #region Health and death
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
    #endregion

}
