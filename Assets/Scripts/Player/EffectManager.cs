using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour {
    
    public List<Effect> effects;

    PlayerController playerController;

    float initialSpeed = 0;

    void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

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
            if (initialSpeed == 0) initialSpeed = playerController.speed;
            playerController.GetComponent<PlayerController>().speed = initialSpeed;
        }
    }

    public void ApplyEffect(Effect effect)
    {
        if (effects == null) effects = new List<Effect>();

        if (effects.Contains(effect) && !effect.enableMultiple)
        {
            effects[effects.IndexOf(effect)].ResetDuration();
        }
        else
        {
            effects.Add(effect);
            StartCoroutine(effect.StartEffect(playerController.GetComponent<Player>()));
        }
    }

    public void ClearEffect(Effect effect)
    {
        effect.duration = 0;
        effects.Remove(effect);
    }

}
