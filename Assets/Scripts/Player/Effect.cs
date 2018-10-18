using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Effect", menuName = "Effect")]
public class Effect : ScriptableObject {

    public new string name;

    public float maxDuration;
    public float applyRate;

    float duration;

    public float heal = 0;
    public float accelerate = 1;

    public float Duration { get { return duration; } }

    public IEnumerator StartEffect(Player player)
    {
        duration = maxDuration;
        
        player.speed *= accelerate;

        do
        {
            if (heal > 0) player.Heal(heal);
            else if (heal < 0) player.DealDamage(-heal);

            yield return new WaitForSeconds(applyRate);

            duration -= applyRate;

        } while (duration > 0);

    }

    public void Reverse(Player player)
    {
        duration = 0;
    }

}
