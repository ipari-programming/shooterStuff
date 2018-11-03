using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    enum AiActivity { none, wander, follow, search, attack }

    public float health;
    public float damage;
    public float speed;
    public float range;
    public float attackSpeed;

    Player player;

    Rigidbody2D rb;

    AiActivity aiActivity = AiActivity.none;

    float cooldownWander = 0;
    float cooldownAttack = 0;

    Vector3 locationMemory = Vector2.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        switch (aiActivity)
        {
            case AiActivity.none:

                locationMemory = Vector2.zero;
                break;

            case AiActivity.wander:
                
                rb.velocity = locationMemory * speed * Time.deltaTime;

                if (cooldownWander <= 0)
                {
                    locationMemory = new Vector2(Random.Range(-3, 3), Random.Range(-3, 3));

                    cooldownWander = 20 / speed;
                }
                else
                {
                    cooldownWander -= Time.deltaTime;
                }

                break;
            case AiActivity.follow:

                locationMemory = player.transform.position;

                rb.velocity = locationMemory * speed * Time.deltaTime;

                break;
            case AiActivity.search:

                break;
            case AiActivity.attack:

                if (cooldownAttack <= 0)
                {
                    // TODO atttack
                    Debug.Log("attack");

                    cooldownAttack = 1 / attackSpeed;
                }
                else
                {
                    cooldownAttack -= Time.deltaTime;
                }

                break;
        }

        DetectPlayer();
    }

    void DetectPlayer()
    {
        player = FindObjectOfType<Player>();

        if (player != null)
        {
            if (Vector2.Distance(transform.position, player.transform.position) <= range)
            {
                aiActivity = AiActivity.attack;
                return;
            }
        }

        aiActivity = AiActivity.wander;
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
