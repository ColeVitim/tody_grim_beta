using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FamiliarBulletScript : MonoBehaviour
{


    [Header("Reference")]
    [SerializeField] private Rigidbody2D rb;


    [Header("Attribute")]
    [SerializeField] private float bulletSpeed = 0.5f;

    private Transform target;

    Vector3 direction;
    Transform pos;

    public int damage = 5;
    public float rotZ;

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    public void GetDirection(Vector3 _direction)
    {
        direction = _direction;
    }

    void Start()
    {
            //Vector3 direction = (target.position - transform.position).normalized;
            float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;   
            Physics2D.IgnoreLayerCollision(6, 9, true);
            Destroy(gameObject, .5f);

    }
    

    void FixedUpdate()
    {
        //if(!target) return;
            //Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * Time.deltaTime * bulletSpeed;
     
    }



    private void OnCollisionEnter2D(Collision2D other)
    {
        other.gameObject.SendMessage("Hit", damage);
        Destroy(gameObject);
    }
}
