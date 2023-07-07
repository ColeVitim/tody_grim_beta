using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletCounterScript : MonoBehaviour
{
   public static float bulletCount;
   Text bullets;

    void Start()
    {
        bullets = GetComponent<Text>();
    }
    void Update()
    {
        bullets.text = "º "+ Mathf.FloorToInt(bulletCount);
        
    }
}
