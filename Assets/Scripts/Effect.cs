using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Effect", menuName = "Effect")]
public class Effect : ScriptableObject {

    public new string name;

    public float maxDuration;
    public float applyRate;

    float duration;

    public bool reverse;

    public int heal;
    public float accelerate;

    float initialSpeed;

    public IEnumerator StartEffect(Player player)
    {
        duration = maxDuration;

        initialSpeed = player.speed;
        player.speed *= accelerate;

        do
        {
            if (heal > 0) player.Heal(heal);
            else if (heal < 0) player.DealDamage(-heal);

            yield return new WaitForSeconds(applyRate);

            duration -= applyRate;

        } while (duration > 0);

        Reverse(player);

    }

    public void Reverse(Player player)
    {
        duration = 0;

        if (reverse)
        {
            player.speed = initialSpeed;
        }
    }

}
