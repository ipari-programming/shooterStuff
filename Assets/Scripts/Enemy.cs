using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    enum AiActivity { wander, search, attack }

    public float health;
    public float damage;
    public float speed;
    public float range;
    public float attackSpeed;

    Player player;

    Rigidbody2D rb;

    AiActivity aiActivity = AiActivity.wander;

    bool isBusy = false;

    Vector2 playerPos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Physics2D.queriesStartInColliders = false;
    }

    void Update()
    {
        switch (aiActivity)
        {
            case AiActivity.wander:

                Vector2 rdmLocation = new Vector2(
                    transform.position.x + UnityEngine.Random.Range(-3, 3),
                    transform.position.y + UnityEngine.Random.Range(-3, 3)
                );
                if (!isBusy) StartCoroutine(Goto(rdmLocation, .5f));

                break;
            case AiActivity.search:

                

                break;
            case AiActivity.attack:

                if (!isBusy) StartCoroutine(Goto(playerPos, 0));

                break;
        }

        LookForPlayer();
    }

    void ChangeActivity(AiActivity newActivity, bool force)
    {
        if (force)
        {
            StopAllCoroutines();
            isBusy = false;
        }

        aiActivity = newActivity;
    }

    IEnumerator Goto(Vector3 destination, float waitAfter)
    {
        isBusy = true;

        Vector3 prevPos = transform.position;

        while (Vector3.Distance(transform.position, destination) > .1f)
        {
            rb.velocity = (destination - transform.position).normalized * speed * Time.deltaTime * 10;
            transform.up = Vector3.Lerp(transform.up, rb.velocity, Time.deltaTime * speed);

            yield return null;

            if (prevPos != transform.position) prevPos = transform.position;
            else break;
        }

        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(waitAfter);

        isBusy = false;
    }

    void LookForPlayer()
    {
        if (player == null) player = FindObjectOfType<Player>();
        else
        {
            for (float i = 0; i <  Mathf.PI; i += Mathf.PI / 16)
            {
                float angle = i - (Mathf.PI / 2) - (transform.eulerAngles.z / 180 * Mathf.PI);

                Vector2 dir = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));

                RaycastHit2D hit = Physics2D.Raycast(transform.position, dir);

                if (hit)
                {
                    if (hit.transform == player.transform)
                    {
                        Debug.DrawLine(transform.position, hit.point, Color.red);

                        playerPos = player.transform.position;

                        ChangeActivity(AiActivity.attack, true);
                        
                        break;
                    }
                    else
                    {
                        Debug.DrawLine(transform.position, hit.point, Color.green);
                        ChangeActivity(AiActivity.wander, false);
                    }
                }
            }
        }
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
