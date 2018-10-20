using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    public Joystick joystickMove;
    public Joystick joystickShoot;

    public float speed = 10;

    public float damage;
    public float weaponRange;
    public float bulletSpeed;

    public Sprite bullet;

    public GameObject bulletPrefab;

    public bool isWeaponRay;

    public Vector3 shootingOffset;

    Rigidbody2D rb;

    Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        // Movement
        Vector2 move;
        move.x = joystickMove.Horizontal + Input.GetAxis("Horizontal");
        move.y = joystickMove.Vertical + Input.GetAxis("Vertical");

        move *= speed * Time.fixedDeltaTime * 20;

        rb.velocity = move;

        animator.SetFloat("speed", move.magnitude);

        // Aiming and shooting
        Vector2 aim;
        aim.x = joystickShoot.Horizontal;
        aim.y = joystickShoot.Vertical;
        bool isAiming = aim != Vector2.zero;

        if (isAiming && (Mathf.Abs(aim.x) > .5f || Mathf.Abs(aim.y) > .5f)) StartCoroutine(Shoot());

        // Rotation
        if (move != Vector2.zero || isAiming) transform.rotation = Quaternion.LookRotation(isAiming ? aim : move, Vector3.forward);

        transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, 0, 0);
    }

    #region Shooting
    bool isBulletOut = false;
    IEnumerator Shoot()
    {
        animator.SetBool("attack", true);

        if (!isBulletOut)
        {
            GameObject bulletEffect = Instantiate(bulletPrefab, transform.position + shootingOffset, Quaternion.identity);

            bulletEffect.transform.RotateAround(transform.position, Vector3.forward, -transform.rotation.eulerAngles.z);

            bulletEffect.GetComponent<SpriteRenderer>().sprite = bullet;
            bulletEffect.GetComponent<Bullet>().isRay = isWeaponRay;
            bulletEffect.GetComponent<Bullet>().damage = damage;

            if (isWeaponRay)
            {
                RaycastHit2D hit = Physics2D.Raycast(bulletEffect.transform.position, transform.up, weaponRange);

                if (hit)
                {
                    Enemy enemy = hit.transform.GetComponent<Enemy>();
                    if (enemy)
                    {
                        enemy.DealDamage(damage);
                    }
                }

                isBulletOut = true;
                bulletEffect.GetComponent<Rigidbody2D>().AddForce(bulletEffect.transform.up * bulletSpeed);

                yield return new WaitForSeconds(1 / weaponRange / 2);

                bulletEffect.GetComponent<Rigidbody2D>().AddForce(-bulletEffect.transform.up * bulletSpeed * 2);

                yield return new WaitForSeconds(1 / weaponRange / 2);

                Destroy(bulletEffect);
                isBulletOut = false;
            }
            else
            {
                isBulletOut = true;
                bulletEffect.GetComponent<Rigidbody2D>().AddForce(((Vector2)bulletEffect.transform.up + rb.velocity.normalized / 3) * bulletSpeed);

                yield return new WaitForSeconds(weaponRange / 10f);

                Destroy(bulletEffect);
                isBulletOut = false;
            }

            animator.SetBool("attack", false);
        }
    }
    #endregion
}
