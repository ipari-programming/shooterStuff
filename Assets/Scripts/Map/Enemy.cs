using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public ParticleSystem damageParticle;

    public float health;
    public float damageOnCollide = 0;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.GetComponent<Player>()) return;

        Player player = collision.gameObject.GetComponent<Player>();

        player.DealDamage(damageOnCollide);
    }

    public bool DealDamage(float amount)
    {
        if (damageParticle != null) StartCoroutine(PlayParticle(damageParticle));

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

    IEnumerator PlayParticle(ParticleSystem particle)
    {
        ParticleSystem p = Instantiate(particle, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(.1f);

        Destroy(p);
    }
}
