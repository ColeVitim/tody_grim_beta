using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagiaPoison : MonoBehaviour
{
    [SerializeField]
    GameObject ParHitPoison;


    public int damage = 3;
    public AudioClip explosao;



 
    IEnumerator MagiaLoop()
    {
        
        yield return new WaitForSeconds(.4f);
        Destroy(gameObject);
    }

    void Start()
    {
    }

    public void StartMagic(bool isFacingLeft)
    {
        if (isFacingLeft)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            StartCoroutine(MagiaLoop());

        }
        else
        {
            StartCoroutine(MagiaLoop());
        }

    }

 /*   void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(ParHitPoison, collision.gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(explosao, transform.position);


        if (collision.CompareTag("Enemy") || collision.CompareTag("Player"))
        {
                     
            collision.gameObject.SendMessage("Poison", damage);
             
        }

        
    }*/

void GetPlayerGameObject()
    {
        GameObject player = GameObject.Find("Player");
        if(player != null)
          player.GetComponent<Control>().setPoder();
    }


    void OnCollisionEnter2D(Collision2D col)
    {
               
        ContactPoint2D[] contacts = new ContactPoint2D[1];
        col.GetContacts(contacts);
        var contactPoint = contacts[0].point;
        Instantiate(ParHitPoison, contactPoint, Quaternion.identity);
        AudioSource.PlayClipAtPoint(explosao, transform.position);
        Destroy(gameObject);

        if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "Player")
           {
            col.gameObject.SendMessage("Poison", damage);
                CinemachineShake.Instance.ShakeCamera(.5f, .1f);
           }
        

        else if (col.gameObject.tag == "Magic")
            {
            GetPlayerGameObject(); 
                CinemachineShake.Instance.ShakeCamera(.5f, .1f);
            }
            
    }
            




}