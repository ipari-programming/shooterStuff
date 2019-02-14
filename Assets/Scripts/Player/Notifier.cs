using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notifier : MonoBehaviour
{
    public Text textNotif;

    Animator animator;

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
        textNotif.text = text;
        animator.SetBool("open", true);

        yield return new WaitForSeconds(delay);

        animator.SetBool("open", false);
    }

    public void Cancel()
    {
        animator.SetBool("open", false);
    }
}
