using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy2Script : MonoBehaviour
{
    //public ParticleSystem parsys;
    
    public int Health;
    public int HealthMax;

    AudioSource scream;

    bool audioonce;
    
    [SerializeField]
    float agroRangeMax;
    [SerializeField]
    float agroRangeMin;
    [SerializeField]
    float agroRangeOk;
    [SerializeField]
    float agroRangeAttack;
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    float wanderSpeed;
    [SerializeField]
    float maxDistance;    
    [SerializeField]
    float range;    
    [SerializeField]
    Transform CastPos;
    [SerializeField]
    Transform BackPos;
    [SerializeField]
    GameObject ParHitPoison;

    public float countCast = 0.0f;
    public float timeCast;
    public float timeCastC;
    public float RespTime;


    Rigidbody2D rb2d;
    Animator anime;
    BoxCollider2D boxC;

    public GameObject Player;
    public GameObject spawnPoint;

    float distPlayer;

    bool isCharging = false;
    bool isCasting = false;
    public bool isFacingLeft = false;
    bool isPoison = false;
    bool isAboutCast = false;
    bool isFlee = false;
    bool isHiding = false;
    bool detected;
    bool isWaiting;

    Vector2 wayPoint;
    Vector2 respawn;
    bool newWay;    

    string magiaC;

    UnityEngine.Object magiaRef;
    UnityEngine.Object EnemyRef;
    IEnumerator MagiaChargeR;
    IEnumerator poisonR;

    IEnumerator detectedTimeR;

    bool detectedTimer;

    Transform player;

    public Slider castSlider;

    public GameObject attackHit;
    bool isAttack;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
        Health = HealthMax;
        scream = GetComponent<AudioSource>();
        attackHit.SetActive(false);
        respawn = transform.position;
        SetNewDestination();
        EnemyRef = Resources.Load("Enemy2");

              
        if(isFacingLeft)
            transform.localScale = new Vector3(1, 1, 1);
        
    }

    void Update()
    {
        GetPlayerTransform();

        if (Health <= 0)
        {
            //gameObject.SetActive(false);
            Destroy(gameObject);
            Invoke ("Death", RespTime);
        }

        ///////////////////////////////////////////AGRO PLAYER 
         if(CanSeePlayer(agroRangeOk)) 
           { detected = true;
            if(!audioonce)
                {
                scream.Play();
                 audioonce = true;
                 }
           }

        else if(!CanSeePlayer(agroRangeMin) && !detectedTimer)
            {      
            if (detectedTimeR != null )
            StopCoroutine(detectedTimeR);
            
            detectedTimeR = detectedTime();
            StartCoroutine(detectedTimeR);    
            }
        
        if (CanSeePlayer(agroRangeMin) && !CanSeePlayer(agroRangeAttack) && !isAttack && detected)
        {
            ChasePlayer();
        }
         
        else if(CanSeePlayer(agroRangeAttack) && !isAttack ) 
           {
            StartCoroutine(Attack());

           }       
       
        else
        {
         
            StopChase();
            
        }
    }

  //////////////////////////////////////////////////////ACTIONS  
    void ChasePlayer()
    {
        if (transform.position.x < player.position.x)
        {
            rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);
            transform.localScale = new Vector3(-1, 1, 1);
            isFacingLeft = false;

        }

        else if (transform.position.x > player.position.x)
        {
            rb2d.velocity = new Vector2(-moveSpeed, rb2d.velocity.y);
            transform.localScale = new Vector3(1, 1, 1);
            isFacingLeft = true;

        }
    }

    IEnumerator Attack()
    {
       
        rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        isAttack = true;
        anime.Play("shantaemonsterATTACK");
        yield return new WaitForSeconds(.5f);
        attackHit.SetActive(true);
        yield return new WaitForSeconds(.5f);
        attackHit.SetActive(false);
        anime.Play("shantaemonster");
        yield return new WaitForSeconds(.5f);
        isAttack = false;

    }

    void StopChase()
    {
        
        if(Vector2.Distance(transform.position, wayPoint) < range || detected)
        {
            StartCoroutine(wanderTime(Random.Range(5, 7)));
            SetNewDestination();
        }

        if (transform.position.x < wayPoint.x && !isWaiting)
        {
            rb2d.velocity = new Vector2(wanderSpeed, rb2d.velocity.y);
            transform.localScale = new Vector3(-1, 1, 1);
            isFacingLeft = false;

        }

        else if (transform.position.x > wayPoint.x && !isWaiting)
        {
            rb2d.velocity = new Vector2(-wanderSpeed, rb2d.velocity.y);
            transform.localScale = new Vector3(1, 1, 1);
            isFacingLeft = true;

        }
     
     

    }

