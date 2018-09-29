﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fluid : MonoBehaviour {

    public Effect fluidEffect;

    EffectManager effectManager;

    void Start()
    {
        effectManager = FindObjectOfType<EffectManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        effectManager.ApplyEffect(fluidEffect);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        effectManager.ClearEffect(fluidEffect);
    }

}
