using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectManager : MonoBehaviour {
    
    public List<Effect> effects;

    PlayerController playerController;

    EffectDisplay effectDisplay;

    float initialSpeed = 0;

    void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
        effectDisplay = FindObjectOfType<EffectDisplay>();
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

        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        if (effects != null && effects.Count > 0)
        {
            string text = "Effects:\n\r";
            foreach (Effect eff in effects)
            {
                text += eff.name;
                if (eff.duration < 60 && eff.duration > .5f) text += " (" + Mathf.Round(effects[0].duration * 10) / 10 + ")";
                text += "\n\r";
            }
            effectDisplay.GetComponent<Text>().text = text;
        }
        else
        {
            effectDisplay.GetComponent<Text>().text = "";
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
