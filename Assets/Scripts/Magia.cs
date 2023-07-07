using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magia : MonoBehaviour
{
    [SerializeField]
    GameObject ParHit;

    [SerializeField]
    GameObject ParHitPoison;

    public AudioClip explosao;

    GameObject Player;

    public int damage = 10;

    int numContacts;
    public bool isEnemy;

    IEnumerator MagiaLoop()
    {
        
        yield return new WaitForSeconds(.1f);
        Destroy(gameObject, .5f);
    }

    void Start()
    {
        ;
    }

void GetPlayerGameObject()
    {
        GameObject player = GameObject.Find("Player");
        if(player != null)
          player.GetComponent<Control>().setPoder();
            
        
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

    void OnCollisionEnter2D(Collision2D col)
    {
               
        ContactPoint2D[] contacts = new ContactPoint2D[1];
        col.GetContacts(contacts);
        var contactPoint = contacts[0].point;
        Instantiate(ParHit, contactPoint, Quaternion.identity);
        AudioSource.PlayClipAtPoint(explosao, transform.position);
        Destroy(gameObject);

        if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "Player")
            {
            col.gameObject.SendMessage("Hit", damage);
            CinemachineShake.Instance.ShakeCamera(.5f, .1f);
            }        

        else if (col.gameObject.tag == "Magic")
               {
                GetPlayerGameObject(); 
                CinemachineShake.Instance.ShakeCamera(.5f, .1f);
               }


            

    }
    
}