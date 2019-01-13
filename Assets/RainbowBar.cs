using UnityEngine;
using UnityEngine.UI;

public class RainbowBar : MonoBehaviour
{
    public Image sliderFill;
    
    void Update()
    {
        sliderFill.color = new Color(Random.Range(.5f, 1), Random.Range(.5f, 1), Random.Range(.5f, 1));
    }
}
