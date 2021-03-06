﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectManager : MonoBehaviour {
    
    public List<Effect> effects;

    PlayerController playerController;

    EffectDisplay effectDisplay;

    float initialSpeed;
    float initialDamage;

    void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
        effectDisplay = FindObjectOfType<EffectDisplay>();

        initialSpeed = playerController.speed;
        initialDamage = playerController.damage;

        effects = new List<Effect>();
    }

    void Update()
    {
        if (effects.Count > 0)
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
            ResetPlayer();
        }

        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        if (effects.Count > 0)
        {
            string text = "";
            foreach (Effect eff in effects)
            {
                text += eff.name;
                if (eff.duration < 60 && eff.duration > .1f) text += " (" + Mathf.Round(eff.duration * 10) / 10 + ")";
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

        if (effects.Count < 1)
        {
            ResetPlayer();
            return;
        }

        foreach (Effect eff in effects)
        {
            if (eff.accelerate != 1) return;
        }

        ResetPlayer();
    }

    public void ClearAll()
    {
        foreach (Effect eff in effects) eff.duration = 0;

        effects.Clear();

        ResetPlayer();
    }

    void ResetPlayer()
    {
        if (initialSpeed > 0) playerController.GetComponent<PlayerController>().speed = initialSpeed;
        if (initialDamage > 0) playerController.GetComponent<PlayerController>().damage = initialDamage;
    }
}
