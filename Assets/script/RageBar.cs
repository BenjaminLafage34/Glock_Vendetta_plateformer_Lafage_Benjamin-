using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RageBar : MonoBehaviour
{


    public Slider slider;
    public void SetMinRage(int rage)
    {
        slider.minValue = rage;
        slider.value = rage;
    }
    public void SetRage(int rage)
    {
        slider.value = rage;
    }
}
