using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class Player : MonoBehaviour {

    public CinemachineVirtualCamera cam;

    public GameObject healthBar;
    public GameObject healthBarFill;
    
    public GameObject bulletPrefab;

    public GameObject buttonRespawn;

    public Joystick joystickMove;
    public Joystick joystickShoot;

    public Sprite skinIdle;
    public Sprite skinRun;
    public Sprite skinAttack;
    public Sprite bullet;

    public float maxHealth;
    public float health;
    public float damage;
    public float speed = 10;
    public float weaponRange;
    public float bulletSpeed;

    public Vector3 shootingOffset;

    public bool isWeaponRay;

    public Color mainColor;

    Rigidbody2D rb;

    SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        healthBarFill.GetComponent<Image>().color = mainColor;
        healthBarFill.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, health / maxHealth * healthBar.GetComponent<RectTransform>().sizeDelta.x);
    }

    void FixedUpdate ()
    {
        // Movement
        Vector2 move;
        move.x = joystickMove.Horizontal + Input.GetAxis("Horizontal");
        move.y = joystickMove.Vertical + Input.GetAxis("Vertical");

        move *= speed;

        rb.velocity = move;

        sr.sprite = move.x + move.y == 0 ? skinIdle : skinRun;

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

        }

        sr.sprite = isBulletOut ? skinAttack : skinIdle;
    }
    #endregion

    #region Health stuff
    public void Heal(float amount)
    {
        if (health + amount > maxHealth) health = maxHealth;
        else health += amount;

        healthBarFill.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, health / maxHealth * healthBar.GetComponent<RectTransform>().sizeDelta.x);
    }

    public bool DealDamage(float amount)
    {
        health -= amount;

        healthBarFill.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, health / maxHealth * healthBar.GetComponent<RectTransform>().sizeDelta.x);

        if (health <= 0)
        {
            Die();
            return true;
        }
        return false;
    }

    void Die()
    {
        Debug.Log("Player dead.");
        Destroy(gameObject);
        buttonRespawn.SetActive(true);
    }
    #endregion

    public IEnumerator Teleport(Transform destination)
    {
        cam.Follow = null;

        transform.position = destination.position;

        cam.transform.position = new Vector3(destination.position.x, destination.position.y, -10);

        yield return new WaitForSeconds(.1f);

        cam.Follow = transform;
    }
}
