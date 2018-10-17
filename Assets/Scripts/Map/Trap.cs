using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {

    public Effect trapEffect;

    EffectManager effectManager;

    void OnTriggerEnter2D(Collider2D collision)
    {
        effectManager = FindObjectOfType<EffectManager>();
        if (collision.GetComponent<Player>())
            effectManager.ApplyEffect(trapEffect);
    }

}
