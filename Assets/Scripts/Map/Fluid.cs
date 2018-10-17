using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fluid : MonoBehaviour {

    public Effect fluidEffect;

    EffectManager effectManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        effectManager = FindObjectOfType<EffectManager>();
        if (collision.GetComponent<Player>())
            effectManager.ApplyEffect(fluidEffect);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
            effectManager.ClearEffect(fluidEffect);
    }

}
