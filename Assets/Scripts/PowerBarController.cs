using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PowerBarController : MonoBehaviour
{
    [SerializeField] public Slider slider;
    [SerializeField] Gradient gradient;
    [SerializeField] Image fill;

    public void SliderUp(float sliderValue)
    {
        slider.value += sliderValue;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SliderDown(float sliderValue)
    {
        if(slider.value >= 0.1f)
        {
            slider.value -= sliderValue;
            fill.color = gradient.Evaluate(slider.normalizedValue);
        }
    }
}
