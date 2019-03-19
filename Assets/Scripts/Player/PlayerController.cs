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
    public float attackSoundDelay;

    public RuntimeAnimatorController bullet;

    public GameObject bulletPrefab;

    public bool isMelee;
    public bool oneJoystick;

    public Vector3 shootingOffset;

    Rigidbody2D rb;

    Animator animator;

    Inventory inventory;

    AudioManager audioManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();

        inventory = FindObjectOfType<Inventory>();

        Physics2D.queriesStartInColliders = false;

        joystickShoot.gameObject.SetActive(!oneJoystick);
    }

    private void Update()
    {
        audioManager = FindObjectOfType<AudioManager>();
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

        // Melee combat

        if (isMelee && oneJoystick && rb.velocity.magnitude > 0 && inventory.Contains("chaosemerald"))
        {
            StartCoroutine(Attack(true));
        }

        // Shooting
        Vector2 aim;
        aim.x = joystickShoot.Horizontal;
        aim.y = joystickShoot.Vertical;
        bool isAiming = aim != Vector2.zero;

        if (isAiming && (Mathf.Abs(aim.x) > .5f || Mathf.Abs(aim.y) > .5f) && inventory.Contains("chaosemerald")) StartCoroutine(Attack(false));

        // Rotation
        if (move != Vector2.zero || isAiming) transform.rotation = Quaternion.LookRotation(isAiming ? aim : move, Vector3.forward);

        transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, 0, 0);

        // Stop attack animation

        if ((rb.velocity.magnitude == 0 && oneJoystick) || !isAiming) animator.SetBool("attack", false);
    }

    #region Combat
    bool isBulletOut = false;
    IEnumerator Attack(bool disableSound)
    {
        if (!isBulletOut)
        {
            animator.SetBool("attack", true);

            if (!disableSound) audioManager.StartEffect(gameObject.name.ToLower(), attackSoundDelay);
            
            if (isMelee)
            {
                isBulletOut = true;
                Collider2D target = Physics2D.OverlapCircle(transform.position, weaponRange, LayerMask.GetMask("Enemy"));

                if (target != null) target.GetComponent<Enemy>().DealDamage(damage);

                yield return new WaitForSeconds(1 / bulletSpeed);
            }
            else
            {
                GameObject bulletEffect = Instantiate(bulletPrefab, transform.position + shootingOffset, Quaternion.identity);

                bulletEffect.transform.RotateAround(transform.position, Vector3.forward, -transform.rotation.eulerAngles.z);

                bulletEffect.GetComponent<Animator>().runtimeAnimatorController = bullet;
                bulletEffect.GetComponent<Bullet>().damage = damage;

                isBulletOut = true;
                bulletEffect.GetComponent<Rigidbody2D>().AddForce(((Vector2)bulletEffect.transform.up + rb.velocity.normalized / 3) * bulletSpeed);

                yield return new WaitForSeconds(weaponRange / 10f);

                Destroy(bulletEffect);
            }
            
            isBulletOut = false;
        }
    }

    void OnDrawGizmos()
    {
        if (isMelee)
        {
            Gizmos.DrawWireSphere(transform.position, weaponRange);
        }
    }
    #endregion
}
