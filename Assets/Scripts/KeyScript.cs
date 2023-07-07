using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyScript : MonoBehaviour
{
    public static int keyValor;
    Text Keys;
    // Start is called before the first frame update
    void Start()
    {
        Keys = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Keys.text = "{} "+ keyValor;
    }
}
