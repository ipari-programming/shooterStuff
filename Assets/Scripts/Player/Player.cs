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

    void Update ()
    {
        // Movement
        Vector2 move;
        move.x = joystickMove.Horizontal;
        move.y = joystickMove.Vertical;

        #region Debug from PC
        move.x = move.x == 0 ? Input.GetAxis("Horizontal") : move.x;
        move.y = move.y == 0 ? Input.GetAxis("Vertical") : move.y;
        #endregion

        move *= speed * 10;

        rb.AddForce(move);

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
            
            float x = transform.position.x + shootingOffset.x * Mathf.Sin((transform.rotation.eulerAngles.z + shootingOffset.y) * (Mathf.PI / 180));
            float y = transform.position.y + shootingOffset.x * Mathf.Cos((transform.rotation.eulerAngles.z + shootingOffset.y) * (Mathf.PI / 180));

            GameObject bulletEffect = Instantiate(bulletPrefab, new Vector2(x, y), transform.rotation);
            bulletEffect.GetComponent<SpriteRenderer>().sprite = bullet;
            bulletEffect.GetComponent<Bullet>().isRay = isWeaponRay;
            bulletEffect.GetComponent<Bullet>().damage = damage;

            if (isWeaponRay)
            {
                RaycastHit2D hit = Physics2D.Raycast(new Vector2(x, y), transform.up, weaponRange);

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
                bulletEffect.GetComponent<Rigidbody2D>().AddForce(bulletEffect.transform.up * bulletSpeed);

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
