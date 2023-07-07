using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class GlobalIluminacao : MonoBehaviour
{

    [SerializeField] public Light2D light;

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light2D>();
        light.intensity = 0.02f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
