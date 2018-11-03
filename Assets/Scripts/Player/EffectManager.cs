using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour {

    public Player player;
    
    public List<Effect> effects;

    float initialSpeed = 0;

    void Update()
    {
        if (effects != null && effects.Count > 0)
        {
            foreach (Effect effect in effects)
            {
                if (effect.duration <= 0)
                {
                    ClearEffect(effect);
                    break;
                }
            }
        }
        else
        {
            if (initialSpeed == 0) initialSpeed = player.GetComponent<PlayerController>().speed;
            player.GetComponent<PlayerController>().speed = initialSpeed;
        }
    }

    public void ApplyEffect(Effect effect)
    {
        if (effects == null) effects = new List<Effect>();
        if (effects.Contains(effect) && !effect.enableMultiple)
        {
            effects[effects.IndexOf(effect)].count++;
            effects[effects.IndexOf(effect)].ResetDuration();
        }
        else
        {
            effects.Add(effect);
            StartCoroutine(effect.StartEffect(player));
        }
    }

    public void ClearEffect(Effect effect)
    {
        if (effects[effects.IndexOf(effect)].count < 2)
        {
            effects.Remove(effect);
        }
        else
        {
            effects[effects.IndexOf(effect)].count--;
        }
    }

}
