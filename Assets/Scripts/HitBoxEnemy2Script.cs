using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxEnemy2Script : MonoBehaviour
{
   
         public AudioClip ataque;
   
    void OnTriggerEnter2D(Collider2D collision)
    {
        


        if(collision.CompareTag("Player"))
        {
        collision.gameObject.SendMessage("Hit", 10);
        AudioSource.PlayClipAtPoint(ataque, transform.position);


        }
    }
}
