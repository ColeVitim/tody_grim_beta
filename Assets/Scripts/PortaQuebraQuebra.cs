using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaQuebraQuebra : MonoBehaviour
{
    public float health;
    public float healthMax;
    public float damageTake;

    SpriteRenderer rend;


    // Start is called before the first frame update
    void Start()
    {
        health = healthMax;
        rend = GetComponent<SpriteRenderer>();    

    }

    // Update is called once per frame
    void Update()
    {
       health += 6 * Time.deltaTime;

       if(health > healthMax)
       {
        health = healthMax;
       }

       else if(health > 0 && health <= 5)
       {
        Color c1 = rend.color;
        c1.g = 0.25f;
        c1.b = 0.25f;
        rend.color = c1;
       }

       else if(health > 5 && health <= 10)
       {
        Color c2 = rend.color;
        c2.g = .5f;
        c2.b = .5f;
        rend.color = c2;
       }       

       else if(health > 10 && health <= 15)
       {
        Color c3 = rend.color;
        c3.g = 1f;
        c3.b = 1f;
        rend.color = c3;
       }          

       else if(health <= 0)
       {
        Destroy(gameObject);
       }
       

    }

    void Hit(int damage)
    {
        damageTake = (float)damage;
        health -= damageTake;
    }
}
