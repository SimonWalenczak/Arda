using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetValues(float time)
    {
        slider.maxValue = time;
        slider.value = 0;
    }

    public void SetTime(float time)
    {
        slider.value = time;
        fill.color = gradient.Evaluate(slider.normalizedValue);
        //print(time);
    }


}
