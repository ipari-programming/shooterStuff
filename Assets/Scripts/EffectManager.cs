﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour {

    public Player player;

    [SerializeField()]
    List<Effect> effects;

    void Update()
    {
        if (effects != null && effects.Count > 0)
        {
            foreach (Effect effect in effects)
            {
                if (effect.Duration <= 0)
                {
                    ClearEffect(effect);
                    break;
                }
            }
        }
    }

    public void ApplyEffect(Effect effect)
    {
        if (effects == null) effects = new List<Effect>();
        if (!effects.Contains(effect))
        {
            effects.Add(effect);
            StartCoroutine(effect.StartEffect(player));
        }
    }

    public void ClearEffect(Effect effect)
    {
        effects.Remove(effect);
        effect.Reverse(player);
    }

}
