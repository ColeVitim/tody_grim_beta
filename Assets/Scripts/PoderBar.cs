using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PoderBar : MonoBehaviour
{
       public Slider sliderPoder;

    public void SetMaxPoder(int poder)
    {
        sliderPoder.maxValue = poder;
        sliderPoder.value = 0;
    }

    public void SetPoder(int poder)
    {
        sliderPoder.value = poder;

    }
}

