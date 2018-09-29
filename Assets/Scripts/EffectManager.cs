using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour {

    public Player player;

    List<Effect> effects;

    public void ApplyEffect(Effect effect)
    {
        if (effects == null) effects = new List<Effect>();
        effects.Add(effect);
        StartCoroutine(effect.StartEffect(player));
    }

    public void ClearEffect(Effect effect)
    {
        effects.Remove(effect);
        effect.Reverse(player);
    }

}
