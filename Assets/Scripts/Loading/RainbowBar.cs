using UnityEngine;
using UnityEngine.UI;

public class RainbowBar : MonoBehaviour
{
    public Image sliderFill;

    int i = 0;

    void Update()
    {
        if (i < 7) i++;
        else
        {
            sliderFill.color = new Color(Random.Range(.3f, .6f), Random.Range(.3f, .6f), Random.Range(.3f, .6f));
            i = 0;
        }
    }
}
