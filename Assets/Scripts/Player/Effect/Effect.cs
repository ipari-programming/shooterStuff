using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Effect", menuName = "Effect")]
public class Effect : ScriptableObject {

    public new string name;

    public float minDuration;
    public float maxDuration;
    public float applyRate;
    public float duration;
    [Space]
    public float heal = 0;
    public float accelerate = 1;
    public float damageMultiplier = 1;

    public bool enableMultiple = false;

    public IEnumerator StartEffect(Player player)
    {
        duration = Random.Range(minDuration, maxDuration);
        
        player.GetComponent<PlayerController>().speed *= accelerate;
        player.GetComponent<PlayerController>().damage *= damageMultiplier;

        do
        {
            if (heal > 0) player.Heal(heal);
            else if (heal < 0) player.DealDamage(-heal);

            yield return new WaitForSeconds(applyRate);

            duration -= applyRate;

        } while (duration > 0);

    }

    public void ResetDuration()
    {
        duration = Random.Range(minDuration, maxDuration);
    }
}
