using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering.Universal;

public class FamiliarScript : MonoBehaviour
{


    [Header("References")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Rigidbody2D rb;   
    [SerializeField] private GameObject lightFocus;

    [SerializeField] public Light2D light;






    [Header("Attribute")]
    [SerializeField] private float targetRange = 5f;
    [SerializeField] private float bps = 1f; // Bullets per second
    [SerializeField] private float speed = 2f;



    private Transform target;
    public float OffSet;
    
    [SerializeField] public Transform familiarPos;
    [SerializeField] public Transform PlayerPos;


    private float timeUntilFire;
    bool lightC;
    bool floatC;

    bool isAttacking;

    public Vector2 yOffSet;
    public Vector2 familiar;






    // Start is called before the first frame update
    void Start()
    {
        light = lightFocus.GetComponent<Light2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame

    IEnumerator lightR()
    {

            lightC = true;
            light.falloffIntensity = Random.Range(0.8f, 1f);
            yield return new WaitForSeconds(.1f);
            lightC = false;

        
    }

    IEnumerator floatR()
    {

        floatC = true;
        rb.velocity = new Vector2(rb.velocity.x, Random.Range(-speed, speed));
        yield return new WaitForSeconds(.3f);
        floatC = false;


    }
    void Update()
    {
        if(!isAttacking)   
        {
        transform.position = Vector2.Lerp(transform.position, familiarPos.position, OffSet * Time.deltaTime);
        }

        if(Vector2.Distance(transform.position, familiarPos.position) >= 2.5f)
        isAttacking = false;

        if (!lightC)
        {
            StartCoroutine(lightR());
        }

        if (!floatC)
        {
            StartCoroutine(floatR());
        }

        if (target == null)
        {
            isAttacking = false;
            FindTarget();

           if (PlayerPos.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

            else if (PlayerPos.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }

            return;
        }

        if (!CheckTargetRange())
        {
            target = null;
        }
        else
        {

            timeUntilFire += Time.deltaTime;

            if (target.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

            else if (target.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }

            if (timeUntilFire >= 1f / bps)
            {
                Shoot();
                timeUntilFire = 0;
            }         

        }

     


    }

    private void Shoot()
    {   
        Vector3 offSetD = new Vector3(target.position.x, target.position.y + .05f, 0);
        Vector3 _direction = (offSetD - firePoint.position).normalized;
        isAttacking = true;
        GameObject bulletObj = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        FamiliarBulletScript bulletScript = bulletObj.GetComponent<FamiliarBulletScript>();
        bulletScript.SetTarget(target);
        bulletScript.GetDirection(_direction);


    }

    private bool CheckTargetRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetRange;
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetRange, (Vector2)transform.position, 0f, enemyMask);
        if(hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

  /*  private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetRange);
    }*/

}
