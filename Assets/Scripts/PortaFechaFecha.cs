using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaFechaFecha : MonoBehaviour
{
    // Start is called before the first frame update

    public float moveSpeed;
    bool canFall = true;
    public int timeFall;
    int damage = 10;

    Rigidbody2D rb2d;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!canFall) rb2d.velocity = new Vector2 (0, moveSpeed);

        else  rb2d.velocity = new Vector2 (0, -moveSpeed * 2);
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
            if(col.gameObject.tag == "Ground")
        {
            canFall = false;
            StartCoroutine(canFallCooldown());
        }   

            if (col.gameObject.tag == "Player")
        {
            col.gameObject.SendMessage("Hit", damage);
            CinemachineShake.Instance.ShakeCamera(.5f, .1f);
        }        
           

    }
    

    IEnumerator canFallCooldown()
    {
        yield return new WaitForSeconds(timeFall);
        canFall = true;
    }
}
