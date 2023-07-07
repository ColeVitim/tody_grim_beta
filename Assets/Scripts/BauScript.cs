using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BauScript : MonoBehaviour
{
   Animator anime;
   bool isOpen = false;
   public bool isClose;
   
    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();
    }

    void Update()
{
            if(Input.GetKeyDown("e") && !isOpen && isClose)
            {
               Debug.Log("aquicarai");
               KeyScript.keyValor ++;
               anime.Play("Bau");
               isOpen = true;
            }
}
    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {

            
            isClose = true;
        }
    }

        void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            isClose = false;
        }
    }
}
