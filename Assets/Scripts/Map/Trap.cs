using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {

    public Effect trapEffect;

    public ParticleSystem trapParticle;

    public float cooldown = 0;

    CircleCollider2D collider;

    float currentCooldown;

    EffectManager effectManager;

    void Start()
    {
        collider = GetComponent<CircleCollider2D>();

        currentCooldown = 0;
    }

    void Update()
    {
        currentCooldown -= Time.deltaTime;

        if (currentCooldown > .01f) return;

        if (collider != null) collider.enabled = true;

        if (trapParticle != null) trapParticle.gameObject.SetActive(true);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentCooldown < .01f)
        {
            effectManager = FindObjectOfType<EffectManager>();

            if (collision.GetComponent<Player>())
                effectManager.ApplyEffect(trapEffect);

            currentCooldown = cooldown;

            if (collider != null) collider.enabled = false;

            if (trapParticle != null) trapParticle.gameObject.SetActive(false);
        }
    }

}
