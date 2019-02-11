using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {

    public Effect trapEffect;

    public ParticleSystem trapParticle;

    public float cooldown = 0;

    float currentCooldown;

    EffectManager effectManager;

    void Start()
    {
        currentCooldown = cooldown;
    }

    void Update()
    {
        currentCooldown -= Time.deltaTime;

        if (currentCooldown < .01f && trapParticle != null) trapParticle.gameObject.SetActive(true);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentCooldown < .01f)
        {
            effectManager = FindObjectOfType<EffectManager>();

            if (collision.GetComponent<Player>())
                effectManager.ApplyEffect(trapEffect);

            currentCooldown = cooldown;

            if (trapParticle != null) trapParticle.gameObject.SetActive(false);
        }
    }

}
