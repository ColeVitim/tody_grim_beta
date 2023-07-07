using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poder : MonoBehaviour
{
    [SerializeField]
    GameObject ParHit;

    public int damage = 15;
    int Speed;
    public bool isEnemy;
    Rigidbody2D rgbd;
    public AudioClip explosao;


    IEnumerator MagiaLoop()
    {
        
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

    void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
    }

    public void StartMagic(bool isFacingLeft)
    {
        if (isFacingLeft)
        {
            transform.localScale = new Vector3(-0.46f, 0.46f, 0.46f);
            Speed = -10;
            StartCoroutine(MagiaLoop());

        }


        else
        {
            StartCoroutine(MagiaLoop());
            Speed = 10;

        }

    }

void FixedUpdate()
{
    rgbd.velocity = new Vector2 (Speed, 0);

}

    void OnCollisionEnter2D(Collision2D col)
    {
        
        
        ContactPoint2D[] contacts = new ContactPoint2D[1];
        col.GetContacts(contacts);
        var contactPoint = contacts[0].point;
        Instantiate(ParHit, contactPoint, Quaternion.identity);
        AudioSource.PlayClipAtPoint(explosao, transform.position);


        if (col.gameObject.tag == "Enemy")
        {
                col.gameObject.SendMessage("Hit", damage);
                        Destroy(gameObject);
                CinemachineShake.Instance.ShakeCamera(.7f, .1f);


        }
        else if (col.gameObject.tag == "Ground")
        {
                        Destroy(gameObject);
                CinemachineShake.Instance.ShakeCamera(.3f, .1f);


        }

    }
    
}