/////////////////////////////////////////////////////ENEMY WANDER

    void SetNewDestination()
    {
        if(!newWay)
        {
            wayPoint = new Vector2(transform.position.x + Random.Range(-maxDistance, maxDistance), transform.position.y);
            newWay = true;
        }

        else if(newWay)
        {
            wayPoint = respawn;
            newWay = false;
        }
    }

/////////////////////////////////////////////////////PLAYER TRACER
    void GetPlayerTransform()
    {
        GameObject playerPos = GameObject.Find("Player");
        if(playerPos != null)
          player = playerPos.transform;
          
        
        
    }

    bool CanSeePlayer(float distance)
    {
        bool val = false;
        float castDist = distance;
       

        if(isFacingLeft)
            {
                castDist = -distance;
            }

        Vector2 endPosBack = CastPos.position + Vector3.right * castDist;
        Vector2 endPos = BackPos.position - Vector3.right * castDist;

        RaycastHit2D hit = Physics2D.Linecast(CastPos.position, endPos, 1 << LayerMask.NameToLayer("Player"));
        RaycastHit2D hitBack = Physics2D.Linecast(BackPos.position, endPosBack, 1 << LayerMask.NameToLayer("Player"));

        if(hit.collider !=null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                val = true;
            }
            else
            {
                val = false;
            }
            Debug.DrawLine(CastPos.position, hit.point, Color.red);
        }

        else if(hitBack.collider !=null)
        {
            if (hitBack.collider.gameObject.CompareTag("Player"))
            {
                val = true;
            }
            else
            {
                val = false;
            }
            Debug.DrawLine(BackPos.position, hitBack.point, Color.red);
        }

        
        else
        {
            Debug.DrawLine(CastPos.position, endPos, Color.blue);
            Debug.DrawLine(BackPos.position, endPosBack, Color.blue);

        }
        return val;
           
    }
//////////////////////////////////////////////////////ROTINAS MAGIA
        
     IEnumerator Poison(int damage)
     {
        
        if (poisonR != null)
        {
        StopCoroutine(poisonR);
        }

        poisonR = PoisonR(damage);
        StartCoroutine(poisonR);
        yield return new WaitForSeconds(1);

    
     }

    IEnumerator PoisonR(int damage)
    {
        for(int i = 10; i > 0; i--)  
        {
 
            isPoison = true;

            Health -= damage;
            Instantiate(ParHitPoison, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            yield return new WaitForSeconds(2);
        } 

        isPoison = false;
    }

        void Hit(int damage)
    {
        //parsys.Play();
        Health -= damage;
        detected = true;

    }

        IEnumerator wanderTime(float timeWander)
    {
        isWaiting = true;
        yield return new WaitForSeconds(timeWander);
        isWaiting = false;
      
    }

    IEnumerator detectedTime()
    {
        detectedTimer = true;
        yield return new WaitForSeconds(10);
        if(!CanSeePlayer(agroRangeMin))
        detected = false;
        detectedTimer = false;



    }

    void Death()
    {
        GameObject EnemyResp = (GameObject)Instantiate(EnemyRef);
        EnemyResp.transform.position = respawn;
        Destroy(gameObject);
    }

}