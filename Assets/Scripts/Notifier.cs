using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notifier : MonoBehaviour
{
    public Text textNotif;

    Animator animator;

    float time;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Notify(string text)
    {
        StartCoroutine(Notification(text, 3));
    }

    public void Notify(string text, float delay)
    {
        StartCoroutine(Notification(text, delay));
    }
    
    IEnumerator Notification(string text, float delay)
    {
        time = text.Length > 0 ? delay : 0;

        textNotif.text = text;
        animator.SetBool("open", true);

        while (time > 0)
        {
            yield return new WaitForFixedUpdate();
            time -= Time.fixedDeltaTime;
        }

        animator.SetBool("open", false);
    }

    public void Cancel()
    {
        animator.SetBool("open", false);
    }
}
