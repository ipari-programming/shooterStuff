using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {

    public Effect trapEffect;

    EffectManager effectManager;

	void Start ()
    {
        effectManager = FindObjectOfType<EffectManager>();
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
            effectManager.ApplyEffect(trapEffect);
    }

}
