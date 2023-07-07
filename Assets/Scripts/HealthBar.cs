using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{

    public Slider sliderHB;

    public void SetMaxHealth(int health)
    {
        sliderHB.maxValue = health;
        sliderHB.value = health;
    }

    public void SetHealth(int health)
    {
        sliderHB.value = health;

    }
}
