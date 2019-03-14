using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float damage;
    public float speed;
    public float range;
    public float attackSpeed;

    enum AiActivity { wander, attack }

    Player player;

    Rigidbody2D rb;

    Animator animator;

    AiActivity aiActivity;

    Vector3 destination, prevPos;

    bool positionReached = false;
    bool crAttack = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();

        Physics2D.queriesStartInColliders = false;

        aiActivity = AiActivity.wander;

        destination = transform.position;
    }

    void Update()
    {
        LookForPlayer();

        if (animator != null)
        {
            animator.SetFloat("speed", rb.velocity.magnitude);
            animator.SetBool("attack", crAttack);
        }
        
        switch (aiActivity)
        {
            case AiActivity.wander:

                Vector2 rdmLocation = new Vector2(
                    transform.position.x + Random.Range(-3, 3),
                    transform.position.y + Random.Range(-3, 3)
                );

                if (positionReached)
                {
                    destination = rdmLocation;
                }

                break;
            case AiActivity.attack:

                destination = player.transform.position;

                if (Vector3.Distance(transform.position, player.transform.position) <= range)
                {
                    if (!crAttack) StartCoroutine(Attack());
                }

                break;
        }
    }

    // TODO need to optimize
    void LookForPlayer()
    {
        if (player == null) player = FindObjectOfType<Player>();
        else
        {
            for (float i = 0; i < Mathf.PI; i += Mathf.PI / 16)
            {
                float angle = i - (Mathf.PI / 2) - (transform.eulerAngles.z / 180 * Mathf.PI);

                Vector2 dir = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));

                RaycastHit2D hit = Physics2D.Raycast(transform.position, dir);

                if (hit)
                {
                    if (hit.transform == player.transform)
                    {
                        Debug.DrawLine(transform.position, hit.point, Color.red);

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

        #region Movement

        rb.velocity = Vector3.Normalize(destination - transform.position) * speed * Time.deltaTime * 10;
        transform.up = Vector3.Lerp(transform.up, rb.velocity, Time.deltaTime * speed);

        if (Vector3.Distance(destination, transform.position) < .1f || Vector3.Distance(prevPos, transform.position) < .005f)
        {
            positionReached = true;
        }
        else
        {
            positionReached = false;
        }

        prevPos = transform.position;

        #endregion
    }

    void ChangeActivity(AiActivity newActivity, bool force)
    {
        aiActivity = newActivity;
    }

    IEnumerator Attack()
    {
        crAttack = true;

        while (player != null && Vector3.Distance(transform.position, player.transform.position) <= range)
        {
            player.DealDamage(damage);

            yield return new WaitForSeconds(1 / attackSpeed);

            if (player == null) break;
        }

        crAttack = false;
    }
}
