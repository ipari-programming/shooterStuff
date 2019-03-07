using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
// using Cinemachine;

public class Player : MonoBehaviour {

    // public CinemachineVirtualCamera cam;

    public GameObject healthBar;
    public GameObject healthBarFill;

    public GameObject buttonRespawn;

    public float maxHealth;
    public float health;

    public Color mainColor;

    void Start()
    {
        healthBarFill.GetComponent<Image>().color = mainColor;
        healthBarFill.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, health / maxHealth * healthBar.GetComponent<RectTransform>().sizeDelta.x);
    }

    #region Health stuff
    public void Heal(float amount)
    {
        if (health + amount > maxHealth) health = maxHealth;
        else health += amount;

        healthBarFill.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, health / maxHealth * healthBar.GetComponent<RectTransform>().sizeDelta.x);
    }

    public bool DealDamage(float amount)
    {
        health -= amount;

        healthBarFill.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, health / maxHealth * healthBar.GetComponent<RectTransform>().sizeDelta.x);

        if (health <= 0)
        {
            Die();
            return true;
        }
        return false;
    }

    void Die()
    {
        SceneManager.LoadScene(3);
        /*
        Destroy(gameObject);
        buttonRespawn.SetActive(true);
        */
    }
    #endregion

    public IEnumerator Teleport(Transform destination)
    {
        // cam.Follow = null;

        transform.position = destination.position;

        // cam.transform.position = new Vector3(destination.position.x, destination.position.y, -10);

        yield return new WaitForSeconds(.1f);

        // cam.Follow = transform;
    }
}
